


# Engine Render Update


```
@ PlayerLoop

    CALL_UPDATE_MODULAR(EarlyUpdate, GpuTimestamp);
    ...
    //TODO：输入的相应比较及时
    CALL_UPDATE_MODULAR(EarlyUpdate, UpdateInputManager);
    ...
    //TODO： 图集操作？？？
    CALL_UPDATE_MODULAR(EarlyUpdate, SpriteAtlasManagerUpdate);
    ...
    CALL_UPDATE_MODULAR(Update, ScriptRunBehaviourUpdate);
    CALL_UPDATE_MODULAR(Update, ScriptRunDelayedDynamicFrameRate);
    //TODO：理论上，从这个时序可以得出 Timeline 的 Tick 是的 Mono Tick 之后，通常 Timeline 是靠 PlayableDirector 驱动的
    CALL_UPDATE_MODULAR(Update, DirectorUpdate);

```

