/****************************************************************************
 Copyright (c) 2015-2016 Chukong Technologies Inc.
 Copyright (c) 2017-2018 Xiamen Yaji Software Co., Ltd.

 http://www.cocos2d-x.org

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
 ****************************************************************************/

var NIL = function () {};

/**
 * !#en
 * cc.Scene is a subclass of cc.Node that is used only as an abstract concept.<br/>
 * cc.Scene and cc.Node are almost identical with the difference that users can not modify cc.Scene manually.
 * !#zh
 * cc.Scene 是 cc.Node 的子类，仅作为一个抽象的概念。<br/>
 * cc.Scene 和 cc.Node 有点不同，用户不应直接修改 cc.Scene。
 * @class Scene
 * @extends Node
 */
cc.Scene = cc.Class({
    name: 'cc.Scene',
    extends: require('./CCNode'),

    properties: {
        _is3DNode: {
            default: true,
            override: true
        },

        /**
         * !#en Indicates whether all (directly or indirectly) static referenced assets of this scene are releasable by default after scene unloading.
         * !#zh 指示该场景中直接或间接静态引用到的所有资源是否默认在场景切换后自动释放。
         * @property {Boolean} autoReleaseAssets
         * @default false
         */
        autoReleaseAssets: {
            default: undefined,
            type: cc.Boolean
        },

    },

    ctor: function () {
        this._anchorPoint.x = 0.0;
        this._anchorPoint.y = 0.0;

        this._activeInHierarchy = false;
        this._inited = !cc.game._isCloning;

        if (CC_EDITOR) {
            this._prefabSyncedInLiveReload = false;
        }

        // cache all depend assets for auto release
        this.dependAssets = null;
    },

    destroy: function () {
        if (cc.Object.prototype.destroy.call(this)) {
            var children = this._children;
            for (var i = 0; i < children.length; ++i) {
                children[i].active = false;
            }
        }
        this._active = false;
        this._activeInHierarchy = false;
    },

    _onHierarchyChanged: NIL,
    _instantiate : null,

    _load: function () {
        if (!this._inited) {
            if (CC_TEST) {
                cc.assert(!this._activeInHierarchy, 'Should deactivate ActionManager and EventManager by default');
            }
            if (CC_EDITOR && this._prefabSyncedInLiveReload) {
                this._onBatchRestored();
            }
            else {
                this._onBatchCreated();
            }
            this._inited = true;
        }
    },

    _activate: function (active) {
        active = (active !== false);
        if (CC_EDITOR || CC_TEST) {
            // register all nodes to editor
            this._registerIfAttached(active);
        }
        cc.director._nodeActivator.activateNode(this, active);
    }
});

module.exports = cc.Scene;
