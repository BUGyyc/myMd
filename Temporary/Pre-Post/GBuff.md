G-Buffer，全称Geometric Buffer ，译作几何缓冲区，它主要用于存储每个像素对应的位置（Position），法线（Normal），漫反射颜色（Diffuse Color）以及其他有用材质参数。根据这些信息，就可以在像空间（二维空间）中对每个像素进行光照处理。G-Buffer根据需求可以存储不同的内容，以下为一个典型的G-Buffer layout，对于Unity中的Deferred Rendering，G-Buffer layout就与之下的不同，关于Unity的部分之后会再细说。


https://blog.csdn.net/yinfourever/article/details/90263638


