#pragma once
#include <iostream>
#include <vector>
#include <list>
#include <algorithm>
#include <string>

using namespace std;

//ɢ�к���������tableSize�Ǹ�������
//(�����ַ��������)
int hash(const string& key, int tableSize) {
	int hashVal = 0;
	for (int i = 0; i < key.length(); i++) {
		hashVal = hashVal * 37 + key[i];
	}

	hashVal %= tableSize;
	if (hashVal < 0)
		hashVal += tableSize;

	return hashVal;
}


//ʵ�ַ������ӷ�����Ҫ����ṹ
template<typename HashedObj>
class HashTable
{
public:
	explicit HashTable(int size = 101);

	bool contains(const HashedObj& x) const;

	void makeEmpty();
	void insert(const HashedObj& x);
	void remove(const HashedObj& x);
private:
	vector<list<HashedObj>> theLists;
	int currentSize;

	void rehash();
	int myhash(const HashedObj& x) const;
};

int hash(const string& key);
int hash(int key);
