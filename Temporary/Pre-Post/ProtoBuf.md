# 前言
Protobuf实际是一套类似Json或者XML的数据传输格式和规范，用于不同应用或进程之间进行通信时使用。通信时所传递的信息是通过Protobuf定义的message数据结构进行打包，然后编译成二进制的码流再进行传输或者存储。


# protobuf常规结构
```
message MsgA 
{
        required uint32 a = 1;
        required MsgB b = 2; //MsgB也是个 message 对象
}

message MsgB
{
    required uint32 temp = 1;
    optional uint32 temp2 = 2;
    repeated uint32 temp3 = 3;
}
```
- required、optional、repeated的含义
  required不可缺少字段
  optional可缺少字段
  repeated可重复字段，类似List、数组

# protobuf源码
  
http://code.google.com/p/protobuf/downloads/list



# protobuf的优势

- 序列化后体积很小:消息大小只需要XML的1/10 ~ 1/3
- 解析速度快:解析速度比XML快20 ~ 100倍

# 为什么protobuf 可以更小更快

传统的Json存储格式如下
``
{ "id": 1, "name": "hello",  "height": 170, "weight": 140 }
``

每个Key对应一个Value。实际上，每个Key也是要占一定存储的，并且所占并不小。不仅仅是存储上，JSON 必须全文扫描无法跳过不需要的字段，意味着速度上会更慢一些

JSON 的字段是用字符串指定的，相比之下字符串比对应该比基于数字的字段 tag 更耗时。

我们希望存储的数据是紧凑有效的，所以Protobuf选择了下面的方式

- Tag配对
  
    Tag|Value 与 Json 中 Key|Value有什么区别呢？
    JSON 中 Key是用字符串表示的，意味着占的字节比较多，而Tag采用二进制进行存储，通常只占一个字节，所以也相比JSON更小。

- Varint 存储较小数据

    通常Integer是32位表示，需要4个字节，而我们操作数据的过程中，一般情况下，数字不会太大，小于255是多数情况，也就是说，如果按照之前的存储方式，为了表示一个小于255的数字。它真实的有用的数据只占一个字节，我们却用4字节（32位）来存储它，真造成了极大的浪费。采用 Varint 后，可以用更少的字节数来表示数字信息。

- Leg 确定数据长度，加快解析速度

    仅仅是Tag|Value是无法让我们直接确定数据的长度，我们希望在查找的时候就直接获得准确长度，以便快速解析。所以采取Tag|Leg|Value.
    相比于JSON，JSON需要通过Key,因为Key是字符串，需要先从字符串中识别出数据类型，最后才能知道数据的长度，速度是不如protobuf.



# protocol buffer GC 

<https://www.cnblogs.com/SChivas/p/7898166.html>

---
参考

  https://www.ibm.com/developerworks/cn/linux/l-cn-gpb/index.html

  https://www.jianshu.com/p/72108f0aefca

  http://www.sohu.com/a/136487507_505779

  https://zhuanlan.zhihu.com/p/53339153

  https://halfrost.com/protobuf_encode/
