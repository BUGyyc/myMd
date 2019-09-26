JS 绑定的大部分工作其实就是设定 JS 相关操作的 CPP 回调，在回调函数中关联 CPP 对象。其实主要包含如下两种类型：

    注册 JS 函数（包含全局函数，类构造函数、类析构函数、类成员函数，类静态成员函数），绑定一个 CPP 回调
    注册 JS 对象的属性读写访问器，分别绑定读与写的 CPP 回调



https://docs.cocos.com/creator/manual/zh/advanced-topics/jsb/JSB2.0-learning.html