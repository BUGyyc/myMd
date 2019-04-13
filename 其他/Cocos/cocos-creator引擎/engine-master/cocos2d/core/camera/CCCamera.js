/****************************************************************************
 Copyright (c) 2013-2016 Chukong Technologies Inc.
 Copyright (c) 2017-2018 Xiamen Yaji Software Co., Ltd.

 https://www.cocos.com/

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated engine source code (the "Software"), a limited,
  worldwide, royalty-free, non-assignable, revocable and non-exclusive license
 to use Cocos Creator solely to develop games on your target platforms. You shall
  not use Cocos Creator software for developing other software or tools that's
  used for developing games. You are not granted to publish, distribute,
  sublicense, and/or sell copies of Cocos Creator.

 The software or tools in this License Agreement are licensed, not sold.
 Xiamen Yaji Software Co., Ltd. reserves all rights not expressly granted to you.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
 ****************************************************************************/

const AffineTrans = require('../utils/affine-transform');
const renderer = require('../renderer/index');
const RenderFlow = require('../renderer/render-flow');
const game = require('../CCGame');
const ray = require('../3d/geom-utils/ray');

import RendererCamera from '../../renderer/scene/camera';
import View from '../../renderer/core/view';

const mat4 = cc.vmath.mat4;
const vec2 = cc.vmath.vec2;
const vec3 = cc.vmath.vec3;

let _mat4_temp_1 = mat4.create();
let _mat4_temp_2 = mat4.create();

let _v3_temp_1 = vec3.create();
let _v3_temp_2 = vec3.create();
let _v3_temp_3 = vec3.create();

let _cameras = [];

let _debugCamera = null;

function repositionDebugCamera () {
    if (!_debugCamera) return;

    let node = _debugCamera.getNode();
    let canvas = cc.game.canvas;
    node.z = canvas.height / 1.1566;
    node.x = canvas.width / 2;
    node.y = canvas.height / 2;
}

/**
 * !#en Values for Camera.clearFlags, determining what to clear when rendering a Camera.
 * !#zh 摄像机清除标记位，决定摄像机渲染时会清除哪些状态
 * @enum Camera.ClearFlags
 */
let ClearFlags = cc.Enum({
    /**
     * @property COLOR
     */
    COLOR: 1,
    /**
     * @property DEPTH
     */
    DEPTH: 2,
    /**
     * @property STENCIL
     */
    STENCIL: 4,
});

let StageFlags = cc.Enum({
    OPAQUE: 1,
    TRANSPARENT: 2
});

/**
 * !#en
 * Camera is usefull when making reel game or other games which need scroll screen.
 * Using camera will be more efficient than moving node to scroll screen.
 * Camera 
 * !#zh
 * 摄像机在制作卷轴或是其他需要移动屏幕的游戏时比较有用，使用摄像机将会比移动节点来移动屏幕更加高效。
 * @class Camera
 * @extends Component
 */
