//类型继承的例子
//基类
function Person(name) {
    this.name = name;
}

Person.prototype.getName = function () {
    return this.name;
};

//继承类
function Author(name, books) {
    //在使用new运算符时，系统会为你做一些事。
    //它先创建一个空对象，然后调用构造函数。
    //这个过程，这个对象处于作用域的最顶端。
    Author.superClass.constructor.call(this, name);
    //call、apply两个方法的作用一样，
    //这两个函数的作用其它就是更改对象的内部指针。
    //Person.apply(this, [name]);

    this.books = books;
}
Common.extend(Author, Person);
Author.prototype.getBooks = function () {
    return this.books;
};