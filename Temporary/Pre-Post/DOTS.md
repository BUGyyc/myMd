# 实测数据

当前机器配置：

- CPU - (英特尔)Intel(R) Core(TM) i7-9700 CPU @ 3.00GHz(3000 MHz)
- 内存 - 32.00 GB (2400 MHz)
- 显卡 - NVIDIA GeForce RTX 2060

实测以下情况，期望 60 FPS以上，有部分情况挑战较高

- 100万 个 Entity 处理纯数据、无实体显示。实测结果保持在200FPS以上

    ![](../../pic.res/2022-03-08-15-54-08.png)

- 同屏 10 万 个 Entity (Unity Cube 模型) 移动旋转
- 同屏 10 万 个 Entity (带 Animator、Mesh)，LOD处理等
- 5 万 个 Entity (带 Animator、Mesh) 及数据处理、LOD处理

---

## DOTS化

- 对AC项目下，用的是Entitas会哪些已知的问题（Entitas Git：）
  - AC项目中对Entitas的使用，怀疑有一定的误区：
    - 每一个Entity(LogicEntity 或 ViewEntity)都是和一个GameObject对应的，
      单纯从ECS设计上来说，这不是纯粹的Entity，这个造成了GameObject的浪费，
   当然，Editor下有GameObject更加方便调试，另外从数量上来说，客户端是服务器的两倍；
- 依据官方DOTS中的描述，托管类型的数据，不支持Chunk Memory。
  那么首先需要确定的是 AC 中 ProtocolBuf 是否可以导出 Struct 类型，
  游戏中ComponentData 的大头也是在Pb中集中，如果导出 Struct 类型可行，
  需要留意以下问题：
  - 是否会对当前增量更新的方案有影响
- DOTS化后有些需要解决的问题：
  - Timeline 要有关联目标，Hybrid ComponentData 是否支持
  - Unity 自带的CharacterController，当然先考虑一下，
   是否所有的NPC都需要CharacterController来控制移动，
   理论上是不需要那么多的，特别是单一移动的NPC
  - Animator 也是在大量NPC场景下的重点，数量大，本身消耗也大

---

Entitas 使用规则
<https://github.com/mzaks/EntitasCookBook>

---

当前工程内的问题：

- Entity 不够纯粹，是以GameObject为载体，没用上真正的ECS思想
- Timeline Execute 与 System Execute 目前没有确定一个执行顺序
- 游戏内的数据不支持可重入，无法掉线重连后完整恢复游戏
- 业务上的 EntityType 有部分Type划分比较模糊，不清晰，需要达成一致
- Entity 的 Disable下，依然可以被遍历到，引发一些空指针问题，需要排查一下 Entity

当前游戏内的性能瓶颈主要集中在，现世场景下大量NPC的模拟。这也是 DOTS 的接入需要直接解决的问题，
整个接入大致分为两部分，数据层面和渲染层面上的DOTS化。

数据层面：

- 真正意义上的 Entity，解决当前Entitas框架下，Entity挂载于GameObject 下的问题；
- 大量NPC的 Component 是相似的，很适合 Archetype 的划分，生成Chunk Memory；
- 可以协助解决 Timeline Execute 与 System Execute 的确定性顺序，保障掉线重连的底层支持；

数据层面会遇到的问题：

- 在业务上，Entitas 与 DOTS 下的 Entities 代码上差别较大，需要时间整理，Entities代码要求严谨；
- StateMsg 是当前最大的数据块，需要把 Pb 的导出，尽可能导出为 Struct 型，这样才能把 Chunk 使用起来；

渲染层面：

- Hybrid Render V2;
- DOTS 下的LOD;
- DOTS 下的 SubScene，有助于解决大场景资源加卸载的问题;

渲染层面遇到的问题：

- 目前官方给出的Animator解决方案是Preview 版本，不建议接入这个模块，还不够稳定。

---

DOTS 有部分模块是Preview版本，Preview 模块是不建议接入

---

- Create an entity with no components and then add components to it. (You can add components immediately or when additional components are needed.)

![](../../pic.res/2022-03-03-16-21-33.png)

![](../../pic.res/2022-03-03-16-22-17.png)

![](../../pic.res/2022-03-03-16-24-17.png)

# Entity

## Define Entity Query

```ini
EntityQuery query

    = GetEntityQuery(typeof(RotationQuaternion),

                     ComponentType.ReadOnly<RotationSpeed>());
```

# Components

![](../../pic.res/2022-03-03-17-00-33.png)

![](../../pic.res/2022-03-03-17-04-56.png)

IComponentData structs must not contain references to managed objects. This is because ComponentData lives in simple non-garbage-collected tracked Chunk memory, which has many performance advantages.

## IComponentData

## Managed IComponentData

![](../../pic.res/2022-03-03-20-28-29.png)

