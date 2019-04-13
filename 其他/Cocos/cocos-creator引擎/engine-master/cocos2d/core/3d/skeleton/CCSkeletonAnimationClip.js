/****************************************************************************
 Copyright (c) 2017-2018 Xiamen Yaji Software Co., Ltd.

 http://www.cocos.com

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

const AnimationClip = require('../../../animation/animation-clip');
const JointMatrixCurve = require('./CCJointMatrixCurve');
import mat4 from '../../vmath/mat4';

function maxtrixToArray (matrix) {
    let data = new Float32Array(16);
    data[0] = matrix.m00;
    data[1] = matrix.m01;
    data[2] = matrix.m02;
    data[3] = matrix.m03;
    data[4] = matrix.m04;
    data[5] = matrix.m05;
    data[6] = matrix.m06;
    data[7] = matrix.m07;
    data[8] = matrix.m08;
    data[9] = matrix.m09;
    data[10] = matrix.m10;
    data[11] = matrix.m11;
    data[12] = matrix.m12;
    data[13] = matrix.m13;
    data[14] = matrix.m14;
    data[15] = matrix.m15;
    return data;
}

/**
* @module cc
*/
/**
 * !#en SkeletonAnimationClip Asset.
 * !#zh 骨骼动画剪辑。
 * @class SkeletonAnimationClip
 * @extends AnimationClip
 */
let SkeletonAnimationClip = cc.Class({
    name: 'cc.SkeletonAnimationClip',
    extends: AnimationClip,

    properties: {
        _nativeAsset: {
            override: true,
            get () {
                return this._buffer;
            },
            set (bin) {
                let buffer = ArrayBuffer.isView(bin) ? bin.buffer : bin;
                this._buffer = new Float32Array(buffer || bin, 0, buffer.byteLength / 4);
            }
        },

        /**
         * Describe the data structure.
         * { path: { offset, frameCount, property } }
         */
        description: {
            default: null,
            type: Object,
        },

        /**
         * SkeletonAnimationClip's curveData is generated from binary buffer.
         * So should not serialize curveData.
         */
        curveData: {
            visible: false,
            override: true,
            get () {
                return this._curveData || {};
            },
            set () {}
        }
    },

    _init () {
        if (this._curveData) {
            return this._curveData;
        }

        this._curveData = {};
        
        this._generateCommonCurve();

        if (this._model.precomputeJointMatrix) {
            this._generateJointMatrixCurve();
        }

        return this._curveData;
    },

    _generateCommonCurve () {
        let buffer = this._buffer;
        let description = this.description;

        let offset = 0;
        function getValue () {
            return buffer[offset++];
        }

        if (!this._curveData.paths) {
            this._curveData.paths = {};
        }
        let paths = this._curveData.paths;

        for (let path in description) {
            let des = description[path];
            let curves = {};
            paths[path] = { props: curves };

            for (let property in des) {
                let frames = [];

                let frameCount = des[property].frameCount;
                offset = des[property].offset;
                for (let i = 0; i < frameCount; i++) {
                    let frame = getValue();
                    let value;
                    if (property === 'position' || property === 'scale') {
                        value = cc.v3(getValue(), getValue(), getValue());
                    }
                    else if (property === 'quat') {
                        value = cc.quat(getValue(), getValue(), getValue(), getValue());
                    }
                    frames.push({ frame, value });
                }

                curves[property] = frames;
            }
        }
    },

    _generateJointMatrixCurve () {
        let joints = this._model.nodeMap;
        let curveData = this._curveData;
        let paths = curveData.paths;

        // first build a virtual node tree, 
        // each virtual node should contain position, scale, quat, bindpose properties.
        let root = { children: [] };
        for (let path in paths) {
            let nodeLevels = path.split('/');
            let node = root;
            let currentPath = '';
            for (let i = 0; i < nodeLevels.length; i++) {
                let nodeName = nodeLevels[i];
                currentPath += i === 0 ? nodeName : '/' + nodeName;
                if (!node.children[nodeName]) {
                    let joint = joints[currentPath];
                    if (!joint) {
                        cc.warn(`Can not find joint ${currentPath} when generate joint matrix curve.`)
                        break;
                    }
                    node.children[nodeName] = {
                        name: nodeName,
                        path: currentPath,
                        children: {},
                        position: joint.position,
                        quat:  joint.quat, 
                        scale:  joint.scale,
                        bindpose: joint.bindpose
                    };
                }
                node = node.children[nodeName];
            }
        }

        let newCurveData = { ratios: [], jointMatrixMap: {} };
        let jointMatrixMap = newCurveData.jointMatrixMap;

        // walk through node tree to calculate node's joint matrix at time.
        function walk (node, time, pm) {
            let matrix;
            let EPSILON = 10e-5;

            if (node !== root) {
                let props = paths[node.path].props;
                for (let prop in props) {
                    let frames = props[prop];
                    for (let i = 0; i < frames.length; i++) {
                        let end = frames[i];

                        if (Math.abs(end.frame - time) < EPSILON) {
                            node[prop].set(end.value);
                            break;
                        }
                        else if (end.frame > time) {
                            let start = frames[i - 1];
                            let ratio = (time - start.frame) / (end.frame - start.frame);
                            start.value.lerp(end.value, ratio, node[prop]);
                            break;
                        }
                    }
                }

                matrix = mat4.create();
                mat4.fromRTS(matrix, node.quat, node.position, node.scale);

                if (pm) {
                    mat4.mul(matrix, pm, matrix);
                }

                if (!props._jointMatrix) {
                    props._jointMatrix = [];
                }

                let bindWorldMatrix;
                if (node.bindpose) {
                    bindWorldMatrix = mat4.create();
                    mat4.mul(bindWorldMatrix, matrix, node.bindpose);
                }

                if (!jointMatrixMap[node.path]) {
                    jointMatrixMap[node.path] = [];
                }
                jointMatrixMap[node.path].push(maxtrixToArray(bindWorldMatrix || matrix))
            }

            let children = node.children;
            for (let name in children) {
                let child = children[name];
                walk(child, time, matrix);
            }
        }

        let time = 0;
        let duration = this.duration;
        let step = 1 / this.sample;

        while (time < duration) {
            newCurveData.ratios.push(time / duration);
            walk(root, time);
            time += step;
        }

        this._curveData = newCurveData;
    },

    _createJointMatrixCurve (state, root) {
        let curve = new JointMatrixCurve();
        curve.ratios = this.curveData.ratios;

        curve.pairs = [];

        let jointMatrixMap = this.curveData.jointMatrixMap;
        for (let path in jointMatrixMap) {
            let target = cc.find(path, root);
            if (!target) continue;

            curve.pairs.push({
                target: target,
                values: jointMatrixMap[path]
            });
        }

        return [curve];
    },

    createCurves (state, root) {
        if (!this._model) {
            cc.warn(`Skeleton Animation Clip [${this.name}] Can not find model`);
            return [];
        }

        this._init();

        if (this._model.precomputeJointMatrix) {
            return this._createJointMatrixCurve(state, root);
        }
        else {
            return AnimationClip.prototype.createCurves.call(this, state, root);
        }
    }
});

cc.SkeletonAnimationClip = module.exports = SkeletonAnimationClip;
