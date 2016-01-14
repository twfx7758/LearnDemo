(function () {
    window.Common = {
        //原型扩展
        inherit: function (obj) {
            //检测一下对象
            if (obj == null) throw new TypeError();
            //如果支持对象创建方法
            if (Object.create)  return Object.create(obj);
            var t = typeof obj;
            if (t !== "Object" && t !== "Function") return new TypeError();
            var F = function () { };
            F.prototype = obj;
            return new F();
        },
        //类扩展
        extend: function (subClass, superClass) {
            var F = function () { };
            F.prototype = superClass.prototype;
            subClass.prototype = new F();
            subClass.prototype.constructor = subClass;

            subClass.superClass = superClass.prototype;
            if (superClass.prototype.constructor == Object.prototype.constructor) {
                superClass.prototype.constructor = superClass;
            }
        },
        //掺元类，可以复制全部或者部分方法
        //例：augment(Author, Mixin, 'serialize');
        augment: function (receivingClass, givingClass) {
            if (arguments[2]) {
                for (var i = 2, len = arguments.length; i < len; i++) {
                    receivingClass.prototype[arguments[i]] = givingClass.prototype[arguments[i]];
                }
            }
            else {
                for (methodName in givingClass.prototype) {
                    if (!receivingClass.prototype[methodName]) {
                        receivingClass.prototype[methodName] = givingClass.prototype[methodName];
                    }
                }
            }
        },

        //字符串格式化
        format: function () {
            if (arguments.length == 0)
                return null;
            var str = arguments[0];
            for (var i = 1; i < arguments.length; i++) {
                var valtemp = "";
                if (arguments[i] != null) {
                    valtemp = arguments[i];
                }

                var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
                str = str.replace(re, valtemp);
            }
            return str;
        }
    };
})()