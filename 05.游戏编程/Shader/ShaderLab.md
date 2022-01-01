POSITION 与 SV_POSITION



> SV_POSITION：SV_前缀的变量代表system value，在DX10以后的语义绑定中被使用代表特殊的意义，和POSITION用法并无不同。
>
> 唯一区别是 SV_POSTION一旦被作为vertex shader的输出语义，那么这个最终的顶点位置就被固定了(不能tensellate，不能再被后续改变它的空间位置？)，已经成为了转换裁剪世界的坐标，是可以直接用来进入光栅化处理的坐标，如果作为fragment shader的输入语义那么和POSITION是一样的，代表着每个像素点在屏幕上的位置（这个说法其实并不准确，事实是fragment 在 view space空间中的位置，但直观的感受是如括号之前所述一般） 
>
> 其次：在DX10版本之前没有引入SV_的预定义语义，POSITION被用作vertex shader的输入，输出，fragment shader的输入参数。但DX10之后就推荐使用SV_POSITION作为vertex shader的输出和fragment shader的输入了，注意vertex shader的输入还是使用POSITION！切记。但是DX10以后的代码依旧兼容POSITION作为全程表达，估计编译器会自动判断并替换的吧。好了SV_POSITION的疑惑就此解开。



UnityObjectToClipPos