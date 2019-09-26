
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

## 垃圾回收机制

## 减少垃圾回收

### 数组优化

将[]赋值给一个数组对象，是清空数组的捷径（例如： arr = [];），但是需要注意的是，这种方式又创建了一个新的空对象，并且将原来的数组对象变成了一小片内存垃圾！实际上，将数组长度赋值为0（arr.length = 0）也能达到清空数组的目的，并且同时能实现数组重用，减少内存垃圾的产生。



https://www.cnblogs.com/zhwl/p/4664604.html

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



```
// 让我们从一个自身拥有属性a和b的函数里创建一个对象o：
let f = function () {
   this.a = 1;
   this.b = 2;
}
/* 这么写也一样
function f() {
  this.a = 1;
  this.b = 2;
}
*/
let o = new f(); // {a: 1, b: 2}

// 在f函数的原型上定义属性
f.prototype.b = 3;
f.prototype.c = 4;

// 不要在 f 函数的原型上直接定义 f.prototype = {b:3,c:4};这样会直接打破原型链
// o.[[Prototype]] 有属性 b 和 c
//  (其实就是 o.__proto__ 或者 o.constructor.prototype)
// o.[[Prototype]].[[Prototype]] 是 Object.prototype.
// 最后o.[[Prototype]].[[Prototype]].[[Prototype]]是null
// 这就是原型链的末尾，即 null，
// 根据定义，null 就是没有 [[Prototype]]。

// 综上，整个原型链如下: 

// {a:1, b:2} ---> {b:3, c:4} ---> Object.prototype---> null

console.log(o.a); // 1
// a是o的自身属性吗？是的，该属性的值为 1

console.log(o.b); // 2
// b是o的自身属性吗？是的，该属性的值为 2
// 原型上也有一个'b'属性，但是它不会被访问到。
// 这种情况被称为"属性遮蔽 (property shadowing)"

console.log(o.c); // 4
// c是o的自身属性吗？不是，那看看它的原型上有没有
// c是o.[[Prototype]]的属性吗？是的，该属性的值为 4

console.log(o.d); // undefined
// d 是 o 的自身属性吗？不是，那看看它的原型上有没有
// d 是 o.[[Prototype]] 的属性吗？不是，那看看它的原型上有没有
// o.[[Prototype]].[[Prototype]] 为 null，停止搜索
// 找不到 d 属性，返回 undefined

```
优先找自身属性，然后再在原型链上找

```
function doSomething(){}
doSomething.prototype.foo = "bar";
var doSomeInstancing = new doSomething();
doSomeInstancing.prop = "some value";
console.log("doSomeInstancing.prop:      " + doSomeInstancing.prop);
console.log("doSomeInstancing.foo:       " + doSomeInstancing.foo);
console.log("doSomething.prop:           " + doSomething.prop);
console.log("doSomething.foo:            " + doSomething.foo);
console.log("doSomething.prototype.prop: " + doSomething.prototype.prop);
console.log("doSomething.prototype.foo:  " + doSomething.prototype.foo);
```

https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Inheritance_and_the_prototype_chain


