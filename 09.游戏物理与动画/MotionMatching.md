

# 离线资源

bvh、fbx 数据

- 提取 Pose DataBase
- 提取 Feature DataBase，从 Pose DataBase 中提取；

# 运行时

- 初始化构建数据，将离线处理好的数据，PoseDataBase、FeatureDataBase数据进行构建, FeatureDataBase 是来自于 PoseDataBase；
- 运行时，有动画数据的帧号记录，通过帧号，提取当前帧号 PoseFeature 的数据，然后接收玩家的数据，得到预测的 TrajectoryFeature；
- 将 PoseFeature 、TrajectoryFeature ，组成 Query Vector；
- 从 Feature DataBase 中，查找到最合适的帧号，期间可以通过 bvh 文件来加速查找；
- 用上一步查找到的帧号，取 PoseSet中的目标 Pose 数据；
- 将 Pose 数据，通过 Retarget 的方式，应用到角色身上；