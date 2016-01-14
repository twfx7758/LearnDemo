#include <stdio.h>
#include <memory>
#include "Apple.h"
#include <vector>
#include "Employee.h"

using namespace std;

Apple* process()
{
	Apple* p1 = new Apple();
	return p1;
}

void process2()
{
	shared_ptr<Apple> s_ptr = make_shared<Apple>();
}

int main()
{
	/*vector<int> vec = { 1, 2, 3, 4, 5, 6 };
	for (auto item = vec.begin(); item != vec.end(); ++item)
		int m = *item;*/
	
	Employee m;
	Employee n;
	m.value = 10;
	n.value = 100;
	
	getchar();
	return 0;
}