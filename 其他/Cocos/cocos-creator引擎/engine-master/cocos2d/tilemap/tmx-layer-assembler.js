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

const TiledLayer = require('./CCTiledLayer');
const TiledMap = require('./CCTiledMap');

const RenderFlow = require('../core/renderer/render-flow');

const Orientation = TiledMap.Orientation;
const TileFlag = TiledMap.TileFlag;
const FLIPPED_MASK = TileFlag.FLIPPED_MASK;
const StaggerAxis = TiledMap.StaggerAxis;
const StaggerIndex = TiledMap.StaggerIndex;

import { mat4, vec3 } from '../core/vmath';

let _mat4_temp = mat4.create();
let _mat4_temp2 = mat4.create();
let _vec3_temp = vec3.create();

let tmxAssembler = {
    updateRenderData (comp) {
        let renderData = comp._renderData;
        if (!renderData) {
            renderData = comp._renderData = comp.requestRenderData();
        }

        let size = comp.node._contentSize;
        let anchor = comp.node._anchorPoint;
        renderData.updateSizeNPivot(size.width, size.height, anchor.x, anchor.y);
        renderData.material = comp.sharedMaterials[0];
        
        this.updateVertices(comp);
    },

    fillBuffers (comp, renderer) {
        let renderData = comp._renderData;
        let data = renderData._data;

        let buffer = renderer._meshBuffer,
            vertexCount = renderData.vertexCount;
            
        let offsetInfo = buffer.request(vertexCount, renderData.indiceCount);

        // buffer data may be realloc, need get reference after request.
        let indiceOffset = offsetInfo.indiceOffset,
            vertexOffset = offsetInfo.byteOffset >> 2,
            vertexId = offsetInfo.vertexOffset,
            vbuf = buffer._vData,
            ibuf = buffer._iData,
            uintbuf = buffer._uintVData;
        
        for (let i = 0, l = renderData.vertexCount; i < l; i++) {
            let vert = data[i];
            vbuf[vertexOffset++] = vert.x;
            vbuf[vertexOffset++] = vert.y;
            vbuf[vertexOffset++] = vert.u;
            vbuf[vertexOffset++] = vert.v;
            uintbuf[vertexOffset++] = vert.color;
        }

        for (let i = 0, l = renderData.indiceCount; i < l; i+=6) {
            ibuf[indiceOffset++] = vertexId;
            ibuf[indiceOffset++] = vertexId+1;
            ibuf[indiceOffset++] = vertexId+2;
            ibuf[indiceOffset++] = vertexId+1;
            ibuf[indiceOffset++] = vertexId+3;
            ibuf[indiceOffset++] = vertexId+2;
            vertexId += 4;
        }

        comp.node._renderFlag |= RenderFlow.FLAG_UPDATE_RENDER_DATA;
    },

    updateVertices (comp) {
        let node = comp.node;
        let renderData = comp._renderData;
        let data = renderData._data;
        let color = node._color._val;
        let opacity = node._color.a;

        renderData.dataLength = renderData.vertexCount = renderData.indiceCount = 0;

        let layerOrientation = comp._layerOrientation,
        tiles = comp._tiles;

        if (!tiles || !comp._tileset) {
            return;
        }
        
        let appx = node._anchorPoint.x * node._contentSize.width,
            appy = node._anchorPoint.y * node._contentSize.height;

        mat4.copy(_mat4_temp, node._worldMatrix);
        vec3.set(_vec3_temp, -appx, -appy, 0);
        mat4.translate(_mat4_temp, _mat4_temp, _vec3_temp);

        let a = _mat4_temp.m00, b = _mat4_temp.m01, c = _mat4_temp.m04, d = _mat4_temp.m05,
            tx = _mat4_temp.m12, ty = _mat4_temp.m13;

        let maptw = comp._mapTileSize.width,
            mapth = comp._mapTileSize.height,
            tilew = comp._tileset._tileSize.width,
            tileh = comp._tileset._tileSize.height,
            extw = tilew - maptw,
            exth = tileh - mapth,
            winw = cc.winSize.width,
            winh = cc.winSize.height,
            rows = comp._layerSize.height,
            cols = comp._layerSize.width,
            grids = comp._texGrids,
            tiledTiles = comp._tiledTiles,
            ox = comp._offset.x,
            oy = comp._offset.y,
            w = tilew * a, h = tileh * d;

        tx += ox * a + oy * c;
        ty += ox * b + oy * d;
        // Culling
        let startCol = 0, startRow = 0,
            maxCol = cols, maxRow = rows;

        let cullingA = a, cullingD = d,
            cullingMapx = tx, cullingMapy = ty,
            cullingW = w, cullingH = h;
        let enabledCulling = !CC_EDITOR && cc.macro.ENABLE_TILEDMAP_CULLING;
        
        if (enabledCulling) {
            let camera = cc.Camera.findCamera(comp.node);
            if (camera) {
                camera.getWorldToCameraMatrix(_mat4_temp2);
                mat4.mul(_mat4_temp, _mat4_temp2, _mat4_temp);
                cullingA = _mat4_temp.m00;
                cullingD = _mat4_temp.m05;
                cullingMapx = ox * cullingA + oy * _mat4_temp.m04 + _mat4_temp.m12;
                cullingMapy = ox * _mat4_temp.m01 + oy * cullingD + _mat4_temp.m13;
                cullingW = tilew * cullingA;
                cullingH = tileh * cullingD;
            }
                
            if (layerOrientation === Orientation.ORTHO) {
                mat4.invert(_mat4_temp, _mat4_temp);

                let rect = cc.visibleRect;
                let a = _mat4_temp.m00, b = _mat4_temp.m01, c = _mat4_temp.m04, d = _mat4_temp.m05,
                    tx = _mat4_temp.m12, ty = _mat4_temp.m13;
                let v0x = rect.topLeft.x * a + rect.topLeft.y * c + tx;
                let v0y = rect.topLeft.x * b + rect.topLeft.y * d + ty;
                let v1x = rect.bottomLeft.x * a + rect.bottomLeft.y * c + tx;
                let v1y = rect.bottomLeft.x * b + rect.bottomLeft.y * d + ty;
                let v2x = rect.topRight.x * a + rect.topRight.y * c + tx;
                let v2y = rect.topRight.x * b + rect.topRight.y * d + ty;
                let v3x = rect.bottomRight.x * a + rect.bottomRight.y * c + tx;
                let v3y = rect.bottomRight.x * b + rect.bottomRight.y * d + ty;
                let minx = Math.min(v0x, v1x, v2x, v3x),
                    maxx = Math.max(v0x, v1x, v2x, v3x),
                    miny = Math.min(v0y, v1y, v2y, v3y),
                    maxy = Math.max(v0y, v1y, v2y, v3y);
                
                startCol = Math.floor(minx / maptw);
                startRow = rows - Math.ceil(maxy / mapth);
                maxCol = Math.ceil((maxx + extw) / maptw);
                maxRow = rows - Math.floor((miny - exth) / mapth);

                // Adjustment
                if (startCol < 0) startCol = 0;
                if (startRow < 0) startRow = 0;
                if (maxCol > cols) maxCol = cols;
                if (maxRow > rows) maxRow = rows;
            }
        }

        let colOffset = startRow * cols, gid, grid,
            top, left, bottom, right, 
            gt, gl, gb, gr,
            axis, tileOffset, diffX1, diffY1, odd_even;

        if (layerOrientation === Orientation.HEX) {
            let hexSideLength = comp._hexSideLength;
            axis = comp._staggerAxis;
            tileOffset = comp._tileset.tileOffset;
            odd_even = (comp._staggerIndex === StaggerIndex.STAGGERINDEX_ODD) ? 1 : -1;
            diffX1 = (axis === StaggerAxis.STAGGERAXIS_X) ? ((maptw - hexSideLength)/2) : 0;
            diffY1 = (axis === StaggerAxis.STAGGERAXIS_Y) ? ((mapth - hexSideLength)/2) : 0;
        }

        let dataOffset = 0;
        let a2, b2, c2, d2, tx2, ty2, color2;
        for (let row = startRow; row < maxRow; ++row) {
            for (let col = startCol; col < maxCol; ++col) {
                let index = colOffset + col;
                let flippedX = false, flippedY = false;

                let tiledTile = tiledTiles[index];
                if (tiledTile) {
                    gid = tiledTile.gid;
                }
                else {
                    gid = comp._tiles[index];
                }
                
                grid = grids[(gid & FLIPPED_MASK) >>> 0];
                if (!grid) {
                    continue;
                }

                switch (layerOrientation) {
                    case Orientation.ORTHO:
                        left = col * maptw;
                        bottom = (rows - row - 1) * mapth;
                        break;
                    case Orientation.ISO:
                        left = maptw / 2 * ( cols + col - row - 1);
                        bottom = mapth / 2 * ( rows * 2 - col - row - 2);
                        break;
                    case Orientation.HEX:
                        let diffX2 = (axis === StaggerAxis.STAGGERAXIS_Y && row % 2 === 1) ? (maptw / 2 * odd_even) : 0;
                        left = col * (maptw - diffX1) + diffX2 + tileOffset.x;
                        let diffY2 = (axis === StaggerAxis.STAGGERAXIS_X && col % 2 === 1) ? (mapth/2 * -odd_even) : 0;
                        bottom = (rows - row - 1) * (mapth -diffY1) + diffY2 - tileOffset.y;
                        break;
                }

                if (tiledTile) {
                    let tiledNode = tiledTile.node;

                    // use tiled tile properties

                    // color
                    color2 = color;
                    let newOpacity = (tiledNode.opacity * opacity) / 255;
                    color = tiledNode.color.setA(newOpacity)._val;

                    // transform
                    a2 = a; b2 = b; c2 = c; d2 = d; tx2 = tx; ty2 = ty;
                    tiledNode._updateLocalMatrix();
                    mat4.copy(_mat4_temp, tiledNode._matrix);
                    vec3.set(_vec3_temp, -left, -bottom, 0);
                    mat4.translate(_mat4_temp, _mat4_temp, _vec3_temp);
                    mat4.multiply(_mat4_temp, node._worldMatrix, _mat4_temp);
                    a = _mat4_temp.m00; b = _mat4_temp.m01; c = _mat4_temp.m04; d = _mat4_temp.m05;
                    tx = _mat4_temp.m12; ty = _mat4_temp.m13;
                }

                right = left + tilew;
                top = bottom + tileh;

                // TMX_ORIENTATION_ISO trim
                if (enabledCulling && layerOrientation === Orientation.ISO) {
                    gb = cullingMapy + bottom*cullingD;
                    if (gb > winh+cullingH) {
                        col += Math.floor((gb-winh)*2/cullingH) - 1;
                        continue;
                    }
                    gr = cullingMapx + right*cullingA;
                    if (gr < -cullingW) {
                        col += Math.floor((-gr)*2/cullingW) - 1;
                        continue;
                    }
                    gl = cullingMapx + left*cullingA;
                    gt = cullingMapy + top*cullingD;
                    if (gl > winw || gt < 0) {
                        col = maxCol;
                        continue;
                    }
                }

                // Rotation and Flip
                if (gid > TileFlag.DIAGONAL) {
                    flippedX = (gid & TileFlag.HORIZONTAL) >>> 0;
                    flippedY = (gid & TileFlag.VERTICAL) >>> 0;
                }

                renderData.vertexCount += 4;
                renderData.indiceCount += 6;
                renderData.dataLength = renderData.vertexCount;

                // tl
                data[dataOffset].x = left * a + top * c + tx;
                data[dataOffset].y = left * b + top * d + ty;
                data[dataOffset].u = flippedX ? grid.r : grid.l;
                data[dataOffset].v = flippedY ? grid.b : grid.t;
                data[dataOffset].color = color;
                dataOffset++;

                // bl
                data[dataOffset].x = left * a + bottom * c + tx;
                data[dataOffset].y = left * b + bottom * d + ty;
                data[dataOffset].u = flippedX ? grid.r : grid.l;
                data[dataOffset].v = flippedY ? grid.t : grid.b;
                data[dataOffset].color = color;
                dataOffset++;

                // tr
                data[dataOffset].x = right * a + top * c + tx;
                data[dataOffset].y = right * b + top * d + ty;
                data[dataOffset].u = flippedX ? grid.l : grid.r;
                data[dataOffset].v = flippedY ? grid.b : grid.t;
                data[dataOffset].color = color;
                dataOffset++;

                // br
                data[dataOffset].x = right * a + bottom * c + tx;
                data[dataOffset].y = right * b + bottom * d + ty;
                data[dataOffset].u = flippedX ? grid.l : grid.r;
                data[dataOffset].v = flippedY ? grid.t : grid.b;
                data[dataOffset].color = color;
                dataOffset++;

                if (tiledTile) {
                    color = color2;
                    a = a2; b = b2; c = c2; d = d2; tx = tx2; ty = ty2;
                }
            }
            colOffset += cols;
        }
    },
};

module.exports = TiledLayer._assembler = tmxAssembler;
