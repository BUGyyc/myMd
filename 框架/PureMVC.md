/*
 * @Author: delevin.ying 
 * @Date: 2019-03-12 09:37:46 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-03-12 09:47:36
 */
 # PureMVC


- 在 MVC 模式中，应用程序被分为低耦合的三层：Model、View 和 Controller。PureMVC 在此基础上有所扩充，通过模块化设计，致力于进一步降低模块间耦合性，创建了一个易于扩展限制很小的通用框架，图一是 PureMVC 的设计图。
- Proxy 对象负责操作数据模型，与远程服务通信存取数据，这样可以保证 Model 层的可移植性。通常 Proxy 对象的引用保存在 Model 中。


PureMVC

-Mediator 与 View
-Proxy 与 Model
-Command 与 Controller

Facade 单例直接管理 View、Model、Controller
Facade 也可直接管理所有的Mediator、Proxy、Command

传统的MVC模式下，Controller一般是最臃肿的

https://blog.csdn.net/qq_29579137/article/details/73692842
