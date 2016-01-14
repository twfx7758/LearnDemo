// FirstClass.cpp : CFirstClass 的实现

#include "stdafx.h"
#include "FirstClass.h"


// CFirstClass



STDMETHODIMP CFirstClass::Add(LONG para1, LONG para2, LONG* result)
{
	// TODO:  在此添加实现代码
	*result = para1 + para2;
	return S_OK;
}
