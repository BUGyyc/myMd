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

import gfx from '../../renderer/gfx';

/**
 * The class BufferRange denotes a range of the buffer.
 */
export let BufferRange = cc.Class({
    name: 'cc.BufferRange',

    properties: {
        /**
         * The offset of the range.
         * @property {Number} offset
         */
        offset: 0,
        /**
         * The length of the range.
         * @property {Number} length
         */
        length: 0
    }
});

export let VertexFormat = cc.Class({
    name: 'cc.mesh.VertexFormat',

    properties: {
        name: '',
        type: -1,
        num: -1,
        normalize: false
    }
});

/**
 * A vertex bundle describes a serials of vertex attributes.
 * These vertex attributes occupy a range of the buffer and
 * are interleaved, no padding bytes, in the range.
 */
export let VertexBundle = cc.Class({
    name: 'cc.mesh.VertexBundle',
    properties: {
        /**
         * The data range of this bundle.
         * This range of data is essentially mapped to a GPU vertex buffer.
         * @property {BufferRange} data
         */
        data: {
            default: null,
            type: BufferRange
        },
        /**
         * The attribute formats.
         * @property {VertexFormat} formats
         */
        formats: {
            default: [],
            type: VertexFormat
        },
        /**
         * The bundle's vertices count.
         */
        verticesCount: 0,
    }
});

/**
 * A primitive is a geometry constituted with a list of
 * same topology primitive graphic(such as points, lines or triangles).
 */
export let Primitive = cc.Class({
    name: 'cc.mesh.Primitive',
    properties: {
        /**
         * The vertex bundle that the primitive use.
         * @property {[Number]} vertexBundleIndices
         */
        vertexBundleIndices: {
            default: [],
            type: Number
        },
        /**
         * The data range of the primitive.
         * This range of data is essentially mapped to a GPU indices buffer.
         * @property {BufferRange} data
         */
        data: {
            default: null,
            type: BufferRange
        },
        /**
         * The type of this primitive's indices.
         * @property {Number} indexUnit
         */
        indexUnit: gfx.INDEX_FMT_UINT16,
        /**
         * The primitive's topology.
         * @property {Number} topology
         */
        topology: gfx.PT_TRIANGLES
    }
});
