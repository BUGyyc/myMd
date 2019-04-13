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

const Texture2D = require('../assets/CCTexture2D');

/**
 * cc.textureUtil is a singleton object, it can load cc.Texture2D asynchronously
 * @class textureUtil
 * @static
 */
let textureUtil = {
    loadImage (url, cb, target) {
        cc.assertID(url, 3103);

        var tex = cc.loader.getRes(url);
        if (tex) {
            if (tex.loaded) {
                cb && cb.call(target, tex);
                return tex;
            }
            else
            {
                tex.once("load", function(){
                   cb && cb.call(target, tex);
                }, target);
                return tex;
            }
        }
        else {
            tex = new Texture2D();
            tex.url = url;
            cc.loader.load({url: url, texture: tex}, function (err, texture) {
                if (err) {
                    return cb && cb.call(target, err || new Error('Unknown error'));
                }
                texture.handleLoadedTexture();
                cb && cb.call(target, null, texture);
            });
            return tex;
        }
    },

    cacheImage (url, image) {
        if (url && image) {
            var tex = new Texture2D();
            tex.initWithElement(image);
            var item = {
                id: url,
                url: url, // real download url, maybe changed
                error: null,
                content: tex,
                complete: false
            }
            cc.loader.flowOut(item);
            return tex;
        }
    },

    postLoadTexture (texture, callback) {
        if (texture.loaded) {
            callback && callback();
            return;
        }
        if (!texture.url) {
            callback && callback();
            return;
        }
        // load image
        cc.loader.load({
            url: texture.url,
            // For image, we should skip loader otherwise it will load a new texture
            skips: ['Loader'],
        }, function (err, image) {
            if (image) {
                if (CC_DEBUG && image instanceof cc.Texture2D) {
                    return cc.error('internal error: loader handle pipe must be skipped');
                }
                if (!texture.loaded) {
                    texture._nativeAsset = image;
                }
            }
            callback && callback(err);
        });
    }
};

cc.textureUtil = module.exports = textureUtil;