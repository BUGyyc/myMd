WebSocket
WebSocket是一种在单个TCP连接上进行全双工通信的协议。WebSocket通信协议于2011年被IETF定为标准RFC 6455，并由RFC7936补充规范。WebSocket API也被W3C定为标准。
# WebSocket 的意义
它是为了解决HTTP连接的频繁断开再重连的消耗问题
早期为了实现服务端推送技术，因为HTTP 协议有一个缺陷：通信只能由客户端发起，做不到服务器主动向客户端推送信息。所以需要客户端定时的去访问服务器，以获取服务器的最新状态，从而达到推送的目标。
而HTTP是建立在TCP传输协议上的，HTTP在通信之前，需要先进行三次握手，然后再是发送消息，最后进行四次挥手断开连接。
如果用HTTP去实现推送技术会造成过多带宽的消耗，


在 WebSocket API 中，浏览器和服务器只需要完成一次握手，两者之间就直接可以创建持久性的连接，并进行双向数据传输。





https://www.cnblogs.com/fuqiang88/p/5956363.html


https://www.cnblogs.com/merray/p/7918977.html

https://blog.csdn.net/sinat_31057219/article/details/72872359