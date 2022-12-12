


双buffer方案在“一写多读”的场景下能够实现lock-free的目标，那么对于“多写一读”或者“多写多读”场景，是否也能够满足呢？

不太合适。比较适用于一写多读的情况。


<https://mp.weixin.qq.com/s/3MDkpEHZECopSChBvTvXiQ>