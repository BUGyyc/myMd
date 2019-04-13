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

const Graphics = require('../../../../graphics/graphics');
const PointFlags = require('../../../../graphics/types').PointFlags;
const LineJoin = Graphics.LineJoin;
const LineCap = Graphics.LineCap;
const Earcut = require('./earcut');
const Impl = require('./impl');

const MAX_VERTEX = 65535;
const MAX_INDICE = MAX_VERTEX * 2;

const PI      = Math.PI;
const min     = Math.min;
const max     = Math.max;
const ceil    = Math.ceil;
const acos    = Math.acos;
const cos     = Math.cos;
const sin     = Math.sin;
const atan2   = Math.atan2;
const abs     = Math.abs;

let _renderData = null;
let _impl = null;
let _curColor = 0;

function curveDivs (r, arc, tol) {
    let da = acos(r / (r + tol)) * 2.0;
    return max(2, ceil(arc / da));
}

function clamp (v, min, max) {
    if (v < min) {
        return min;
    }
    else if (v > max) {
        return max;
    }
    return v;
}

let graphicsAssembler = {
    createImpl (graphics) {
        return new Impl(graphics);
    },

    updateRenderData (graphics) {
        let datas = graphics._impl.getRenderDatas();
        for (let i = 0, l = datas.length; i < l; i++) {
            datas[i].material = graphics.sharedMaterials[0];
        }
    },

    fillBuffers (graphics, renderer) {
        renderer._flush();

        let tempNode = renderer.node;
        renderer.node = graphics.node;
        this.renderIA(graphics, renderer);
        renderer.node = tempNode;
    },

    renderIA (graphics, renderer) {
        let impl = graphics._impl;
        let renderDatas = impl.getRenderDatas();
        for (let index = 0, length = renderDatas.length; index < length; index++) {
            let renderData = renderDatas[index];
            let meshbuffer = renderData.meshbuffer;
            renderData.ia._count = meshbuffer.indiceStart;
            renderer._flushIA(renderData);
            meshbuffer.uploadData();
        }
    },

    genRenderData (graphics, cverts) {
        let renderDatas = _impl.getRenderDatas(); 
        let renderData = renderDatas[_impl._dataOffset];
        let meshbuffer = renderData.meshbuffer;

        let maxVertsCount = meshbuffer.vertexStart + cverts;
        if (maxVertsCount > MAX_VERTEX ||
            maxVertsCount * 3 > MAX_INDICE) {
            ++_impl._dataOffset;
            maxVertsCount = cverts;
            
            if (_impl._dataOffset < renderDatas.length) {
                renderData = renderDatas[_impl._dataOffset];
            }
            else {
                renderData = _impl.requestRenderData(graphics);
                renderDatas[_impl._dataOffset] = renderData;
            }

            renderData.material = graphics.sharedMaterials[0];
            meshbuffer = renderData.meshbuffer;
        }

        if (maxVertsCount > meshbuffer.vertexOffset) {
            meshbuffer.requestStatic(cverts, cverts*3);
        }

        return renderData;
    },

    stroke (graphics) {
        _curColor = graphics._strokeColor._val;

        this._flattenPaths(graphics._impl);
        this._expandStroke(graphics);
    
        graphics._impl._updatePathOffset = true;
    },

    fill (graphics) {
        _curColor = graphics._fillColor._val;

        this._expandFill(graphics);
        graphics._impl._updatePathOffset = true;
    },

    _expandStroke (graphics) {
        let w = graphics.lineWidth * 0.5,
            lineCap = graphics.lineCap,
            lineJoin = graphics.lineJoin,
            miterLimit = graphics.miterLimit;

        _impl = graphics._impl;
    
        let ncap = curveDivs(w, PI, _impl._tessTol);
    
        this._calculateJoins(_impl, w, lineJoin, miterLimit);
    
        let paths = _impl._paths;
        
        // Calculate max vertex usage.
        let cverts = 0;
        for (let i = _impl._pathOffset, l = _impl._pathLength; i < l; i++) {
            let path = paths[i];
            let pointsLength = path.points.length;

            if (lineJoin === LineJoin.ROUND) cverts += (pointsLength + path.nbevel * (ncap + 2) + 1) * 2; // plus one for loop
            else cverts += (pointsLength + path.nbevel * 5 + 1) * 2; // plus one for loop

            if (!path.closed) {
                // space for caps
                if (lineCap === LineCap.ROUND) {
                    cverts += (ncap * 2 + 2) * 2;
                } else {
                    cverts += (3 + 3) * 2;
                }
            }
        }
        
        let renderData = _renderData = this.genRenderData(graphics, cverts),
            meshbuffer = renderData.meshbuffer,
            vData = meshbuffer._vData,
            iData = meshbuffer._iData;
            
        for (let i = _impl._pathOffset, l = _impl._pathLength; i < l; i++) {
            let path = paths[i];
            let pts = path.points;
            let pointsLength = pts.length;
            let offset = meshbuffer.vertexStart;

            let p0, p1;
            let start, end, loop;
            loop = path.closed;
            if (loop) {
                // Looping
                p0 = pts[pointsLength - 1];
                p1 = pts[0];
                start = 0;
                end = pointsLength;
            } else {
                // Add cap
                p0 = pts[0];
                p1 = pts[1];
                start = 1;
                end = pointsLength - 1;
            }
    
            if (!loop) {
                // Add cap
                let dPos = p1.sub(p0);
                dPos.normalizeSelf();
    
                let dx = dPos.x;
                let dy = dPos.y;
    
                if (lineCap === LineCap.BUTT)
                    this._buttCap(p0, dx, dy, w, 0);
                else if (lineCap === LineCap.SQUARE)
                    this._buttCap(p0, dx, dy, w, w);
                else if (lineCap === LineCap.ROUND)
                    this._roundCapStart(p0, dx, dy, w, ncap);
            }
    
            for (let j = start; j < end; ++j) {
                if (lineJoin === LineJoin.ROUND) {
                    this._roundJoin(p0, p1, w, w, ncap);
                }
                else if ((p1.flags & (PointFlags.PT_BEVEL | PointFlags.PT_INNERBEVEL)) !== 0) {
                    this._bevelJoin(p0, p1, w, w);
                }
                else {
                    this._vset(p1.x + p1.dmx * w, p1.y + p1.dmy * w);
                    this._vset(p1.x - p1.dmx * w, p1.y - p1.dmy * w);
                }
    
                p0 = p1;
                p1 = pts[j + 1];
            }
    
            if (loop) {
                // Loop it
                let vDataoOfset = offset * 3;
                this._vset(vData[vDataoOfset],   vData[vDataoOfset+1]);
                this._vset(vData[vDataoOfset+3], vData[vDataoOfset+4]);
            } else {
                // Add cap
                let dPos = p1.sub(p0);
                dPos.normalizeSelf();
    
                let dx = dPos.x;
                let dy = dPos.y;
    
                if (lineCap === LineCap.BUTT)
                    this._buttCap(p1, dx, dy, w, 0);
                else if (lineCap === LineCap.BUTT || lineCap === LineCap.SQUARE)
                    this._buttCap(p1, dx, dy, w, w);
                else if (lineCap === LineCap.ROUND)
                    this._roundCapEnd(p1, dx, dy, w, ncap);
            }

            // stroke indices
            let indicesOffset = meshbuffer.indiceStart;
            for (let start = offset+2, end = meshbuffer.vertexStart; start < end; start++) {
                iData[indicesOffset++] = start - 2;
                iData[indicesOffset++] = start - 1;
                iData[indicesOffset++] = start;
            }

            meshbuffer.indiceStart = indicesOffset;
        }

        _renderData = null;
        _impl = null;
    },
    
    _expandFill (graphics) {
        _impl = graphics._impl;

        let paths = _impl._paths;

        // Calculate max vertex usage.
        let cverts = 0;
        for (let i = _impl._pathOffset, l = _impl._pathLength; i < l; i++) {
            let path = paths[i];
            let pointsLength = path.points.length;

            cverts += pointsLength;
        }

        let renderData = _renderData = this.genRenderData(graphics, cverts),
            meshbuffer = renderData.meshbuffer,
            vData = meshbuffer._vData,
            iData = meshbuffer._iData;

        for (let i = _impl._pathOffset, l = _impl._pathLength; i < l; i++) {
            let path = paths[i];
            let pts = path.points;
            let pointsLength = pts.length;
    
            if (pointsLength === 0) {
                continue;
            }
    
            // Calculate shape vertices.
            let offset = meshbuffer.vertexStart;
    
            for (let j = 0; j < pointsLength; ++j) {
                this._vset(pts[j].x, pts[j].y);
            }
    
            let indicesOffset = meshbuffer.indiceStart;
    
            if (path.complex) {
                let earcutData = [];
                for (let j = offset, end = meshbuffer.vertexStart; j < end; j++) {
                    let vDataOffset = j * 3;
                    earcutData.push(vData[vDataOffset]);
                    earcutData.push(vData[vDataOffset+1]);
                }
    
                let newIndices = Earcut(earcutData, null, 2);
    
                if (!newIndices || newIndices.length === 0) {
                    continue;
                }
    
                for (let j = 0, nIndices = newIndices.length; j < nIndices; j++) {
                    iData[indicesOffset++] = newIndices[j] + offset;
                }
            }
            else {
                let first = offset;
                for (let start = offset+2, end = meshbuffer.vertexStart; start < end; start++) {
                    iData[indicesOffset++] = first;
                    iData[indicesOffset++] = start - 1;
                    iData[indicesOffset++] = start;
                }
            }

            meshbuffer.indiceStart = indicesOffset;
        }

        _renderData = null;
        _impl = null;
    },

    _calculateJoins (impl, w, lineJoin, miterLimit) {
        let iw = 0.0;
    
        if (w > 0.0) {
            iw = 1 / w;
        }
    
        // Calculate which joins needs extra vertices to append, and gather vertex count.
        let paths = impl._paths;
        for (let i = impl._pathOffset, l = impl._pathLength; i < l; i++) {
            let path = paths[i];
    
            let pts = path.points;
            let ptsLength = pts.length;
            let p0 = pts[ptsLength - 1];
            let p1 = pts[0];
            let nleft = 0;
    
            path.nbevel = 0;
    
            for (let j = 0; j < ptsLength; j++) {
                let dmr2, cross, limit;
    
                // perp normals
                let dlx0 = p0.dy;
                let dly0 = -p0.dx;
                let dlx1 = p1.dy;
                let dly1 = -p1.dx;
    
                // Calculate extrusions
                p1.dmx = (dlx0 + dlx1) * 0.5;
                p1.dmy = (dly0 + dly1) * 0.5;
                dmr2 = p1.dmx * p1.dmx + p1.dmy * p1.dmy;
                if (dmr2 > 0.000001) {
                    let scale = 1 / dmr2;
                    if (scale > 600) {
                        scale = 600;
                    }
                    p1.dmx *= scale;
                    p1.dmy *= scale;
                }
    
                // Keep track of left turns.
                cross = p1.dx * p0.dy - p0.dx * p1.dy;
                if (cross > 0) {
                    nleft++;
                    p1.flags |= PointFlags.PT_LEFT;
                }
    
                // Calculate if we should use bevel or miter for inner join.
                limit = max(11, min(p0.len, p1.len) * iw);
                if (dmr2 * limit * limit < 1) {
                    p1.flags |= PointFlags.PT_INNERBEVEL;
                }
    
                // Check to see if the corner needs to be beveled.
                if (p1.flags & PointFlags.PT_CORNER) {
                    if (dmr2 * miterLimit * miterLimit < 1 || lineJoin === LineJoin.BEVEL || lineJoin === LineJoin.ROUND) {
                        p1.flags |= PointFlags.PT_BEVEL;
                    }
                }
    
                if ((p1.flags & (PointFlags.PT_BEVEL | PointFlags.PT_INNERBEVEL)) !== 0) {
                    path.nbevel++;
                }
    
                p0 = p1;
                p1 = pts[j + 1];
            }
        }
    },
    
    _flattenPaths (impl) {
        let paths = impl._paths;
        for (let i = impl._pathOffset, l = impl._pathLength; i < l; i++) {
            let path = paths[i];
            let pts = path.points;
    
            let p0 = pts[pts.length - 1];
            let p1 = pts[0];
    
            if (p0.equals(p1)) {
                path.closed = true;
                pts.pop();
                p0 = pts[pts.length - 1];
            }
    
            for (let j = 0, size = pts.length; j < size; j++) {
                // Calculate segment direction and length
                let dPos = p1.sub(p0);
                p0.len = dPos.mag();
                if (dPos.x || dPos.y)
                    dPos.normalizeSelf();
                p0.dx = dPos.x;
                p0.dy = dPos.y;
                // Advance
                p0 = p1;
                p1 = pts[j + 1];
            }
        }
    },

    _chooseBevel (bevel, p0, p1, w) {
        let x = p1.x;
        let y = p1.y;
        let x0, y0, x1, y1;
    
        if (bevel !== 0) {
            x0 = x + p0.dy * w;
            y0 = y - p0.dx * w;
            x1 = x + p1.dy * w;
            y1 = y - p1.dx * w;
        } else {
            x0 = x1 = x + p1.dmx * w;
            y0 = y1 = y + p1.dmy * w;
        }
    
        return [x0, y0, x1, y1];
    },
    
    _buttCap (p, dx, dy, w, d) {
        let px = p.x - dx * d;
        let py = p.y - dy * d;
        let dlx = dy;
        let dly = -dx;
    
        this._vset(px + dlx * w, py + dly * w);
        this._vset(px - dlx * w, py - dly * w);
    },
    
    _roundCapStart (p, dx, dy, w, ncap) {
        let px = p.x;
        let py = p.y;
        let dlx = dy;
        let dly = -dx;
    
        for (let i = 0; i < ncap; i++) {
            let a = i / (ncap - 1) * PI;
            let ax = cos(a) * w,
                ay = sin(a) * w;
            this._vset(px - dlx * ax - dx * ay, py - dly * ax - dy * ay);
            this._vset(px, py);
        }
        this._vset(px + dlx * w, py + dly * w);
        this._vset(px - dlx * w, py - dly * w);
    },
    
    _roundCapEnd (p, dx, dy, w, ncap) {
        let px = p.x;
        let py = p.y;
        let dlx = dy;
        let dly = -dx;
    
        this._vset(px + dlx * w, py + dly * w);
        this._vset(px - dlx * w, py - dly * w);
        for (let i = 0; i < ncap; i++) {
            let a = i / (ncap - 1) * PI;
            let ax = cos(a) * w,
                ay = sin(a) * w;
            this._vset(px, py);
            this._vset(px - dlx * ax + dx * ay, py - dly * ax + dy * ay);
        }
    },
    
    _roundJoin (p0, p1, lw, rw, ncap) {
        let dlx0 = p0.dy;
        let dly0 = -p0.dx;
        let dlx1 = p1.dy;
        let dly1 = -p1.dx;
    
        let p1x = p1.x;
        let p1y = p1.y;
    
        if ((p1.flags & PointFlags.PT_LEFT) !== 0) {
            let out = this._chooseBevel(p1.flags & PointFlags.PT_INNERBEVEL, p0, p1, lw);
            let lx0 = out[0];
            let ly0 = out[1];
            let lx1 = out[2];
            let ly1 = out[3];
    
            let a0 = atan2(-dly0, -dlx0);
            let a1 = atan2(-dly1, -dlx1);
            if (a1 > a0) a1 -= PI * 2;
    
            this._vset(lx0, ly0);
            this._vset(p1x - dlx0 * rw, p1.y - dly0 * rw);
    
            let n = clamp(ceil((a0 - a1) / PI) * ncap, 2, ncap);
            for (let i = 0; i < n; i++) {
                let u = i / (n - 1);
                let a = a0 + u * (a1 - a0);
                let rx = p1x + cos(a) * rw;
                let ry = p1y + sin(a) * rw;
                this._vset(p1x, p1y);
                this._vset(rx, ry);
            }
    
            this._vset(lx1, ly1);
            this._vset(p1x - dlx1 * rw, p1y - dly1 * rw);
        } else {
            let out = this._chooseBevel(p1.flags & PointFlags.PT_INNERBEVEL, p0, p1, -rw);
            let rx0 = out[0];
            let ry0 = out[1];
            let rx1 = out[2];
            let ry1 = out[3];
    
            let a0 = atan2(dly0, dlx0);
            let a1 = atan2(dly1, dlx1);
            if (a1 < a0) a1 += PI * 2;
    
            this._vset(p1x + dlx0 * rw, p1y + dly0 * rw);
            this._vset(rx0, ry0);
    
            let n = clamp(ceil((a1 - a0) / PI) * ncap, 2, ncap);
            for (let i = 0; i < n; i++) {
                let u = i / (n - 1);
                let a = a0 + u * (a1 - a0);
                let lx = p1x + cos(a) * lw;
                let ly = p1y + sin(a) * lw;
                this._vset(lx, ly);
                this._vset(p1x, p1y);
            }
    
            this._vset(p1x + dlx1 * rw, p1y + dly1 * rw);
            this._vset(rx1, ry1);
        }
    },
    
    _bevelJoin (p0, p1, lw, rw) {
        let rx0, ry0, rx1, ry1;
        let lx0, ly0, lx1, ly1;
        let dlx0 = p0.dy;
        let dly0 = -p0.dx;
        let dlx1 = p1.dy;
        let dly1 = -p1.dx;
    
        if (p1.flags & PointFlags.PT_LEFT) {
            let out = this._chooseBevel(p1.flags & PointFlags.PT_INNERBEVEL, p0, p1, lw);
            lx0 = out[0];
            ly0 = out[1];
            lx1 = out[2];
            ly1 = out[3];
    
            this._vset(lx0, ly0);
            this._vset(p1.x - dlx0 * rw, p1.y - dly0 * rw);
    
            this._vset(lx1, ly1);
            this._vset(p1.x - dlx1 * rw, p1.y - dly1 * rw);
        } else {
            let out = this._chooseBevel(p1.flags & PointFlags.PT_INNERBEVEL, p0, p1, -rw);
            rx0 = out[0];
            ry0 = out[1];
            rx1 = out[2];
            ry1 = out[3];
    
            this._vset(p1.x + dlx0 * lw, p1.y + dly0 * lw);
            this._vset(rx0, ry0);
    
            this._vset(p1.x + dlx1 * lw, p1.y + dly1 * lw);
            this._vset(rx1, ry1);
        }
    },
    
    _vset (x, y) {
        let meshbuffer = _renderData.meshbuffer;
        let dataOffset = meshbuffer.vertexStart * 3;

        let vData = meshbuffer._vData;
        let uintVData = meshbuffer._uintVData;

        vData[dataOffset] = x;
        vData[dataOffset+1] = y;
        uintVData[dataOffset+2] = _curColor;

        meshbuffer.vertexStart ++;
        meshbuffer._dirty = true;
    }
};

Graphics._assembler = graphicsAssembler;

module.exports = graphicsAssembler;