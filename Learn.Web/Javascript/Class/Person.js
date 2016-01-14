//原型继承的例子
var Person = {
    name: "default name",
    getName: function () {
        return this.name;
    }
};

var Author = Common.inherit(Person);
Author.books = [];
Author.getBooks = function () {
    return this.books;
};
