




https://www.cocos.com/4388

# 前言

和其他游戏开发思路一样，对小游戏的优化也主要围绕下面三个点

- CPU方面优化
- GPU方面优化
- 内存优化

---

# CPU方面优化

- 一帧内执行过多操作，导致这一帧压力过大，通常采取分帧
三消游戏一帧内执行过多计算、播放过多特效，在低配设备上，必然会出现卡顿掉帧的问题。
多个特效在多个帧中分别开始播放，减少聚集在同一帧导致的运行压力


- 避免重复，重复的查找与重复的计算等操作都是多余的消耗。

例如在Unity中，update中常用到的对象，如：GetComponent<XXX>()，我们希望它在update执行之前就已经获取到，而不需要每一帧都是获取。

例如


- 非必要逻辑，可以适当延迟执行，或者间隔一定时间去执行
如下NPC执行一些随机性的逻辑，非直接触发式
```
update() {
    ...
    //检测NPC是否对话
    this.step++;
    if (this.step > this.value) {
        this.onNpcCheckTalk();
        this.step = 0;
    }
    ...
    //检测NPC是否移动
    this.checkNpcState++;
    if (this.checkNpcState > this.checkValue) {
        this.onNpcWalk();
        this.checkNpcState = 0;
    }
},
```
上面代码展示的是放在update中执行检测，当然我们也可以通过计时器去控制，具体问题具体分析。
在Unity中通常不提倡写在update中，而是写在协程中。例如A*寻路的计算与移动逻辑分散到几帧中执行。
如下：

```
/// <summary>
/// 按路径移动过去
/// </summary>
/// <returns></returns>
IEnumerator TravelPath () {
    Vector3 a, b, c = pathToTravel[0].Position;
    //朝向
    yield return LookAt (pathToTravel[1].Position);
    if (!currentTravelLocation) {
        currentTravelLocation = pathToTravel[0];
    }
    Grid.DecreaseVisibility (currentTravelLocation, VisionRange);
    int currentColumn = currentTravelLocation.ColumnIndex;
    float t = Time.deltaTime * travelSpeed;
    //按照路径去移动
    for (int i = 1; i < pathToTravel.Count; i++) {
        currentTravelLocation = pathToTravel[i];
        a = c;
        b = pathToTravel[i - 1].Position;
        int nextColumn = currentTravelLocation.ColumnIndex;
        if (currentColumn != nextColumn) {
            if (nextColumn < currentColumn - 1) {
                a.x -= HexMetrics.innerDiameter * HexMetrics.wrapSize;
                b.x -= HexMetrics.innerDiameter * HexMetrics.wrapSize;
            } else if (nextColumn > currentColumn + 1) {
                a.x += HexMetrics.innerDiameter * HexMetrics.wrapSize;
                b.x += HexMetrics.innerDiameter * HexMetrics.wrapSize;
            }
            Grid.MakeChildOfColumn (transform, nextColumn);
            currentColumn = nextColumn;
        }
        c = (b + currentTravelLocation.Position) * 0.5f;
        Grid.IncreaseVisibility (pathToTravel[i], VisionRange);
        for (; t < 1f; t += Time.deltaTime * travelSpeed) {
            transform.localPosition = Bezier.GetPoint (a, b, c, t);
            Vector3 d = Bezier.GetDerivative (a, b, c, t);
            d.y = 0f;
            transform.localRotation = Quaternion.LookRotation (d);
            yield return null;
        }
        Grid.DecreaseVisibility (pathToTravel[i], VisionRange);
        t -= 1f;
    }
    currentTravelLocation = null;
    a = c;
    b = location.Position;
    c = b;
    Grid.IncreaseVisibility (location, VisionRange);
    for (; t < 1f; t += Time.deltaTime * travelSpeed) {
        transform.localPosition = Bezier.GetPoint (a, b, c, t);
        Vector3 d = Bezier.GetDerivative (a, b, c, t);
        d.y = 0f;
        transform.localRotation = Quaternion.LookRotation (d);
        yield return null;
    }
    transform.localPosition = location.Position;
    orientation = transform.localRotation.eulerAngles.y;
    ListPool<HexCell>.Add (pathToTravel);
    pathToTravel = null;
    //移动完成，返回默认状态动画
    Common.getInstance ().dispatchEvent (new CEvent (CEventName.HERO_MOVE_COMPLETE));
    this.GetComponent<HeroBehaviours> ().SetAnimatorState ("isIdle");
}
```

- 减少频繁的创建与销毁操作，对象池
如下，抓到大金矿后，爆出多个金币，就是用到了对象池

```
/**
* 爆金币动画
*/
onMoneyCoinAni(startPos, num) {
    for (let i = 0; i < num; i++) {
        //从对象池中取
        let coin = this.onCreateOneCoinByPool();

        ...

        let bezier = [startPos, targetPos1, targetPos2];
        let time = Math.random() * 0.5;
        let pos = cc.v2(this.boomNode.position.x, this.pushBoom.position.y);
        coin.runAction(
            cc.sequence(
                cc.bezierTo(0.2 + time, bezier),
                cc.delayTime(0.1),
                cc.moveTo(0.2, pos),
                cc.callFunc(() => {
                    //最后回收到对象池
                    this.coinPool.put(coin);
                })
            )
        );
    }
},
```
# GPU方面优化

- 图集合并

creator有打包图集的流程

https://docs.cocos.com/creator/manual/zh/asset-workflow/auto-atlas.html

- 防止交叉渲染
相邻节点的所用的贴图应该尽可能来自同一个图集

例如jz1、jz2使用图集1，claw1、claw2使用图集2，在绘制jz1后，准备绘制claw1,原本使用的图集1将无法运用于claw1,因为claw1使用的是图集2。
最后DrawCall将是4次。

如果将顺序修改，如下所示


DrawCall将降低到2次
  

# 内存方面

- 资源加载时机选择
- 资源卸载时机选择


- 压缩资源



---
https://cloud.tencent.com/developer/article/1336507


https://www.cnblogs.com/bigfishzhou/p/5885997.html

https://developers.weixin.qq.com/community/develop/article/doc/00008edebb42f853622814fb356c13

https://www.jianshu.com/p/d4fb22509eb9

https://www.jianshu.com/p/56d2b215a48f