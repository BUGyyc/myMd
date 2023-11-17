

# 基础原理

使用 GPU 来承担更多的剔除和运算工作


# 执行流程


## Visibility Pass

对场景进行光栅化，将 PrimitiveID 和 MaterialID 保存在 visibility buffer 中


## WorkList Pass

构建 WorkList, 将屏幕划分成多个 tile，根据用到某个 Material ID 的 Tile ...

## Shading Pass

拿到几何和材质信息，对表面进行着色



# GPU Culling

Cluster 剔除，多个三角面组合为一个 Cluster