/****************************************************************************
 Copyright (c) 2018 Xiamen Yaji Software Co., Ltd.

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
const Label = require('../../../../components/CCLabel');
const ttf = require('./ttf');
const bmfont = require('./bmfont');

let canvasPool = {
    pool: [],
    get () {
        let data = this.pool.pop();

        if (!data) {
            let canvas = document.createElement("canvas");
            let context = canvas.getContext("2d");
            data = {
                canvas: canvas,
                context: context
            }
        }

        return data;
    },
    put (canvas) {
        if (this.pool.length >= 32) {
            return;
        }
        this.pool.push(canvas);
    }
};

Label._canvasPool = canvasPool;

module.exports = {
    getAssembler (comp) {
        let assembler = ttf;
        
        if (comp.font instanceof cc.BitmapFont) {
            assembler = bmfont;
        }

        return assembler;
    },

    createData (comp) {
        return comp._assembler.createData(comp);
    },

    draw (ctx, comp) {
        // Check whether need to render
        if (!comp._texture) {
            return 0;
        }

        let assembler = comp._assembler;
        if (!assembler) return 0;
        
        assembler.updateRenderData(comp);
        return assembler.draw(ctx, comp);
    }
};