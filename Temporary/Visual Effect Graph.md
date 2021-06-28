





# VFX 与 Particle System

![](Visual%20Effect%20Graph.assets/image-20210628143705960.png)

# Visual Effect Graph

![](Visual%20Effect%20Graph.assets/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20210628143403.png)

## Visual Effect Graph 编辑入门


### Systems

通常来说System包含一个Effect的初始化、更新、输出显示等流程


![img](Visual%20Effect%20Graph.assets/SystemDrawBox.png)

- Spawn 产生粒子的源头，可以设置循环模式、延迟时间等
- Initialize Particle 初始化粒子，设置尺寸、出生点、颜色、生命周期等
- Update Particle 按照 Visual Effect Graph Asset设置的Update Mode类型，每帧执行，通常可以进行改变坐标、方向、颜色、尺寸、力作用等
- Output 输出的实际效果，一个System可以有多个Output



### Event

VFX 允许外部通过事件传递信息给VFX内部

![image-20210628155207012](Visual%20Effect%20Graph.assets/image-20210628155207012.png)

```
    VisualEffect vf = this.GetComponent<VisualEffect>();
    vf?.SendEvent("SetBlue");
```


## VFX 实例

如下，以固定速率生成粒子，每秒100000

![image-20210628160120408](Visual%20Effect%20Graph.assets/image-20210628160120408.png)

设定一定数量的模拟粒子，初始一个随机速度、随机生命，出生点设定在一个球面范围

![image-20210628160327938](Visual%20Effect%20Graph.assets/image-20210628160327938.png)



改变坐标

![image-20210628161440665](Visual%20Effect%20Graph.assets/image-20210628161440665.png)

输出显示，设定粒子朝向相机横截面、设定粒子尺寸、颜色等

![image-20210628161818938](Visual%20Effect%20Graph.assets/image-20210628161818938.png)




## VFX  new Features


### Output Event

即上文提到的外部发送Event给VFX

![img](Visual%20Effect%20Graph.assets/banner-cpu-output-event.png)

### LOD 

使用LOD优化Mesh粒子

![img](Visual%20Effect%20Graph.assets/banner-space-scene-2.png)

### Static mesh sampling

静态网格采样

![img](Visual%20Effect%20Graph.assets/banner-mesh-sampling.png)






---

https://zhuanlan.zhihu.com/p/61570237

https://docs.unity3d.com/Packages/com.unity.visualeffectgraph@10.4/manual/index.html

https://blog.unity.com/technology/creating-explosive-visuals-with-the-visual-effect-graph