// FirstClass.cpp : CFirstClass ��ʵ��

#include "stdafx.h"
#include "FirstClass.h"


// CFirstClass



STDMETHODIMP CFirstClass::Add(LONG para1, LONG para2, LONG* result)
{
	// TODO:  �ڴ����ʵ�ִ���
	*result = para1 + para2;
	return S_OK;
}
