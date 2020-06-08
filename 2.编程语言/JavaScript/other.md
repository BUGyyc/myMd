
# Array

- push() 尾部添加元素
- unshift() 数组头部添加元素
- concat() 合并两个数组
- pop() 删除尾部元素并返回
- shift() 删除头部元素并返回

值类型用 typeOf 检测类型，引用类型用 instanceOf 检测类型

---

# 作用域链

内层函数可以访问外层函数局部变量，外层函数不能访问内层函数局部变量

---

# 回收机制

标记回收、引用计数

---

# 执行域
var 局部变量
出现变量提升

---
# function

## apply

函数的参数以数组形式传递

## call

函数的参数直接传递

## bind

https://www.runoob.com/w3cnote/js-call-apply-bind.html

---

# 闭包
JavaScript 变量可以是局部变量或全局变量。

私有变量可以用到闭包。

闭包是一种保护私有变量的机制，在函数执行时形成私有的作用域，保护里面的私有变量不受外界干扰。

```
function add() {
    var counter = 0;
    function plus() {counter += 1;}
    plus();    
    return counter; 
}
```
输出结果为1
counter被修改了


```
    var name = "The Window";
　　var object = {
　　　　name : "My Object",
　　　　getNameFunc : function(){
　　　　　　return function(){
　　　　　　　　return this.name;
　　　　　　};
　　　　}
　　};
　　alert(object.getNameFunc()());
```
输出为 The Window
name没有被修改

因为：
getNameFunc: function() {//假设函数名为Ａ
return function()/*假设函数名为Ｂ*/ { return this.name; };
}
在函数里面构建函数的时候，闭包产生。
在函数Ｂ内调用函数Ａ的this.name,由于函数Ａ没有name属性，所以就去找全局变量name，找到了，所以返回“The Window”，要是没有找到，则返回“undefined”。


```
    var name = "The Window";
　　var object = {
　　　　name : "My Object",
　　　　getNameFunc : function(){
　　　　　　var that = this;
　　　　　　return function(){
　　　　　　　　return that.name;
　　　　　　};
　　　　}
　　};
　　alert(object.getNameFunc()());
```
var _this = this;
return function() { return _this.name +"__"+ this.name; };


父对象的所有变量，对子对象都是可见的，反之则不成立。

http://www.ruanyifeng.com/blog/2009/08/learning_javascript_closures.html


this的指向是由它所在函数调用的上下文决定的，而不是由它所在函数定义的上下文决定的。

```
var name = "The Window";
var object = {
    name: "My Object",
    getNameFunc: function () {
        return function () {
            return this.name;
        };
    }
};
alert(object.getNameFunc()());
```

# 原型链





