
// HandWritingDlg.h : 头文件
//

#pragma once
#include "afxwin.h"
#include<string>
#include"cv.h"
#include"highgui.h"
#include "svm.h"
#include "BP.h"
#define Malloc(type,n) (type*)malloc((n)*sizeof(type))

// CHandWritingDlg 对话框
class CHandWritingDlg : public CDialogEx
{
// 构造
public:
	CHandWritingDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_HANDWRITING_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
private:
	CString filename;
	int methodType;
	IplImage* img; 
	BpNet bp; 
	svm_model* model;
public:
	CStatic m_Picture;
	//方法
	CComboBox m_Methods;
	//选择框
	afx_msg void OnCbnSelchangeCombo1();
	//获取路径
	std::string CHandWritingDlg::getPath();
	//获取特征
	unsigned int* CHandWritingDlg::getFeature(CvMat* row);
	CFont * f;
};