let Camera = cc.Class({
    name: 'cc.Camera',
    extends: cc.Component,

    ctor () {
        if (game.renderType !== game.RENDER_TYPE_CANVAS) {
            let camera = new RendererCamera();

            camera.setStages([
                'opaque',
            ]);

            camera.dirty = true;

            this._inited = false;
            this._camera = camera;
        }
        else {
            this._inited = true;
        }
    },

    editor: CC_EDITOR && {
        menu: 'i18n:MAIN_MENU.component.others/Camera',
        inspector: 'packages://inspector/inspectors/comps/camera.js',
        executeInEditMode: true
    },

    properties: {
        _cullingMask: 0xffffffff,
        _clearFlags: ClearFlags.DEPTH | ClearFlags.STENCIL,
        _backgroundColor: cc.color(0, 0, 0, 255),
        _depth: 0,
        _zoomRatio: 1,
        _targetTexture: null,
        _fov: 60,
        _orthoSize: 10,
        _nearClip: 0.1,
        _farClip: 4096,
        _ortho: true,
        _rect: cc.rect(0, 0, 1, 1),
        _renderStages: 1,

        /**
         * !#en
         * The camera zoom ratio.
         * !#zh
         * 摄像机缩放比率
         * @property {Number} zoomRatio
         */
        zoomRatio: {
            get () {
                return this._zoomRatio;
            },
            set (value) {
                this._zoomRatio = value;
            }
        },

        /**
         * !#en
         * Field of view. The width of the Camera’s view angle, measured in degrees along the local Y axis.
         * !#zh
         * 决定摄像机视角的宽度，当摄像机处于透视投影模式下这个属性才会生效。
         * @property {Number} fov
         * @default 60
         */
        fov: {
            get () {
                return this._fov;
            },
            set (v) {
                this._fov = v;
            }
        },

        /**
         * !#en
         * The viewport size of the Camera when set to orthographic projection.
         * !#zh
         * 摄像机在正交投影模式下的视窗大小。
         * @property {Number} orthoSize
         * @default 10
         */
        orthoSize: {
            get () {
                return this._orthoSize;
            },
            set (v) {
                this._orthoSize = v;
            }
        },

        /**
         * !#en
         * The near clipping plane.
         * !#zh
         * 摄像机的近剪裁面。
         * @property {Number} nearClip
         * @default 0.1
         */
        nearClip: {
            get () {
                return this._nearClip;
            },
            set (v) {
                this._nearClip = v;
                this._updateClippingpPlanes();
            }
        },

        /**
         * !#en
         * The far clipping plane.
         * !#zh
         * 摄像机的远剪裁面。
         * @property {Number} farClip
         * @default 4096
         */
        farClip: {
            get () {
                return this._farClip;
            },
            set (v) {
                this._farClip = v;
                this._updateClippingpPlanes();
            }
        },

        /**
         * !#en
         * Is the camera orthographic (true) or perspective (false)?
         * !#zh
         * 设置摄像机的投影模式是正交还是透视模式。
         * @property {Boolean} ortho
         * @default false
         */
        ortho: {
            get () {
                return this._ortho;
            },
            set (v) {
                this._ortho = v;
                this._updateProjection();
            }
        },

        /**
         * !#en
         * Four values (0-1) that indicate where on the screen this camera view will be drawn.
         * !#zh
         * 决定摄像机绘制在屏幕上哪个位置，值为 0-1。
         * @property {Rect} rect
         * @default cc.rect(0,0,1,1)
         */
        rect: {
            get () {
                return this._rect;
            },
            set (v) {
                this._rect = v;
                this._updateRect();
            }
        },

        /**
         * !#en
         * This is used to render parts of the scene selectively.
         * !#zh
         * 决定摄像机会渲染场景的哪一部分。
         * @property {Number} cullingMask
         */
        cullingMask: {
            get () {
                return this._cullingMask;
            },
            set (value) {
                this._cullingMask = value;
                this._updateCameraMask();
            }
        },

        /**
         * !#en
         * Determining what to clear when camera rendering.
         * !#zh
         * 决定摄像机渲染时会清除哪些状态。
         * @property {Camera.ClearFlags} clearFlags
         */
        clearFlags: {
            get () {
                return this._clearFlags;
            },
            set (value) {
                this._clearFlags = value;
                if (this._camera) {
                    this._camera.setClearFlags(value);
                }
            }
        },

        /**
         * !#en
         * The color with which the screen will be cleared.
         * !#zh
         * 摄像机用于清除屏幕的背景色。
         * @property {Color} backgroundColor
         */
        backgroundColor: {
            get () {
                return this._backgroundColor;
            },
            set (value) {
                this._backgroundColor = value;
                this._updateBackgroundColor();
            }
        },

        /**
         * !#en
         * Camera's depth in the camera rendering order.
         * !#zh
         * 摄像机深度，用于决定摄像机的渲染顺序。
         * @property {Number} depth
         */
        depth: {
            get () {
                return this._depth;
            },
            set (value) {
                this._depth = value;
                if (this._camera) {
                    this._camera._priority = value;
                }
            }
        },

        /**
         * !#en
         * Destination render texture.
         * Usually cameras render directly to screen, but for some effects it is useful to make a camera render into a texture.
         * !#zh
         * 摄像机渲染的目标 RenderTexture。
         * 一般摄像机会直接渲染到屏幕上，但是有一些效果可以使用摄像机渲染到 RenderTexture 上再对 RenderTexture 进行处理来实现。
         * @property {RenderTexture} targetTexture
         */
        targetTexture: {
            get () {
                return this._targetTexture;
            },
            set (value) {
                this._targetTexture = value;
                this._updateTargetTexture();
            }
        },

        renderStages: {
            get () {
                return this._renderStages;
            },
            set (val) {
                this._renderStages = val;
                this._updateStages();
            }
        },

        _is3D: {
            get () {
                return this.node._is3DNode;
            }
        }
    },

    statics: {
        /**
         * !#en
         * The first enabled camera.
         * !#zh
         * 第一个被激活的摄像机。
         * @property {Camera} main
         * @static
         */
        main: null,

        /**
         * !#en
         * All enabled cameras.
         * !#zh
         * 激活的所有摄像机。
         * @property {[Camera]} cameras
         * @static
         */
        cameras: _cameras,

        ClearFlags: ClearFlags,

        /**
         * !#en
         * Get the first camera which the node belong to.
         * !#zh
         * 获取节点所在的第一个摄像机。
         * @method findCamera
         * @param {Node} node 
         * @return {Camera}
         * @static
         */
        findCamera (node) {
            for (let i = 0, l = _cameras.length; i < l; i++) {
                let camera = _cameras[i];
                if (camera.containsNode(node)) {
                    return camera;
                }
            }

            return null;
        },

        _setupDebugCamera () {
            if (_debugCamera) return;
            if (game.renderType === game.RENDER_TYPE_CANVAS) return;
            let camera = new RendererCamera();
            _debugCamera = camera;

            camera.setStages([
                'opaque',
            ]);
            
            camera.setFov(Math.PI * 60 / 180);
            camera.setNear(0.1);
            camera.setFar(4096);

            camera.dirty = true;

            camera._cullingMask = 1 << cc.Node.BuiltinGroupIndex.DEBUG;
            camera._priority = cc.macro.MAX_ZINDEX;
            camera.setClearFlags(0);
            camera.setColor(0, 0, 0, 0);

            let node = new cc.Node();
            camera.setNode(node);

            repositionDebugCamera();
            cc.view.on('design-resolution-changed', repositionDebugCamera);

            renderer.scene.addCamera(camera);
        }
    },

    _updateCameraMask () {
        if (this._camera) {
            let mask = this._cullingMask & (~(1 << cc.Node.BuiltinGroupIndex.DEBUG));
            this._camera._cullingMask = mask;
        }
    },

    _updateBackgroundColor () {
        if (!this._camera) return;

        let color = this._backgroundColor;
        this._camera.setColor(
            color.r / 255,
            color.g / 255,
            color.b / 255,
            color.a / 255,
        );
    },

    _updateTargetTexture () {
        if (!this._camera) return;

        let texture = this._targetTexture;
        this._camera._framebuffer = texture ? texture._framebuffer : null;
    },

    _updateClippingpPlanes () {
        if (!this._camera) return;
        this._camera.setNear(this._nearClip);
        this._camera.setFar(this._farClip);
    },

    _updateProjection () {
        if (!this._camera) return;
        let type = this._ortho ? 1 : 0;
        this._camera.setType(type);
    },

    _updateRect () {
        if (!this._camera) return;
        this._camera.setRect(this._rect);
    },

    _updateStages () {
        let flags = this._renderStages;
        let stages = this._camera._stages;
        stages.length = 0;
        if (flags & StageFlags.OPAQUE) {
            stages.push('opaque');
        }
        if (flags & StageFlags.TRANSPARENT) {
            stages.push('transparent');
        }
    },

    _init () {
        if (this._inited) return;
        this._inited = true;

        let camera = this._camera;
        if (!camera) return;
        camera.setNode(this.node);
        camera.setClearFlags(this._clearFlags);
        camera._priority = this._depth;
        this._updateBackgroundColor();
        this._updateCameraMask();
        this._updateTargetTexture();
        this._updateClippingpPlanes();
        this._updateProjection();
        this._updateStages();
    },

    onLoad () {
        this._init();
    },

    onEnable () {
        if (!CC_EDITOR && game.renderType !== game.RENDER_TYPE_CANVAS) {
            cc.director.on(cc.Director.EVENT_BEFORE_DRAW, this.beforeDraw, this);
            renderer.scene.addCamera(this._camera);
        }
        _cameras.push(this);
    },

    onDisable () {
        if (!CC_EDITOR && game.renderType !== game.RENDER_TYPE_CANVAS) {
            cc.director.off(cc.Director.EVENT_BEFORE_DRAW, this.beforeDraw, this);
            renderer.scene.removeCamera(this._camera);
        }
        cc.js.array.remove(_cameras, this);
    },

    /**
     * !#en
     * Returns the matrix that transform the node's (local) space coordinates into the camera's space coordinates.
     * !#zh
     * 返回一个将节点坐标系转换到摄像机坐标系下的矩阵
     * @method getNodeToCameraTransform
     * @param {Node} node - the node which should transform
     * @return {AffineTransform}
     */
    getNodeToCameraTransform (node) {
        let out = AffineTrans.identity();
        node.getWorldMatrix(_mat4_temp_2);
        if (this.containsNode(node)) {
            this.getWorldToCameraMatrix(_mat4_temp_1);
            mat4.mul(_mat4_temp_2, _mat4_temp_2, _mat4_temp_1);
        }
        AffineTrans.fromMat4(out, _mat4_temp_2);
        return out;
    },

    /**
     * !#en
     * Conver a camera coordinates point to world coordinates.
     * !#zh
     * 将一个摄像机坐标系下的点转换到世界坐标系下。
     * @method getCameraToWorldPoint
     * @param {Vec2} point - the point which should transform
     * @param {Vec2} out - the point to receive the result
     * @return {Vec2}
     */
    getCameraToWorldPoint (point, out) {
        out = out || cc.v2();
        this.getCameraToWorldMatrix(_mat4_temp_1);
        vec2.transformMat4(out, point, _mat4_temp_1);
        return out;
    },

    /**
     * !#en
     * Conver a world coordinates point to camera coordinates.
     * !#zh
     * 将一个世界坐标系下的点转换到摄像机坐标系下。
     * @method getWorldToCameraPoint
     * @param {Vec2} point 
     * @param {Vec2} out - the point to receive the result
     * @return {Vec2}
     */
    getWorldToCameraPoint (point, out) {
        out = out || cc.v2();
        this.getWorldToCameraMatrix(_mat4_temp_1);
        vec2.transformMat4(out, point, _mat4_temp_1);
        return out;
    },

    /**
     * !#en
     * Get the camera to world matrix
     * !#zh
     * 获取摄像机坐标系到世界坐标系的矩阵
     * @method getCameraToWorldMatrix
     * @param {Mat4} out - the matrix to receive the result
     * @return {Mat4}
     */
    getCameraToWorldMatrix (out) {
        this.getWorldToCameraMatrix(out);
        mat4.invert(out, out);
        return out;
    },


    /**
     * !#en
     * Get the world to camera matrix
     * !#zh
     * 获取世界坐标系到摄像机坐标系的矩阵
     * @method getWorldToCameraMatrix
     * @param {Mat4} out - the matrix to receive the result
     * @return {Mat4}
     */
    getWorldToCameraMatrix (out) {
        this.node.getWorldRT(_mat4_temp_1);

        let zoomRatio = this.zoomRatio;
        _mat4_temp_1.m00 *= zoomRatio;
        _mat4_temp_1.m01 *= zoomRatio;
        _mat4_temp_1.m04 *= zoomRatio;
        _mat4_temp_1.m05 *= zoomRatio;

        let m12 = _mat4_temp_1.m12;
        let m13 = _mat4_temp_1.m13;

        let center = cc.visibleRect.center;
        _mat4_temp_1.m12 = center.x - (_mat4_temp_1.m00 * m12 + _mat4_temp_1.m04 * m13);
        _mat4_temp_1.m13 = center.y - (_mat4_temp_1.m01 * m12 + _mat4_temp_1.m05 * m13);

        if (out !== _mat4_temp_1) {
            mat4.copy(out, _mat4_temp_1);
        }
        return out;
    },

    /**
     * !#en
     * Get a ray from screen position
     * !#zh
     * 从屏幕坐标获取一条射线
     * @method getRay
     * @param {Vec2} screenPos
     * @return {Ray}
     */
    getRay (screenPos) {
        if (!ray) return screenPos;
        
        vec3.set(_v3_temp_3, screenPos.x, screenPos.y, 1);
        this._camera.screenToWorld(_v3_temp_2, _v3_temp_3, cc.visibleRect.width, cc.visibleRect.height);

        if (this.ortho) {
            vec3.set(_v3_temp_3, screenPos.x, screenPos.y, -1);
            this._camera.screenToWorld(_v3_temp_1, _v3_temp_3, cc.visibleRect.width, cc.visibleRect.height);
        }
        else {
            this.node.getWorldPosition(_v3_temp_1);
        }

        return ray.fromPoints(ray.create(), _v3_temp_1, _v3_temp_2);
    },

    /**
     * !#en
     * Check whether the node is in the camera.
     * !#zh
     * 检测节点是否被此摄像机影响
     * @method containsNode
     * @param {Node} node - the node which need to check
     * @return {Boolean}
     */
    containsNode (node) {
        return node._cullingMask & this.cullingMask;
    },

    /**
     * !#en
     * Render the camera manually.
     * !#zh
     * 手动渲染摄像机。
     * @method render
     * @param {Node} root 
     */
    render (root) {
        root = root || cc.director.getScene();
        if (!root) return null;

        // force update node world matrix
        this.node.getWorldMatrix(_mat4_temp_1);
        this.beforeDraw();
        RenderFlow.visit(root);
        renderer._forward.renderCamera(this._camera, renderer.scene);
    },

    _layout () {
        let height = cc.game.canvas.height / cc.view._scaleY;

        let targetTexture = this._targetTexture;
        if (targetTexture) {
            height = targetTexture.height;
        }

        let fov = this._fov * cc.macro.RAD;
        this.node.z = height / (Math.tan(fov / 2) * 2);

        fov = Math.atan(Math.tan(fov / 2) / this.zoomRatio) * 2;
        this._camera.setFov(fov);
        this._camera.setOrthoHeight(height / 2 / this.zoomRatio);
    },

    beforeDraw () {
        if (!this._camera) return;

        if (!this.node._is3DNode) {
            this._layout();
        }
        else {
            this._camera.setFov(this._fov * cc.macro.RAD);
            this._camera.setOrthoHeight(this._orthoSize);
        }

        this._camera.dirty = true;
    }
});

module.exports = cc.Camera = Camera;
