
- 多轨道时序控制
- 可扩展



---

结构

扩展自定义timeline



![image-20200904170836111](../#.res/pic/image-20200904170836111.png)

## Timeline编辑器扩展

### PlayableDirector（控制器）

- 创建一个Track的方式
  - PlayableDirector.playableAsset as TimelineAsset;
  - TimelineAsset.CreateTrack

### TrackAsset（自定义轨道类型）

- 接口
  - CreatePlayable //创建一个PlayableBehaviour时触发，有多少个片段触发多少次
  - CreateTrackMixer //至少有一个片段，创建轨道时触发，**已经获取所有的Clip**实例
- 类Attribute
  - [TrackClipType(typeof(MyPlayableAsset))] //轨道的信息资源
  - [TrackBindingType(typeof(GameObject))] //决定Track能放什么类型的参数
  - [TrackColor(0, 1, 0)] //给轨道定义颜色

### PlayableAsset（资源）

- API
  - ScriptPlayable.Create() //创建Playable
  - GetGameObjectBinding(director); //获得轨道绑定的game object

### PlayableBehaviour（行为）

- 接口
  - PrepareFrame //循环播放刷新时调用
  - OnBehaviourPlay //进入该行为片段时调用
  - OnBehaviourPause //退出该行为片段时调用
  - OnPlayableCreate //创建时、编辑器模式选中tiemline时
  - OnGraphStart //创建后、播放时
  - OnGraphStop //停止时，编辑器模式选中其他对象时
  - OnPlayableDestroy //同上，在停止后调用

### Playable

- API
  - playable.GetTime() //获取该片段当前的播放时间进度


Track 绑定 GameObject

  playableDirector.SetGenericBinding(timelineEventTrack, gameObject);





Playable




PlayableAsset

ScriptObject

PlayableBehaviour



  https://docs.unity3d.com/Packages/com.unity.timeline@1.4/api/UnityEngine.Timeline.TimelinePlayable.html