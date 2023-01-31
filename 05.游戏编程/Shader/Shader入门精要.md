- [渲染管线简述](#渲染管线简述)
  - [应用阶段](#应用阶段)
  - [几何阶段](#几何阶段)
  - [光栅化阶段](#光栅化阶段)
- [Shader 编程基础](#shader-编程基础)
  - [HLSL 语义、语法](#hlsl-语义语法)
- [可见性算法](#可见性算法)
- [关于抗锯齿](#关于抗锯齿)
- [光照模型](#光照模型)
  - [高洛德着色](#高洛德着色)
  - [兰伯特着色](#兰伯特着色)
  - [Phong 着色](#phong-着色)
  - [Bling-Phong 着色](#bling-phong-着色)
- [辐射度量学](#辐射度量学)
- [GPU 与 CPU 架构](#gpu-与-cpu-架构)
- [着色管线](#着色管线)
- [Ref](#ref)


# 渲染管线简述

- 应用阶段
- 几何阶段
- 光栅化阶段

## 应用阶段

CPU向GPU发起渲染调用。

## 几何阶段

- 顶点着色器
- 曲面细分着色器
- 几何着色器
- 剪切
- 屏幕映射

## 光栅化阶段

- 三角形设置
- 三角形遍历
- 提前深度测试
- 片元着色器
- 深度测试
- 混合

# Shader 编程基础

## HLSL 语义、语法

# 可见性算法



# 关于抗锯齿



# 光照模型

## 高洛德着色

## 兰伯特着色

## Phong 着色

## Bling-Phong 着色


# 辐射度量学


# GPU 与 CPU 架构


# 着色管线








---

# Ref

![image-20220101130511819](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101130511819.png)

![image-20220101131732667](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101131732667.png)

![image-20220101140251584](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101140251584.png)

Standard Surface Shader

Unlit Shader

Image Effect Shader

Computer Shader



![image-20220101145446181](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101145446181.png)

![image-20220101145457960](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101145457960.png)

Pass

UsePass

GrabPass

![image-20220101150330811](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101150330811.png)

![image-20220101151406434](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101151406434.png)

![image-20220101151716199](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101151716199.png)

![image-20220101154525915](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101154525915.png)

![image-20220101154706655](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101154706655.png)

![image-20220101160821932](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101160821932.png)

![image-20220101160928020](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101160928020.png)

![image-20220101161454143](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101161454143.png)

![image-20220101161559078](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101161559078.png)





BRDF

https://www.sardinefish.com/blog/338



![image-20220101163539417](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101163539417.png)





兰伯特光照模型

![image-20220101170627417](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101170627417.png)





Texture  滤波模型

Filter Mode

Point

Bilinear

Trilinear

![image-20220101173818578](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101173818578.png)

![image-20220101174056488](Shader%E5%85%A5%E9%97%A8%E7%B2%BE%E8%A6%81.assets/image-20220101174056488.png)





双向分布函数BRDF



http://www.realtimerendering.com/

https://www.turiyaware.com/blog/unity-ui-blur-in-hdrp



https://cloud.tencent.com/developer/news/98239

http://www.codebaoku.com/it-csharp/it-csharp-215989.html

https://www.dbhc-doman.club/archives/255/game-performance-optimization/



https://juejin.cn/post/6844904038408912909
