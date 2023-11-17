

# Animation Instance

在离线环境下，把动画数据 Bake 下来。运行时，通过 Texture、和 SkinMeshRender 内的数据配合，让顶点着色器去运算顶点变化。

并且整个渲染对象，时通过 Graphic.DrawMeshInstance 的方式绘制的，是支持 GPU Instance 的形式。



按照多数动画的数据，SkinMeshRender 上的顶点，通常最多受四个骨骼影响，所以我们将 Mesh中顶点的 Color存储，四个骨骼的权重。

然后再将离线Bake好的 Texture，进行采样。

在顶点着色器中，我们通过预先定义好的数据结构，去采样 Texture，最后影响的坐标，作用到顶点上。




---

我们可以通过查看应用的 Shader，来思考 GPU-Animation-Instance 的实现步骤

# 离线步骤


- 将需要的骨骼索引信息、骨骼权重信息，全部序列化到byte文件中






# 运行时

## 初始化

- mesh 中的 vertex.color 要存储四个关节的权重，因为 Mesh 上的顶点，通常最多受四个 Joint 影响，所以 color 的四个通道够用
- mesh 中的 用其中一个 uv 存储每一帧动画的骨骼索引，如：v.texcoord2.x 、y、z、w

```
            Color[] colors = new Color[vertexCache.weight.Length];            
            for (int i = 0; i != colors.Length; ++i)
            {
                colors[i].r = vertexCache.weight[i].x;
                colors[i].g = vertexCache.weight[i].y;
                colors[i].b = vertexCache.weight[i].z;
                colors[i].a = vertexCache.weight[i].w;
            }
            //！把四个骨骼的权重也写入，用 mesh.Color 这个数据存储
            vertexCache.mesh.colors = colors;
```

然后索引

```
            List<Vector4> uv2 = new List<Vector4>(vertexCache.boneIndex.Length);
            for (int i = 0; i != vertexCache.boneIndex.Length; ++i)
            {
                uv2.Add(vertexCache.boneIndex[i]);
            }
            //！写入到 UV2 上
            vertexCache.mesh.SetUVs(2, uv2);

```



## 逐帧运算


MaterialPropertyBlock 的运用，MaterialPropertyBlock中只使用了：
- frameIndex，当前帧号
- preFrameIndex，下一个帧号
- transitionProgress，动画的过度进度












---

<https://gitee.com/yycBug/Animation-Instancing>