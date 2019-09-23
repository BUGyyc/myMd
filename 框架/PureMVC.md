
 # PureMVC


- 在 MVC 模式中，应用程序被分为低耦合的三层：Model、View 和 Controller。PureMVC 在此基础上有所扩充，通过模块化设计，致力于进一步降低模块间耦合性，创建了一个易于扩展限制很小的通用框架，图一是 PureMVC 的设计图。
- Proxy 对象负责操作数据模型，与远程服务通信存取数据，这样可以保证 Model 层的可移植性。通常 Proxy 对象的引用保存在 Model 中。

![(path/to/image.png)](https://raw.githubusercontent.com/BUGyyc/MyGallery/master/res/2340489-e2aad8c58ebc99ef.png)

# PureMVC结构划分

-Mediator 与 View
-Proxy 与 Model
-Command 与 Controller

Facade 单例直接管理 View、Model、Controller
Facade 也可直接管理所有的Mediator、Proxy、Command

传统的MVC模式下，Controller一般是最臃肿的

# MVC的缺点

    Controller：控制器，包含了项目的业务逻辑。但是也是被大家吐槽最多的一个，原因就是很多人，或者说大多数人，习惯于什么都往Controller里写，最后一个Controller超过1000行代码是司空见惯的事。所以关于传统MVC的第一个痛点就是，Controller过于臃肿。

    Model：模型，包含了项目的数据模型。MVC定义之初，Model是核心，旨在使得同一个Model可以被复用到多个项目或者被复用到同一个项目的不同模块之中。但是在实际项目中，Model还承载着纯Model层内部的运算的工作，但是运算部分会项目的不同而有所区别，因此与项目的适配反而成为了Model可复用的枷锁。所以关于传统MVC的第二个痛点就是，Model变得不可复用。

    View：视图，包含了项目所有的UI组件。视图本身没有什么好被大家诟病的，但是由于MVC中对于View和Controller界限的模糊界定造成了使用者在写代码的时候会觉得这部分代码放在View或者Controller里都可以的情况。例如事件的处理，组件的组合等。所以关于传统MVC的第三个痛点就是，View概念的模糊。

# PureMVC 解决MVC缺点的方式

## Controller 细分为Command

根据PureMVC的最佳实践，Controller实体不需要单独实现，且Controller内部将每一个操作分割为一个个Command，这从根本上解决了Controller越来越臃肿的问题，强制用户将Controller里每一个操作细粒度化，使得代码可读性更强，维护性更高。

## Proxy 负责域数据，DataObject负责数据模型

PureMVC中，与域相关的逻辑和接口由Proxy来负责，后续的添加和修改接口只在Proxy中完成。而DataObject是完全对业务进行数据建模而产生的数据模型，与业务没有丝毫的关系，因此也保证了高可移植性。

## ViewComponent 只关注UI，其余的交给 Mediator

PureMVC规定了ViewComponent只负责UI的绘制，而其他事情，包括事件的绑定统统交给Mediator来做。这也就避免了ViewComponent内部代码定义模糊，更不会和Controller的代码进行混淆。


# PureMVC结构

## Proxy 与 Model
Proxy（模式），提供了一个一个包装器或一个中介被客户端调用，从而达到去访问在场景背后的真实对象。Proxy模式可以方便的将操作转给真实对象，或者提供额外的逻辑。
在PureMVC中，Model保存了对Proxy对象的引用，Proxy去操作具体的数据模型（Data Object）。也就是说，Proxy管理Data Object以及对Data Object的访问。

## Mediator 与 View
Mediator（模式），定义了一种封装对象之间交互的中介。这种设计模式被认为是行为模式因为它可以改变模式的运行行为。
正如定义里所说，PureMVC中，View只关心UI，具体的对对象的操作由Mediator来管理，包括添加事件监听，发送或接受Notification，改变组件状态等。这也解决了视图与视图控制逻辑的分离。

## Command 与 Controller
Command（模式），是一种行为设计模式，这种模式下所有动作或者行为所需信息被封装到一个对象之内。Command模式解耦了发送者与接收者之间的联系。
在PureMVC中，Controller保存了所有Command的映射。Command是无状态且惰性的，只有在需要的时候才被创建。

## Facade
与传统MVC模式不用的是，PureMVC中对于Model，View，Controller的调用是基于Facade模式的。
Facade模式，对应了GoF中的Facade模式，是一种将复杂且庞大的内部实现暴露为一个简单接口的设计模式，例如对大型类库的封装。
在PureMVC中，Facade是与核心层（Model,View,Controller）进行通信的唯一接口，目的是简化开发复杂度。实际编码过程中，不需要手动实现这三类文件，Facade类在构造方法中已经包含了对这三类单例的构造。


# PureMVC 各层之间的交互

View层的Mediator可以和Model层的Proxy进行互相访问，但是PureMVC设计之初是希望只有View依赖于Model，反之不成立。也就是View可以知道Model层有什么，但是Model层不需要知道View的任何内容。Mediator访问数据可以直接通过Proxy来完成，但是如果要对Proxy具体的内容进行加工，必须要通过Controller的Command来完成，这有助于实现View和Model之间的松散耦合

如上文所说，Proxy最好不要直接调用Mediator来通知它请求完成，而是在异步取到数据之后，通过Notification来进行通知。Proxy只发送通知，不应该监听通知，因为Proxy属于Model层，不应该知道View层的状态变化。当然，Proxy应当对外提供数据变更的接口。

Command的实例化与执行只能由Controller来做。作为控制逻辑的执行体，Command有权拿到Proxy和Mediator的对象，并进行值加工，最后会将结果通过Notification发送给其它Command或者Mediator。

https://blog.csdn.net/qq_29579137/article/details/73692842

https://www.jianshu.com/p/47deaced9eb3
