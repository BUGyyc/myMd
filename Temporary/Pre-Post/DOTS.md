

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




# GameObject Convert To ECS

![](../../pic.res/2022-03-03-18-36-37.png)





Editor 下也可以用 DOTS

For example, it creates an Editor world for entities and systems that run only in the Editor, not in playmode and also creates conversion worlds for managing the conversion of GameObjects to entities. See WorldFlags for examples of different types of worlds that can be created


Your code is then responsible for creating any needed worlds, as well as instantiating and updating systems. You can use the Unity scriptable PlayerLoop to modify the normal Unity player loop so that your systems are updated when required.

https://docs.unity3d.com/2020.1/Documentation/ScriptReference/LowLevel.PlayerLoop.html



---


https://docs.unity3d.com/Packages/com.unity.entities@0.17/manual/ecs_components.html