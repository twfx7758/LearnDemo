#include <stdio.h>
class Apple{
public:
	Apple() = default;
	Apple(Apple& a){ printf("copy apple()!\n"); }
	Apple& operator=(Apple& a){ printf("=Apple()\n"); return *this; }
	~Apple(){ printf("~Apple()!\n"); }
	void print() const { printf("调用了print方法!\n"); }
};