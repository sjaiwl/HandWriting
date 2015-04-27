using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //初始化引用文件的路径，这里使用绝对路径。
        private string pixelFile =
         @"D:\VS2013 Projects\WindowsFormsApplication1\WindowsFormsApplication1\train-images.idx3-ubyte";
        private string labelFile =
         @"D:\VS2013 Projects\WindowsFormsApplication1\WindowsFormsApplication1\train-labels.idx1-ubyte";
        //初始化trainImages
        private DigitImage[] trainImages = null;
        public Form1()
        {
            //初始化
            InitializeComponent();
            textBox1.Text = pixelFile;
            textBox2.Text = labelFile;
            comboBox1.SelectedItem = "6";
            this.ActiveControl = button1;
        }

        //button1_Click触发函数的定义
        private void button1_Click(object sender, EventArgs e)
        {
            this.pixelFile = textBox1.Text;
            this.labelFile = textBox2.Text;
            //从数据集导入图片信息
            this.trainImages = LoadData(pixelFile, labelFile);
            //导入成功的提示
            listBox1.Items.Add("MNIST images loaded into memory");
        }

        //button2_Click的触发函数定义
        private void button2_Click(object sender, EventArgs e)
        {
            // Display 'next' image
            int nextIndex = int.Parse(textBox4.Text);
            DigitImage currImage = trainImages[nextIndex];
            //放大倍数
            int mag = int.Parse(comboBox1.SelectedItem.ToString());
            Bitmap bitMap = MakeBitmap(currImage, mag);
            //显示图片
            pictureBox1.Image = bitMap;
            //输出像素二进制数据
            string pixelVals = PixelValues(currImage);
            textBox5.Text = pixelVals;
            //标签赋值
            textBox3.Text = textBox4.Text; // Update curr idx
            textBox4.Text = (nextIndex + 1).ToString(); // ++next index
            //输出结果
            listBox1.Items.Add("当前图像索引 = " +
            textBox3.Text + " 识别结果 = " + currImage.label);
        }

        //整数转换为二进制串
        public static string IntToBinaryString(int v)
        {
            // to pretty print an int as binary
            string s = Convert.ToString(v, 2); // 转换为等效字符串
            string t = s.PadLeft(32, '0'); // 字符对齐
            string res = "";//转换结果变量初始化
            for (int i = 0; i < t.Length; ++i)
            {
                if (i > 0 && i % 8 == 0)
                    res += " "; // add a space every 8 chars
                res += t[i];//串值添加
            }
            return res;
        }

        //读取图片
        public static DigitImage[] LoadData(string pixelFile, string labelFile)
        {
            int numImages = 60000;//图片总数量
            DigitImage[] result = new DigitImage[numImages];//定义result数组
            //创建28 x 28 矩阵
            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];
            //文件读取操作
            FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
            FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);
            //二进制数据信息读取
            BinaryReader brImages = new BinaryReader(ifsPixels);
            BinaryReader brLabels = new BinaryReader(ifsLabels);
            //MNIST 培训像素文件的格式已初始魔法的整数(32 位)值
            //在打开后的二进制文件，第一次四个整数是阅读使用 ReadInt32 方法
            int magic1 = brImages.ReadInt32(); //读取4字节整数
            magic1 = ReverseBytes(magic1); // 数据格式化
            int imageCount = brImages.ReadInt32();
            imageCount = ReverseBytes(imageCount);
            int numRows = brImages.ReadInt32();
            numRows = ReverseBytes(numRows);
            int numCols = brImages.ReadInt32();
            numCols = ReverseBytes(numCols);
            int magic2 = brLabels.ReadInt32();
            magic2 = ReverseBytes(magic2);
            int numLabels = brLabels.ReadInt32();
            numLabels = ReverseBytes(numLabels);

            for (int di = 0; di < numImages; ++di)
            {
                for (int i = 0; i < 28; ++i) // get 28x28 pixel values
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();//读取字节
                        pixels[i][j] = b;//像素点赋值
                    }
                }
                byte lbl = brLabels.ReadByte(); //读取字节
                DigitImage dImage = new DigitImage(28, 28, pixels, lbl);//定义DigitImage对象
                result[di] = dImage;
            } // Each image
            //关闭操作
            ifsPixels.Close();
            brImages.Close();
            ifsLabels.Close(); 
            brLabels.Close();
            //返回结果
            return result;
        }

        //扭转以字节为单位,MNIST 文件的特点
        public static int ReverseBytes(int v)
        {
            byte[] intAsBytes = BitConverter.GetBytes(v);//基础数据和字节数组转换
            Array.Reverse(intAsBytes);//反转
            return BitConverter.ToInt32(intAsBytes, 0);//基础数据和字节数组转换
        }

        //定义Bitmap图像类
        public static Bitmap MakeBitmap(DigitImage dImage, int mag)
        {
            int width = dImage.width * mag;
            int height = dImage.height * mag;//获取图像宽和高
            //构造Bitmap数据
            Bitmap result = new Bitmap(width, height);
            //通过Bitmap数据信息构造图片
            Graphics gr = Graphics.FromImage(result);
            for (int i = 0; i < dImage.height; ++i)
            {
                for (int j = 0; j < dImage.width; ++j)
                {
                    //设置pixelColor的值
                    int pixelColor = 255 - dImage.pixels[i][j]; // black digits
                    //定义RGB值
                    Color c = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    //定义画笔
                    SolidBrush sb = new SolidBrush(c);
                    //填充像素
                    gr.FillRectangle(sb, j * mag, i * mag, mag, mag);
                }
            }
            return result;
        }

        //像素点的值
        public static string PixelValues(DigitImage dImage)
        {
            string s = "";//定义string变量
            //for循环输出像素值
            for (int i = 0; i < dImage.height; ++i)
            {
                for (int j = 0; j < dImage.width; ++j)
                {
                    s += dImage.pixels[i][j].ToString("X2") + " ";//输出像素的二进制文件
                }
                //换行
                s += Environment.NewLine;
            }
            return s;
        }
    }

    //定义表现图像的类
    public class DigitImage
    {
        public int width; // 28
        public int height; // 28
        public byte[][] pixels; // 0(white) - 255(black)，像素信息
        public byte label; // '0' - '9'
        //DigitImage各参数初始化
        public DigitImage(int width, int height, byte[][] pixels, byte label)
        {
            this.width = width;//宽
            this.height = height;//高
            this.pixels = new byte[height][];//二维像素信息数组初始化
            for (int i = 0; i < this.pixels.Length; ++i)
                this.pixels[i] = new byte[width];
            //byte[][] pixels赋值
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    this.pixels[i][j] = pixels[i][j];
            //label赋值
            this.label = label;
        }
    }


}


   