# HandWriting
MFC框架实现简单手写字符识别

## 项目简介
### 介绍
项目旨在基于机器学习算法实现手写字符（包括数字和大小写字母）的识别。该系统通过读入一张字符图片的方式，实现识别对象的输入，通过BP神经网络、支持向量机等实现手写字符的识别，并输出识别的结果。
### 内容
* 1）手写字符的输入
* 2）图片预处理与特征提取
* 3）输出识别结果和图像信息

### 开发环境
* A）Visual Studio 2013
* B）MNIST 数据集

## 项目组成
基于机器学习的手写字符识别系统通常包括四个部分：图像预处理、特征提取、输出识别结果和图像信息四个阶段，如图所示：

![image](https://github.com/sjaiwl/image_folder/blob/master/handWriting/hand3.png)

## 项目设计
* 1.基于MNIST 数据集的图像识别系统，首先就是要加载MNIST数据集，需要下载两个文件:train-images.idx3-ubyte和train-labels.idx1-ubyte，从而导入图像信息，在程序中加载正确的路径，导入图片。

![image](https://github.com/sjaiwl/image_folder/blob/master/handWriting/hand1.png)

* 2.载入图片后，点击识别可以识别图片的信息，识别是基于MNIST 数据集的识别算法，然后输出相应图片对应的识别结果。

![image](https://github.com/sjaiwl/image_folder/blob/master/handWriting/hand2.png)

* 3.可以一次载入图像集中的所有图片，同时输出结果不仅有识别结果，同时还输出了图片的像素信息。

![image](https://github.com/sjaiwl/image_folder/blob/master/handWriting/hand4.png)

## 使用说明
* 1.首先要确保加载的train-images.idx3-ubyte和train-labels.idx1-ubyte的路径的正确，此处要使用绝对路径，以免发生错误。

* 2.点击“载入图片”按钮，然后从数据集中载入图片。

* 3.点击“下一张图片”按钮，然后输出信息中会显示图片的识别结果，同时右边输出结果是图片的像素信息。

![image](https://github.com/sjaiwl/image_folder/blob/master/handWriting/hand5.png)

## 总结
* 1.该项目能基本识别所有的数字，对字母和汉字还没法识别，还需要进一步的改进，同时识别的效率还有待提高。
* 2.MNIST数据集的导入只能使用绝对路径，还需要进一步的改进。
* 3.自己在完成系统后，对MFC的编程更加熟悉，同时自己对C#的基本语法有了一定的了解。
* 4.自己对具体的识别算法还不是很熟悉，还需要更深入的理解，对于BP神经网络和SVM支持向量机的识别算法需要进一步理解。

