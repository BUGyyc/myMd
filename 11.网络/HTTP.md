

HTTP
应用层协议
HTTP是一个简单的请求-响应协议，它通常运行在TCP之上。它指定了客户端可能发送给服务器什么样的消息以及得到什么样的响应。请求和响应消息的头以ASCII码形式给出；而消息内容则具有一个类似MIME的格式。这个简单模型是早期Web成功的有功之臣，因为它使得开发和部署是那么的直截了当。


# TCP/IP 协议分层
应用层、传输层、网络层、数据链路层

## 应用层

向用户提供应用服务时的通信活动。
如：FTP、HTTP、WebSocket、SMTP等

## 传输层

提供处于网络连接的两台计算机之间的数据传输
如：TCP、UDP

## 网络层

用来处理在网络上流动的数据包。数据包是网络传输的最小单位。
如：IP

## 数据链路层

用来处理连接网络的硬件部分。

# 网络通信流程

以简单的浏览网页的情景为例，点击网页上的按钮。
- 首页作为发送端的客户端在应用层（HTTP协议）发送一个点击按钮的HTTP请求
- 在传输层（TCP）把从应用层收到的HTTP请求报文进行分割，并且在各个分割的报文上按顺序标记序号，然后发送给网络端
- 在网络层（IP协议），增加作为通信目的地的MAC地址后转发给数据链路层。

接收端的服务器在数据链路层接受到数据，按序号 往上层发送，一直到应用层。当传输到应用层，才能算是真正接受到客户端发送过来的HTTP请求。

# HTTP的特点

- 客户端与服务器的通信总是先从客户端开始建立，服务端在没有收到请求之前不会发送相应
- HTTP是无状态协议，这是为了减小服务器压力，让服务器更快处理大量事务。后来为了更方便通信，引入了Cookie
- 客户端对服务器可使用的方法：
    - Get ：用来请求访问已被URI识别的资源。
    - Post : 用来传输实体的主体。
    - Put : 用来传输文件
    - Head : 用来获取报文首部
    - Delete : 用来删除文件
    - Options : 用来查询支持的方法
    - Trace : 

- 持久连接： 早期的HTTP1.0版本中每次通信都需要建立连接，再通信，通信完成后再断开。这造成了过多的资源消耗。后来HTTP1.1引入了持久连接，建立TCP连接后，可以进行多次HTTP通信，最后再去发起TCP断开。

- 管线化： 管线化出现后，不用等待响应亦可发送下一个请求，这样可以并行发送多个请求，而不是等待再发送。



https://zhuanlan.zhihu.com/p/60450391