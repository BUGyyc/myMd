# 前言

在构建游戏内寻路相关功能的时候，往往都是采用 Nav 或者 构建地图+A星的方式

## Nav 寻路方式


### Nav 可走区域的构建
场景模型体素化（Voxelization），或者叫“栅格化”（Rasterization）。
过滤出可行走面（Walkable Suface）
生成 Region
生成 Contour（边缘）
生成 Poly Mesh
生成 Detailed Mesh


### 关于漏斗算法

漏斗算法


## 自定义图结构 + A星



编辑路点图：

![image-20200914194537416](https://raw.githubusercontent.com/BUGyyc/myMd/master/%23.res/pic/nav9.gif)


点编辑：

一般是不需要编辑的，都是在场景内操作动态生成的（例如：Id自增）
![image-20200914194537416](https://raw.githubusercontent.com/BUGyyc/myMd/master/%23.res/pic/nav10.gif)

线编辑：

权重表示这条线路的代价，单向表示这条线路单方向有代价，反方向高代价，这是为了实现单向图或者是部分单向路径

![image-20200914194537416](https://raw.githubusercontent.com/BUGyyc/myMd/master/%23.res/pic/nav11.gif)


单向图：

https://www.zhihu.com/zvideo/1339900170771976192


无向图:



































路点系统 + A 星算法

运行实例1：

![image-20200914194537416](https://raw.githubusercontent.com/BUGyyc/myMd/master/%23.res/pic/nav8.gif)



---

https://zhuanlan.zhihu.com/p/35100455