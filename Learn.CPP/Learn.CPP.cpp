// Learn.CPP.cpp : �������̨Ӧ�ó������ڵ㡣
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
	//����ģʽ(Factory)
	Factory* fac = new ConcreateFactory();
	Product* p = fac->CreateProduct();
	
	getchar();
	return 0;
}

