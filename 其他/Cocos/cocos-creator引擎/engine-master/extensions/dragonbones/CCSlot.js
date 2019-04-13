/****************************************************************************
 Copyright (c) 2016 Chukong Technologies Inc.
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

import { mat4 } from '../../cocos2d/core/vmath';

const BinaryOffset = dragonBones.BinaryOffset;
const BoneType  = dragonBones.BoneType;

dragonBones.CCSlot = cc.Class({
    name: 'dragonBones.CCSlot',
    extends: dragonBones.Slot,

    ctor () {
        this._localVertices = [];
        this._indices = [];
        this._matrix = mat4.create();
        this._worldMatrix = mat4.create();
        this._worldMatrixDirty = true;
        this._visible = false;
        this._color = cc.color();
    },

    _onClear () {
        this._super();
        this._localVertices.length = 0;
        this._indices.length = 0;
        mat4.identity(this._matrix);
        mat4.identity(this._worldMatrix);
        this._worldMatrixDirty = true;
        this._color = cc.color();
        this._visible = false;
    },

    statics : {
        toString: function () {
            return "[class dragonBones.CCSlot]";
        }
    },

    // just for adapt to dragonbones api,no need to do any thing
    _onUpdateDisplay () {
    },

    // just for adapt to dragonbones api,no need to do any thing
    _initDisplay (value) {
    },

    _addDisplay () {
        this._visible = true;
    },

    // just for adapt to dragonbones api,no need to do any thing
    _replaceDisplay (value) {
    },

    _removeDisplay () {
        this._visible = false;
    },

    // just for adapt to dragonbones api,no need to do any thing
    _disposeDisplay (object) {
    },

    // just for adapt to dragonbones api,no need to do any thing
    _updateVisible () {
    },

    // just for adapt to dragonbones api,no need to do any thing
    _updateZOrder () {
    },

    _updateBlendMode () {
        if (this._childArmature) {
            let childSlots = this._childArmature.getSlots();
            for (let i = 0, l = childSlots.length; i < l; i++) {
                let slot = childSlots[i];
                slot._blendMode = this._blendMode;
                slot._updateBlendMode();
            }
        }
    },

    _updateColor () {
        let c = this._color;
        c.r = this._colorTransform.redMultiplier * 255;
        c.g = this._colorTransform.greenMultiplier * 255;
        c.b = this._colorTransform.blueMultiplier * 255;
        c.a = this._colorTransform.alphaMultiplier * 255;
    },

    //return dragonBones.CCTexture2D
    getTexture () {
        return this._textureData && this._textureData.spriteFrame && this._textureData.spriteFrame.getTexture();
    },

    _updateFrame () {
        this._indices.length = 0;
        let indices = this._indices,
            localVertices = this._localVertices;
        let indexOffset = 0, vfOffset = 0;

        let currentTextureData = this._textureData;

        // update the frame
        if (!this._display || this._displayIndex < 0 || !currentTextureData) return;
        let currentDisplayData = this._displayIndex < this.rawDisplayDatas.length ? this.rawDisplayDatas[this._displayIndex] : null;

        let textureAtlas = this._armature._replacedTexture || currentTextureData.parent.renderTexture;
        if (textureAtlas && (!currentTextureData.spriteFrame || currentTextureData.spriteFrame.getTexture() !== textureAtlas)) {
            // Create and cache texture
            let rect = cc.rect(currentTextureData.region.x, currentTextureData.region.y,
                                currentTextureData.region.width, currentTextureData.region.height);
            let offset = cc.v2(0, 0);
            let size = cc.size(currentTextureData.region.width, currentTextureData.region.height);

            currentTextureData.spriteFrame = new cc.SpriteFrame();
            currentTextureData.spriteFrame.setTexture(textureAtlas, rect, false, offset, size);
        }

        let textureAtlasWidth = textureAtlas.width;
        let textureAtlasHeight = textureAtlas.height;
        let region = currentTextureData.region;

        let meshData = this._meshData;
        if (meshData) {
            const scale = this._armature._armatureData.scale;
            const data = meshData.parent.parent.parent;
            const intArray = data.intArray;
            const floatArray = data.floatArray;
            const vertexCount = intArray[meshData.offset + BinaryOffset.MeshVertexCount];
            const triangleCount = intArray[meshData.offset + BinaryOffset.MeshTriangleCount];
            let vertexOffset = intArray[meshData.offset + BinaryOffset.MeshFloatOffset];

            if (vertexOffset < 0) {
                vertexOffset += 65536; // Fixed out of bouds bug. 
            }

            const uvOffset = vertexOffset + vertexCount * 2;

            for (let i = 0, l = vertexCount; i < l; i++) {
                localVertices[vfOffset++] = floatArray[vertexOffset + i*2] * scale;
                localVertices[vfOffset++] = -floatArray[vertexOffset + i*2 + 1] * scale;  

                localVertices[vfOffset++] = (region.x + floatArray[uvOffset + i*2] * region.width) / textureAtlasWidth;
                localVertices[vfOffset++] = (region.y + floatArray[uvOffset + i*2 + 1] * region.height) / textureAtlasHeight;
            }

            for (let i = 0; i < triangleCount * 3; ++i) {
                indices[indexOffset++] = intArray[meshData.offset + BinaryOffset.MeshVertexIndices + i];
            }

            localVertices.length = vfOffset;
            indices.length = indexOffset;

            this._pivotX = 0;
            this._pivotY = 0;
        }
        else {
            let scale = this._armature.armatureData.scale;
            this._pivotX = currentDisplayData.pivot.x;
            this._pivotY = currentDisplayData.pivot.y;

            let rectData = currentTextureData.frame || currentTextureData.region;
            let width = rectData.width * scale;
            let height = rectData.height * scale;
            if (!currentTextureData.frame && currentTextureData.rotated) {
                width = rectData.height;
                height = rectData.width;
            }

            this._pivotX *= width;
            this._pivotY *= height;

            if (currentTextureData.frame) {
                this._pivotX += currentTextureData.frame.x * scale;
                this._pivotY += currentTextureData.frame.y * scale;
            }

            this._pivotY -= region.height * scale;

            let l = region.x / textureAtlasWidth;
            let b = (region.y + region.height) / textureAtlasHeight;
            let r = (region.x + region.width) / textureAtlasWidth;
            let t = region.y / textureAtlasHeight;

            localVertices[vfOffset++] = 0; // 0x
            localVertices[vfOffset++] = 0; // 0y
            localVertices[vfOffset++] = l; // 0u
            localVertices[vfOffset++] = b; // 0v

            localVertices[vfOffset++] = region.width; // 1x
            localVertices[vfOffset++] = 0; // 1y
            localVertices[vfOffset++] = r; // 1u
            localVertices[vfOffset++] = b; // 1v

            localVertices[vfOffset++] = 0; // 2x
            localVertices[vfOffset++] = region.height;; // 2y
            localVertices[vfOffset++] = l; // 2u
            localVertices[vfOffset++] = t; // 2v

            localVertices[vfOffset++] = region.width; // 3x
            localVertices[vfOffset++] = region.height;; // 3y
            localVertices[vfOffset++] = r; // 3u
            localVertices[vfOffset++] = t; // 3v

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 1;
            indices[4] = 3;
            indices[5] = 2;

            localVertices.length = vfOffset;
            indices.length = 6;

            this._blendModeDirty = true;
        }
    },

    _updateMesh () {
        const scale = this._armature._armatureData.scale;
        const meshData = this._meshData;
        const hasDeform = this._deformVertices.length > 0 && meshData.inheritDeform;
        const weight = meshData.weight;

        let localVertices = this._localVertices;
        if (weight !== null) {
            const data = meshData.parent.parent.parent;
            const intArray = data.intArray;
            const floatArray = data.floatArray;
            const vertexCount = intArray[meshData.offset + BinaryOffset.MeshVertexCount];
            let weightFloatOffset = intArray[weight.offset + BinaryOffset.WeigthFloatOffset];

            if (weightFloatOffset < 0) {
                weightFloatOffset += 65536; // Fixed out of bouds bug. 
            }

            for (
                let i = 0, iB = weight.offset + BinaryOffset.WeigthBoneIndices + weight.bones.length, iV = weightFloatOffset, iF = 0, lvi = 0;
                i < vertexCount;
                i++, lvi+=4
            ) {
                const boneCount = intArray[iB++];
                let xG = 0.0, yG = 0.0;

                for (let j = 0; j < boneCount; ++j) {
                    const boneIndex = intArray[iB++];
                    const bone = this._meshBones[boneIndex];

                    if (bone !== null) {
                        const matrix = bone.globalTransformMatrix;
                        const weight = floatArray[iV++];
                        let xL = floatArray[iV++] * scale;
                        let yL = floatArray[iV++] * scale;

                        if (hasDeform) {
                            xL += this._deformVertices[iF++];
                            yL += this._deformVertices[iF++];
                        }

                        xG += (matrix.a * xL + matrix.c * yL + matrix.tx) * weight;
                        yG += (matrix.b * xL + matrix.d * yL + matrix.ty) * weight;
                    }
                }

                localVertices[lvi] = xG;
                localVertices[lvi + 1] = -yG;
            }
        }
        else if (hasDeform) {
            const isSurface = this._parent._boneData.type !== BoneType.Bone;
            // const isGlue = meshData.glue !== null; TODO
            const data = meshData.parent.parent.parent;
            const intArray = data.intArray;
            const floatArray = data.floatArray;
            const vertexCount = intArray[meshData.offset + BinaryOffset.MeshVertexCount];
            let vertexOffset = intArray[meshData.offset + BinaryOffset.MeshFloatOffset];

            if (vertexOffset < 0) {
                vertexOffset += 65536; // Fixed out of bouds bug. 
            }

            for (let i = 0, l = vertexCount, lvi = 0; i < l; i ++, lvi += 4) {
                const x = floatArray[vertexOffset + i*2] * scale + this._deformVertices[i*2];
                const y = floatArray[vertexOffset + i*2 + 1] * scale + this._deformVertices[i*2 + 1];

                if (isSurface) {
                    const matrix = this._parent._getGlobalTransformMatrix(x, y);
                    localVertices[lvi] = matrix.a * x + matrix.c * y + matrix.tx;
                    localVertices[lvi + 1] = -matrix.b * x + matrix.d * y + matrix.ty;
                }
                else {
                    localVertices[lvi] = x;
                    localVertices[lvi + 1] = -y;
                }
            }
        }

    },

    _updateTransform () {
        let t = this._matrix;
        t.m00 = this.globalTransformMatrix.a;
        t.m01 = -this.globalTransformMatrix.b;
        t.m04 = -this.globalTransformMatrix.c;
        t.m05 = this.globalTransformMatrix.d;
        t.m12 = this.globalTransformMatrix.tx - (this.globalTransformMatrix.a * this._pivotX + this.globalTransformMatrix.c * this._pivotY);
        t.m13 = -(this.globalTransformMatrix.ty - (this.globalTransformMatrix.b * this._pivotX + this.globalTransformMatrix.d * this._pivotY));

        this._worldMatrixDirty = true;
    },

    updateWorldMatrix () {
        if (!this._armature) return;

        var parentSlot = this._armature._parent;
        if (parentSlot) {
            parentSlot.updateWorldMatrix();
        }

        if (this._worldMatrixDirty) {
            this.calculWorldMatrix();
            var childArmature = this.childArmature;
            if (!childArmature) return;
            var slots = childArmature.getSlots();
            for (var i = 0,n = slots.length; i < n; i++) {
                var slot = slots[i];
                if (slot) {
                    slot._worldMatrixDirty = true;
                }
            }
        }
    },

    _mulMat (out, a, b) {
        let aa=a.m00, ab=a.m01, ac=a.m04, ad=a.m05, atx=a.m12, aty=a.m13;
        let ba=b.m00, bb=b.m01, bc=b.m04, bd=b.m05, btx=b.m12, bty=b.m13;
        if (ab !== 0 || ac !== 0) {
            out.m00 = ba * aa + bb * ac;
            out.m01 = ba * ab + bb * ad;
            out.m04 = bc * aa + bd * ac;
            out.m05 = bc * ab + bd * ad;
            out.m12 = aa * btx + ac * bty + atx;
            out.m13 = ab * btx + ad * bty + aty;
        }
        else {
            out.m00 = ba * aa;
            out.m01 = bb * ad;
            out.m04 = bc * aa;
            out.m05 = bd * ad;
            out.m12 = aa * btx + atx;
            out.m13 = ad * bty + aty;
        }
    },

    calculWorldMatrix () {
        var parent = this._armature._parent;
        if (parent) {
            this._mulMat(this._worldMatrix ,parent._worldMatrix, this._matrix);
        } else {
            mat4.copy(this._worldMatrix, this._matrix);
        }
        this._worldMatrixDirty = false;
    }
});
