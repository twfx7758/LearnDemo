#pragma once
#ifndef _PRODUCT_H_
#define _PRODUCT_H_
class Product
{
public:
	virtual ~Product() = 0;

protected:
	Product();

private:


};

class ComCreateProduct : public Product
{
public:
	ComCreateProduct();
	~ComCreateProduct();

protected:

private:

};

#endif
