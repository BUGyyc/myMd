import taichi as ti
from random import random
from tqdm import tqdm
from math import sin, pi, sqrt

################
# Support Functions
################
def assignArray2Tensor(t, i, a):
    for j in range(len(a)):
        t[i][j] = a[j]

def diffDotProd(a, b):
    res = 0
    for i in range(3):
        res += (a[i]-b[i]) * (a[i]-b[i])
    return sqrt(res)

################
# Init taichi
################
ti.init(arch=ti.gpu)

################
# Input data
################
def Calx0(t):
    loopInt = 4 # s # 2s for a loop
    x = sin(t/loopInt*2*pi)
    y = sin(t/loopInt*2*pi)
    return [x, y, 0]
n = 5
lConst = 0.1
xInitRel = [[0, -lConst*i, 0] for i in range(5)]
gravityConstant = 9.8
timestep = 0.01


################
# Taichi Tensors
################
x = ti.Vector(3, dt=ti.f32, shape=(n))
v = ti.Vector(3, dt=ti.f32, shape=(n))
vPer = ti.Vector(3, dt=ti.f32, shape=(n))
xPer = ti.Vector(3, dt=ti.f32, shape=(n))
a = ti.Vector(3, dt=ti.f32, shape=(n))
l = ti.field(ti.f32, shape=(n))


################
# Kernels
################
@ti.kernel
def CalvPer():
    for i in v:
        vPer[i] = v[i] + a[i] * timestep

@ti.kernel
def CalxPer_rest():
    for i in x:
        xPer[i] = x[i] + timestep/2 * (v[i] + vPer[i])

@ti.kernel
def Updatev():
    for i in v:
        v[i] = 2/timestep * (xPer[i]-x[i]) - v[i]
    
@ti.kernel
def Updatex():
    for i in x:
        x[i] = xPer[i]


################
# Time Loop Functions
################
def CalxPer(t):
    CalxPer_rest()
    xPer[0] = Calx0(t)

def CalPer(t):
    CalvPer()
    CalxPer(t)

def SolveConstraints():
    ITER_NUM = 2
    for i in range(ITER_NUM):
        # C0
        k = l[0]/diffDotProd(xPer[1], xPer[0])-1
        for u in range(3):
            xPer[1][u] += k * (xPer[1][u]-xPer[0][u])
        # Cj
        for j in range(1, n-1):
            k = 0.5 * (l[j]/diffDotProd(xPer[j+1], xPer[j])-1)
            tmp = [0, 0, 0]
            for u in range(3):
                tmp[u] = k * (xPer[j][u]-xPer[j+1][u])
            for u in range(3):
                xPer[j+1][u] += k * (xPer[j+1][u]-xPer[j][u])
            for u in range(3):
                xPer[j][u] += tmp[u]

def Update():
    Updatev()
    Updatex()

def TimeTick(t):
    CalPer(t)
    SolveConstraints()
    Update()

################
# Init tensors
################
x0 = Calx0(0)
assignArray2Tensor(x, 0, Calx0(0))
for i in range(1, n):
    xi = [0, 0, 0]
    for u in range(3):
        xi[u] = x0[u] + xInitRel[i][u]
    assignArray2Tensor(x, i, xi)

for i in range(n):
    assignArray2Tensor(v, i, [0,0,0])

assignArray2Tensor(a, 0, [0,0,0])
for i in range(1, n):
    assignArray2Tensor(a, i, [0, -gravityConstant, 0])

for i in range(n):
    l[i] = lConst

################
# GUI Settings
################
videoManager = ti.VideoManager(output_dir="./video", framerate=24, automatic_build=False)
gui = ti.GUI(background_color=0x222222)
colors = [0xFF0000, 0x00FFFF, 0x0000FF, 0xFFFF00, 0xFF00FF, 0x00FF00]
def world2screen(x, y):
    L = 1.5
    x = (x-(-L))/(2*L)
    y = (y-(-L))/(2*L)
    return (x,y)

show_on_screen = False

seconds = 5
fps = 24
if show_on_screen:
    fps = 60
timestepsPerFrame = int(1.0/fps/timestep)
totalFrame = seconds*fps
if show_on_screen:
    totalFrame = 1000000


################
# Main Loop
################
for frameCount in tqdm(range(totalFrame)):
    print(x)
    for t in range(timestepsPerFrame):
        TimeTick(timestep*t + frameCount/fps)

    for i in range(n):
        gui.circle(world2screen(x[i][0],x[i][1]), color=colors[i>0], radius=5)

    if show_on_screen:
        gui.show()
    else:
        img = gui.get_image()
        videoManager.write_frame(img)
        gui.clear()
if not show_on_screen:
    videoManager.make_video(gif=True, mp4=False)