![](../../pic.res/2022-03-03-20-32-55.png)

## Hybrid ComponentData

![](../../pic.res/2022-03-03-20-37-56.png)

## Shared ComponentData

Shared components allow your systems to process like entities together. For example, the shared component Rendering.RenderMesh, which is part of the Hybrid.rendering package, defines several fields, including mesh, material, and receiveShadows. When your application renders, it is most efficient to process all of the 3D objects that have the same value for those fields together. Because a shared component specifies these properties, the EntityManager places the matching entities together in memory so that the rendering system can efficiently iterate over them.

If you over-use shared components, it might lead to poor chunk utilization. This is because when you use a shared component it involves a combinatorial expansion of the number of memory chunks based on archetype and every unique value of each shared component field. As such, avoid adding any fields that aren't needed to sort entities into a category to a shared component. To view chunk utilization, use the Entity Debugger.

![](../../pic.res/2022-03-04-11-06-33.png)

## System State ComponentData

## Dynamic Buffer ComponentData

## Chunk ComponentData

# System

You can view the system configuration using the Entity Debugger window (menu: Window > Analysis > Entity Debugger).

![](../../pic.res/2022-03-04-12-36-14.png)

## initializationSystem

![](../../pic.res/2022-03-04-16-06-59.png)

## SimulationSystem

需要确认一下Timeline 的 Update 是否在这个System 之前

![](../../pic.res/2022-03-04-16-07-58.png)

## PresentationSystem

![](../../pic.res/2022-03-04-16-08-46.png)

## Job调度的有序控制

## 数据查询

### Query

### Job

## EntityCommandBuffer

![](../../pic.res/2022-03-04-16-46-13.png)

# DOTS + LOD + Hybrid Render V2

1000 个 GameObject 转化为32 万个Entity

![](../../pic.res/2022-03-08-12-02-46.png)

---

- Write Group ：为了标记一些情况，例如方便搜索时过滤一部分携带WriteGroup的数据
- Serializeable Struct :
- GameObjectConversionSystem
- ConverterVersion：
<https://docs.unity3d.com/Packages/com.unity.entities@0.17/api/Unity.Entities.ConverterVersionAttribute.-ctor.html>

- IConvertGameObjectToEntity  
- Convert
- BurstCompile  
- IJobParallelFor
- IDeclareReferencedPrefabs  
- NativeArray

---

SubScene 转化 ECS 的基础

# GameObject Convert To ECS

![](../../pic.res/2022-03-03-18-36-37.png)

Editor 下也可以用 DOTS

For example, it creates an Editor world for entities and systems that run only in the Editor, not in playmode and also creates conversion worlds for managing the conversion of GameObjects to entities. See WorldFlags for examples of different types of worlds that can be created

Your code is then responsible for creating any needed worlds, as well as instantiating and updating systems. You can use the Unity scriptable PlayerLoop to modify the normal Unity player loop so that your systems are updated when required.

# Blob Asset To Share ComponentData

# Entity Debugger

![](../../pic.res/2022-03-04-18-37-04.png)

<https://docs.unity3d.com/Packages/com.unity.entities@0.9/manual/ecs_debugging.html>

<https://www.youtube.com/watch?v=0y05nw5zET0>

<https://docs.unity3d.com/2020.1/Documentation/ScriptReference/LowLevel.PlayerLoop.html>

# SubScene 加载大场景的方案

To Create a Sub Scene

- In the Unity Hierarchy window, right-click on empty space, or on a GameObject that you want to create the Sub Scene next to.

- Select New Sub Scene > Empty Scene... in the context menu. Unity then creates an empty Sub Scene and creates a corresponding Scene Asset file in your project.

---

# DOTS 与 传统方式对比

10000 个 Prefab 创建

![](../../pic.res/2022-03-07-11-09-04.png)

10000 个 带 SkinMeshRender Prefab

下图 转化出了66 万个 Entity

![](../../pic.res/2022-03-07-14-42-55.png)

计算压力主要集中在MeshRender上，也就是DOTS 内 最后一个System Group 内，和渲染相关性很大，加上LOD估计可以减缓

![](../../pic.res/2022-03-07-14-47-01.png)

---

- GameObject To Entity:
    <https://docs.unity3d.com/Packages/com.unity.entities@0.17/api/Unity.Entities.IConvertGameObjectToEntity.html>

- Attr GenerateAuthoringComponent : 将ComponentData 暴露在 Inspector 上,即使是Editor下也可行

-

---

# DOTS 案例

![](../../pic.res/2022-03-07-20-23-34.png)

<https://developer.unity.cn/projects/5e607e06edbc2a2000accf83>

---

<https://docs.unity3d.com/Packages/com.unity.entities@0.17/manual/ecs_components.html>

<https://docs.unity3d.com/Packages/com.unity.entities@0.17/manual/index.html>
