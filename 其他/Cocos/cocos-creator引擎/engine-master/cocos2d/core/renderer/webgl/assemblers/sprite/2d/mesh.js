/****************************************************************************
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

const fillVerticesWithoutCalc = require('../../utils').fillVerticesWithoutCalc;
const packToDynamicAtlas = require('../../../../utils/utils').packToDynamicAtlas;

module.exports = {
    createData (sprite) {
        return sprite.requestRenderData();
    },

    updateRenderData (sprite) {
        packToDynamicAtlas(sprite, sprite._spriteFrame);

        let renderData = sprite._renderData;
        let frame = sprite.spriteFrame;
        if (!renderData || !frame) return;
        let vertices = frame.vertices;
        if (vertices) {
            if (renderData.vertexCount !== vertices.x.length) {
                renderData.vertexCount = vertices.x.length;
                renderData.indiceCount = vertices.triangles.length;
                
                // 1 for world vertices, 2 for local vertices
                renderData.dataLength = renderData.vertexCount * 2;

                renderData.uvDirty = renderData.vertDirty = true;
            }

            if (renderData.uvDirty) {
                this.updateUVs(sprite);
            }
            let vertDirty = renderData.vertDirty;
            if (vertDirty) {
                this.updateVerts(sprite);
                this.updateWorldVerts(sprite);
            }
        }
    },

    updateUVs (sprite) {
        let vertices = sprite.spriteFrame.vertices,
            u = vertices.nu,
            v = vertices.nv;

        let renderData = sprite._renderData;
        let data = renderData._data;
        for (let i = 0, l = u.length; i < l; i++) {
            let vertice = data[i];
            vertice.u = u[i];
            vertice.v = v[i];
        }

        renderData.uvDirty = false;
    },

    updateVerts (sprite) {
        let node = sprite.node,
            contentWidth = Math.abs(node.width),
            contentHeight = Math.abs(node.height),
            appx = node.anchorX * contentWidth,
            appy = node.anchorY * contentHeight;

        let frame = sprite.spriteFrame,
            vertices = frame.vertices,
            x = vertices.x,
            y = vertices.y,
            originalWidth = frame._originalSize.width,
            originalHeight = frame._originalSize.height,
            rectWidth = frame._rect.width,
            rectHeight = frame._rect.height,
            offsetX = frame._offset.x,
            offsetY = frame._offset.y,
            trimX = offsetX + (originalWidth - rectWidth) / 2,
            trimY = offsetY + (originalHeight - rectHeight) / 2;

        let scaleX = contentWidth / (sprite.trim ? rectWidth : originalWidth),
            scaleY = contentHeight / (sprite.trim ? rectHeight : originalHeight);

        let renderData = sprite._renderData;
        let data = renderData._data;

        if (!sprite.trim) {
            for (let i = 0, l = x.length; i < l; i++) {
                let vertice = data[i + l];
                vertice.x = (x[i]) * scaleX - appx;
                vertice.y = (originalHeight - y[i]) * scaleY - appy;
            }
        }
        else {
            for (let i = 0, l = x.length; i < l; i++) {
                let vertice = data[i + l];
                vertice.x = (x[i] - trimX) * scaleX - appx;
                vertice.y = (originalHeight - y[i] - trimY) * scaleY - appy;
            }
        }

        renderData.vertDirty = false;
    },

    updateWorldVerts (sprite) {
        let node = sprite.node,
            renderData = sprite._renderData,
            data = renderData._data;

        let matrix = node._worldMatrix;

        let a = matrix.m00, b = matrix.m01, c = matrix.m04, d = matrix.m05,
            tx = matrix.m12, ty = matrix.m13;
        for (let i = 0, l = renderData.vertexCount; i < l; i++) {
            let local = data[i + l];
            let world = data[i];
            world.x = local.x * a + local.y * c + tx;
            world.y = local.x * b + local.y * d + ty;
        }
    },

    fillBuffers (sprite, renderer) {
        let vertices = sprite.spriteFrame.vertices;
        if (!vertices) {
            return;
        }

        // update world verts
        if (renderer.worldMatDirty) {
            this.updateWorldVerts(sprite);
        }

        // buffer
        let buffer = renderer._meshBuffer;
        let node = sprite.node;
        let offsetInfo = fillVerticesWithoutCalc(node, buffer, sprite._renderData, node._color._val);

        let ibuf = buffer._iData,
            indiceOffset = offsetInfo.indiceOffset,
            vertexId = offsetInfo.vertexOffset;

        let triangles = vertices.triangles;
        for (let i = 0, l = triangles.length; i < l; i++) {
            ibuf[indiceOffset++] = vertexId + triangles[i];
        }
    },
};