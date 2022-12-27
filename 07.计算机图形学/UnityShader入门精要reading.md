

---

# point

BRDF：

双向反射分布函数。

表示在 确定入射的光照方向和辐射度后，可以通过BRDF 计算某个区域的特定出射方向的光照能量分布。



//??   BRDF = 辐射率 / 辐照度


//?? 辐照度 应该是指 一块区域的光照能量总和 ，可以通过微积分算出能量总和
//?? 辐射率 应该是指 指定的观察方向上的能量分布。


?? 为什么 BRDF 定义的实际值是单位为 sr-1 的量

<https://www.zhihu.com/question/28476602/answer/41003204>


各项同性：

当固定视角和光源方向时，旋转物体表面，反射没有发生任何变化。

各项异性：

当固定视角和光源方向时，旋转物体表面，反射有发生变化。例如：金属材质



SV_Position 与  SV_Target

//?? **区别是 SV_POSTION一旦被作为vertex函数的输出语义，那么这个最终的顶点位置就被固定了，不得改变。**

<https://zhuanlan.zhihu.com/p/113237579>



纹理变量  加后缀 “_ST”

其实就是给纹理增加两个属性 ： Offset 和  Tile


双面 半透明 效果

双Pass ,分别渲染 背面 和 正面。叠加出 双面 半透明



延迟渲染：

双 Pass.
第一个 Pass: 判断图元是否可见，若可见，写入 G-Buff
第二个 Pass: 通过 G-buff下的信息，进行最后的光照计算。


GBuff 主要包含： 颜色缓冲，深度缓冲，法线缓冲



延迟渲染 如何处理 半透明

深度剥离算法，消耗更多显存，做到叠加颜色，最后混合结果。



屏幕空间阴影映射



屏幕空间反射

<https://blog.csdn.net/qjh5606/article/details/120102582>

---

<https://www.cnblogs.com/herenzhiming/articles/5789043.html>

<https://www.zhihu.com/question/28476602/answer/41003204>