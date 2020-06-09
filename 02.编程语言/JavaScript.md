


https://www.w3cschool.cn/javascript/javascript-skillmap.html

# javascript 中的类型

Undefined NULL Number String Boolean Object

# typeof 判断返回以下结果

- udefined ， 值未定义
- boolean , 值为布尔型
- string , 值为字符串
- number ， 值为数值类型
- object , 值为对象类型或者null，null是特殊值，被认为是空的对象引用
- function , 值为函数

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


# 其他

https://blog.fundebug.com/2017/07/17/10-javascript-difficulties/

https://juejin.im/entry/588037d38d6d810058af5d01


---

# Js中的变量与对象
在Js中所有的变量都可以当作对象使用，除了两个例外null和undefined

    function foo(){}
    foo.a = 1;


## 访问属性

    var a = {b:1};
    a['b']; //1

## 删除属性

delete 数组中删除，数组长度不会变，被删除的项变为Undefined


# 原型

Js 不包含传统的类继承模型，而是使用prototype原型模型

## 属性查找

Js的属性查找，会向上遍历查找原型链，直到找到给定名称的属性为止。
到达原型链顶部Object.prototype，未找到就返回Undefined.
优先查找自身，然后再查找原型链


# 函数

函数是Js中的一种对象，它可以像值一样传递。


# This

执行上下文

# 闭包与引用

    for(var i = 0;i<10;i++){
        setTimeout(function(){
            console.log(i);
        },1000);
    }

    for(var i =0;i<10;i++){
        (function(a){
            setTimeout(function(){
                console.log(a);
            },1000)
        })(i);
    }

    for(var i = 0;i<10;i++){
        setTimeout(
            (function(a){
                return function(){
                    console.log(a);
                }
            })(i),1000)
    }

# arguments


# 构造函数

# 作用域

## 变量提升
var的问题，Js先编译再执行
建议使用let

# 数组的操作技巧

Js中数组是对象，
避免使用 for in 遍历数组，因为会查找到原型链上，需要使用hasOwnProperty

## 数组合并
    Array.prototype.push.apply(arr1,arr2)
    //相当于如下
    arr1 = arr1 + arr2//arr1包含1，2

## 数组最大值与最小值

    var arr = [1,2,3]
    Math.max.apply(Math,arr);//3
    Math.min.apply(Math,arr);//1

delete 与 remove 都不能完全清理干净数组，数组长度依然没变，只是值变成了undefined

## Array构造函数


# 类型判断与比较

Js是弱类型语言，这就意味着，==操作符会在比较前先进行强制类型转换

    “” == “0” //false
    0  == “”  //true
    0  == "0" //true
    false == "false" //false
    false == "0"  //true
    false == undefined  //false
    false == null   //false
    null == undefined //true

## typeOf

- typeof：JavaScript一元操作符，用于以字符串的形式返回变量的原始类型，注意，typeof null也会返回object，大多数的对象类型（数组Array、时间Date等）也会返回object



## instanceOf

instanceOf操作符应该仅仅用来比较来自同一个Js上下文的自定义对象。
- instanceof：JavaScript操作符，会在原型链中的构造器中搜索，找到则返回true，否则 返回false


    var arr = ["a", "b", "c"];
    typeof arr;   
    // 返回 "object" 
    arr instanceof Array // true
    arr.constructor();  //[]

## undefined与null

两者都表示‘空’，

undefined表示一个值为undefined的类型

在Js中null也是一种数据类型

## setTimeout与setInterval

由于Js是异步的，可以使用setTimeout与setInterval来计划执行函数

# 其他

## 对象转数组
    var arr = Array.prototype.slice.call(arguments)


