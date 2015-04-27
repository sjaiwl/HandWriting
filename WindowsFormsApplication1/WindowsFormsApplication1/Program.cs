using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //程序可视化样式
            Application.EnableVisualStyles();
            //初始化控件的初始值
            Application.SetCompatibleTextRenderingDefault(false);
            //主程序开始
            Application.Run(new Form1());
        }
    }
}
