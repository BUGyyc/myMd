

回溯
动态规划
贪心算法
分治算法

A星算法
https://blog.csdn.net/denghecsdn/article/details/78778769

https://blog.csdn.net/zerokkqq/article/details/76282908

https://gameinstitute.qq.com/community/detail/117767

渲染管线
DrawCall
Z轴缓存

OpenGL简单流程
- 顶点着色器：将输入的3D坐标转化为相应的3D坐标
- 形状装配：将坐标组合为一个形状
- 几何着色器：将形状细分成一些三角形
- 光栅化：把三角形生成出多个像素点
- 片段着色器：给像素点上色
- 混合测试：因为图形有遮盖等层次关系，以及一些透明度遮挡，最后处理输出能展示的图形

PureMVC

-Mediator 与 View
-Proxy 与 Model
-Command 与 Controller

Facade 单例直接管理 View、Model、Controller
Facade 也可直接管理所有的Mediator、Proxy、Command

传统的MVC模式下，Controller一般是最臃肿的

https://blog.csdn.net/qq_29579137/article/details/73692842


TCP
面向连接的、可靠的、基于字节流的传输层通信协议。
流量控制采用滑动窗口协议
拥塞控制的几种方式：
慢启动，拥塞避免，快速重传，快速恢复，网络拥塞
流量控制滑动窗口

UDP
UDP是面向报文的。发送方的UDP对应用程序交下来的报文，在添加首部后就向下交付给IP层。既不拆分，也不合并，而是保留这些报文的边界，因此，应用程序需要选择合适的报文大小。

KCP
https://github.com/skywind3000/kcp

kcp协议是传输层的一个具有可靠性的传输层ARQ协议。它的设计是为了解决在网络拥堵情况下tcp协议的网络速度慢的问题。kcp力求在保证可靠性的情况下提高传输速度。kcp协议的关注点主要在控制数据的可靠性和提高传输速度上面，因此kcp没有规定下层传输协议，一般用udp作为下层传输协议，kcp层协议的数据包在udp数据报文的基础上增加控制头。当用户数据很大，大于一个udp包能承担的范围时（大于mss），kcp会将用户数据分片存储在多个kcp包中。因此每个kcp包称为一个分片。
为了提供可靠性，kcp采用了重传机制。为实现重传机制，kcp为每个分片分配一个唯一标识，接收方收到一个包后告知发送方接到的包的序号，发送方接到确认后再继续发送。而如果发送方在一定时间内（超时重传时间）没有接到确认，就说明数据包丢失了，发送方需要重传丢失的数据包，所以发送方会把待确认的数据缓存起来，方便重传。
https://www.cnblogs.com/yuanyifei1/p/6846310.html

HTTP
应用层协议
HTTP是一个简单的请求-响应协议，它通常运行在TCP之上。它指定了客户端可能发送给服务器什么样的消息以及得到什么样的响应。请求和响应消息的头以ASCII码形式给出；而消息内容则具有一个类似MIME的格式。这个简单模型是早期Web成功的有功之臣，因为它使得开发和部署是那么的直截了当

Socket
不属于应用层也不属于传输层，它是一个网络编程接口，
https://www.cnblogs.com/wangcq/p/3520400.html


WebSocket
WebSocket是一种在单个TCP连接上进行全双工通信的协议。WebSocket通信协议于2011年被IETF定为标准RFC 6455，并由RFC7936补充规范。WebSocket API也被W3C定为标准。
它是为了解决HTTP连接的频繁断开再重连的消耗问题

ProtoBuf

WebGL
OpenGL

----------
Unity

噪声函数
perlin
柏林噪声
ECS
componement只包含数据
compenmentSysttem包含行为


C#委托
希望函数可以想参数一样传递，委托（Delegate）特别用于实现事件和回调方法。所有的委托（Delegate）都派生自 System.Delegate 类。
https://www.cnblogs.com/murongxiaopifu/p/4149659.html

事件

装箱与拆箱
装箱是将值类型转换为引用类型 ；拆箱是将引用类型转换为值类型 
被装过箱的对象才能被拆箱

.NET中，数据类型划分为值类型和引用(不等同于C++的指针)类型，与此对应，内存分配被分成了两种方式，一为栈，二为堆，注意：是托管堆。
值类型只会在栈中分配。
引用类型分配内存与托管堆。
托管堆对应于垃圾回收。
https://www.cnblogs.com/huashanlin/archive/2007/05/16/749359.html


GC
https://www.cnblogs.com/zblade/p/6445578.html

内存优化
1.资源内存占用；2.引擎模块自身内存占用；3.托管堆内存占用。
https://zhuanlan.zhihu.com/p/21913770
https://cloud.tencent.com/developer/article/1336507

MONO
掉帧与补帧
帧同步
浮点计算

https://blog.csdn.net/u012999985/article/details/79090657


- 序列化与反序列化
- protobuf
hash \hash碰撞
- 垃圾回收总览
- TCP
- UDP
- KCP
WebSocket
网络工程


