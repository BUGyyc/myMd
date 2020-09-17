

# Entity

Entity的意义是ID，它是身份表示

# ComponentData

ComponentData 是数据集合，它作为组合，存在于Entity上

# System

System 是逻辑执行的主要区域

# 为什么ECS值得推崇

- 命中率更高，Chunk管理，同类型Data在同一个Chunk内申请。这使得System进行同type逻辑时，内存紧凑，使得缓存命中率极高。
- 组合更优于继承。
- 适合做网络同步，数据的回滚其实就是直接回滚ComponentData








---

https://developer.aliyun.com/article/248256

https://zhuanlan.zhihu.com/p/64378775