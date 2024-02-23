
- [BTree 内 Running 实现方式](#btree-内-running-实现方式)
- [Motion Matching 内的 Min、Max](#motion-matching-内的-minmax)
- [PBD 实现下的布料，如果同时受力，应该是怎么样的](#pbd-实现下的布料如果同时受力应该是怎么样的)
- [传球技能筛选的流程，应该怎么做](#传球技能筛选的流程应该怎么做)
- [二维世界下，圆与矩形的物理碰撞模拟](#二维世界下圆与矩形的物理碰撞模拟)




## BTree 内 Running 实现方式

大概的思路如下代码：

```C#


BTState ExecuteSequence(List<BTNode> SubNodeList)
{
    while(true)
    {
        var curNode = SubNodeList[curIndex];
        //运行并返回一个状态（running、fail、success等）
        var state = curNode.Execute();

        if(state == BTState.Fail)
        {
            //失败了，中断 Sequence
            return state;
        }

        if(state == BTState.Running)
        {
            //运行中的结果
            return state;
        }

        //成功了，所以要继续累计索引，推进到下一个节点
        curIndex++;
            //防止越界
        if(curIndex >= SubNodeList.Count)
        {
            return BTState.Success;
        }
        state = BTState.Running;
    }
}


var state = executeBT();

//这里的 trigger 是为了
while(state == BTState.Running  && trigger == true)
{
    trigger = false;
    state = executeBT();
}


```


## Motion Matching 内的 Min、Max



## PBD 实现下的布料，如果同时受力，应该是怎么样的



## 传球技能筛选的流程，应该怎么做



## 二维世界下，圆与矩形的物理碰撞模拟