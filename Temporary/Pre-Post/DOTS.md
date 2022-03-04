

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



# System


# GameObject Convert To ECS

![](../../pic.res/2022-03-03-18-36-37.png)





Editor 下也可以用 DOTS

For example, it creates an Editor world for entities and systems that run only in the Editor, not in playmode and also creates conversion worlds for managing the conversion of GameObjects to entities. See WorldFlags for examples of different types of worlds that can be created


Your code is then responsible for creating any needed worlds, as well as instantiating and updating systems. You can use the Unity scriptable PlayerLoop to modify the normal Unity player loop so that your systems are updated when required.

https://docs.unity3d.com/2020.1/Documentation/ScriptReference/LowLevel.PlayerLoop.html



---


https://docs.unity3d.com/Packages/com.unity.entities@0.17/manual/ecs_components.html