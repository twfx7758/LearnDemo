// Learn.CPP.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <iomanip>
#include <string>
#include <cctype>
#include "Factory.h"
#include "Product.h"

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	//工厂模式(Factory)
	Factory* fac = new ConcreateFactory();
	Product* p = fac->CreateProduct();
	
	getchar();
	return 0;
}

