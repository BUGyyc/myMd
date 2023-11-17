
# Env

URP  13.0.0

---

# 设置相关

## Depth Texture 

这个属于深度，通常是需要的？

## Opaque Texture

这个是在不透明的对象之后，截取不透明对象的图像，通过用来做毛玻璃、水面折射的效果。如果不需要，不用开启。

## Opaque Downsampling

和不透明的图像截取相关

## Terrain Holes

地形的山洞，如果没有做山洞的情况，可以关闭。这样可以减少一部分 Shader 变体，减少 Build 的时间。


# 品质设置相关

## HDR

建议低端机不要开启

## MSAA

抗锯齿


## Render Scale 

默认是1，数字越小性能越好，数字越大效果越好




---

# 性能相关




---



<https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@13.0/manual/universalrp-asset.html>