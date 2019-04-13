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

const EventTarget = require('../event/event-target');
const renderer = require('../renderer');
require('../platform/CCClass');

import gfx from '../../renderer/gfx';

const GL_NEAREST = 9728;                // gl.NEAREST
const GL_LINEAR = 9729;                 // gl.LINEAR
const GL_REPEAT = 10497;                // gl.REPEAT
const GL_CLAMP_TO_EDGE = 33071;         // gl.CLAMP_TO_EDGE
const GL_MIRRORED_REPEAT = 33648;       // gl.MIRRORED_REPEAT
const GL_RGBA = 6408;                   // gl.RGBA

const CHAR_CODE_0 = 48;    // '0'
const CHAR_CODE_1 = 49;    // '1'

var idGenerater = new (require('../platform/id-generater'))('Tex');

/**
 * <p>
 * This class allows to easily create OpenGL or Canvas 2D textures from images, text or raw data.                                    <br/>
 * The created cc.Texture2D object will always have power-of-two dimensions.                                                <br/>
 * Depending on how you create the cc.Texture2D object, the actual image area of the texture might be smaller than the texture dimensions <br/>
 *  i.e. "contentSize" != (pixelsWide, pixelsHigh) and (maxS, maxT) != (1.0, 1.0).                                           <br/>
 * Be aware that the content of the generated textures will be upside-down! </p>

 * @class Texture2D
 * @uses EventTarget
 * @extends Asset
 */

/**
 * The texture pixel format, default value is RGBA8888, 
 * you should note that textures loaded by normal image files (png, jpg) can only support RGBA8888 format,
 * other formats are supported by compressed file types or raw data.
 * @enum Texture2D.PixelFormat
 */
const PixelFormat = cc.Enum({
    /**
     * 16-bit texture without Alpha channel
     * @property RGB565
     * @readonly
     * @type {Number}
     */
    RGB565: gfx.TEXTURE_FMT_R5_G6_B5,
    /**
     * 16-bit textures: RGB5A1
     * @property RGB5A1
     * @readonly
     * @type {Number}
     */
    RGB5A1: gfx.TEXTURE_FMT_R5_G5_B5_A1,
    /**
     * 16-bit textures: RGBA4444
     * @property RGBA4444
     * @readonly
     * @type {Number}
     */
    RGBA4444: gfx.TEXTURE_FMT_R4_G4_B4_A4,
    /**
     * 24-bit texture: RGB888
     * @property RGB888
     * @readonly
     * @type {Number}
     */
    RGB888: gfx.TEXTURE_FMT_RGB8,
    /**
     * 32-bit texture: RGBA8888
     * @property RGBA8888
     * @readonly
     * @type {Number}
     */
    RGBA8888: gfx.TEXTURE_FMT_RGBA8,
    /**
     * 32-bit float texture: RGBA32F
     * @property RGBA32F
     * @readonly
     * @type {Number}
     */
    RGBA32F: gfx.TEXTURE_FMT_RGBA32F,
    /**
     * 8-bit textures used as masks
     * @property A8
     * @readonly
     * @type {Number}
     */
    A8: gfx.TEXTURE_FMT_A8,
    /**
     * 8-bit intensity texture
     * @property I8
     * @readonly
     * @type {Number}
     */
    I8: gfx.TEXTURE_FMT_L8,
    /**
     * 16-bit textures used as masks
     * @property AI88
     * @readonly
     * @type {Number}
     */
    AI8: gfx.TEXTURE_FMT_L8_A8,

    /**
     * rgb 2 bpp pvrtc
     * @property RGB_PVRTC_2BPPV1
     * @readonly
     * @type {Number}
     */
    RGB_PVRTC_2BPPV1: gfx.TEXTURE_FMT_RGB_PVRTC_2BPPV1,
    /**
     * rgba 2 bpp pvrtc
     * @property RGBA_PVRTC_2BPPV1
     * @readonly
     * @type {Number}
     */
    RGBA_PVRTC_2BPPV1: gfx.TEXTURE_FMT_RGBA_PVRTC_2BPPV1,
    /**
     * rgb 4 bpp pvrtc
     * @property RGB_PVRTC_4BPPV1
     * @readonly
     * @type {Number}
     */
    RGB_PVRTC_4BPPV1: gfx.TEXTURE_FMT_RGB_PVRTC_4BPPV1,
    /**
     * rgba 4 bpp pvrtc
     * @property RGBA_PVRTC_4BPPV1
     * @readonly
     * @type {Number}
     */
    RGBA_PVRTC_4BPPV1: gfx.TEXTURE_FMT_RGBA_PVRTC_4BPPV1,
    /**
     * rgb etc1
     * @property RGB_ETC1
     * @readonly
     * @type {Number}
     */
    RGB_ETC1: gfx.TEXTURE_FMT_RGB_ETC1,
    /**
     * rgba etc1
     * @property RGBA_ETC1
     * @readonly
     * @type {Number}
     */
    // gfx do not have a standard definition for RGBA_ETC1, need define a specified number for it.
    RGBA_ETC1: 1024,

    /**
     * rgb etc2
     * @property RGB_ETC2
     * @readonly
     * @type {Number}
     */
    RGB_ETC2: gfx.TEXTURE_FMT_RGB_ETC2,
    /**
     * rgba etc2
     * @property RGBA_ETC2
     * @readonly
     * @type {Number}
     */
    RGBA_ETC2: gfx.TEXTURE_FMT_RGBA_ETC2,
});

/**
 * The texture wrap mode
 * @enum Texture2D.WrapMode
 */
const WrapMode = cc.Enum({
    /**
     * The constant variable equals gl.REPEAT for texture
     * @property REPEAT
     * @type {Number}
     * @readonly
     */
    REPEAT: GL_REPEAT,
    /**
     * The constant variable equals gl.CLAMP_TO_EDGE for texture
     * @property CLAMP_TO_EDGE
     * @type {Number}
     * @readonly
     */
    CLAMP_TO_EDGE: GL_CLAMP_TO_EDGE,
    /**
     * The constant variable equals gl.MIRRORED_REPEAT for texture
     * @property MIRRORED_REPEAT
     * @type {Number}
     * @readonly
     */
    MIRRORED_REPEAT: GL_MIRRORED_REPEAT
});

/**
 * The texture filter mode
 * @enum Texture2D.Filter
 */
const Filter = cc.Enum({
    /**
     * The constant variable equals gl.LINEAR for texture
     * @property LINEAR
     * @type {Number}
     * @readonly
     */
    LINEAR: GL_LINEAR,
    /**
     * The constant variable equals gl.NEAREST for texture
     * @property NEAREST
     * @type {Number}
     * @readonly
     */
    NEAREST: GL_NEAREST
});

const FilterIndex = {
    9728: 0, // GL_NEAREST
    9729: 1, // GL_LINEAR
};

let _images = [];
let _sharedOpts = {
    width: undefined,
    height: undefined,
    minFilter: undefined,
    magFilter: undefined,
    wrapS: undefined,
    wrapT: undefined,
    format: undefined,
    mipmap: undefined,
    images: undefined,
    image: undefined,
    flipY: undefined,
    premultiplyAlpha: undefined
};
function _getSharedOptions () {
    for (var key in _sharedOpts) {
        _sharedOpts[key] = undefined;
    }
    _images.length = 0;
    _sharedOpts.images = _images;
    _sharedOpts.flipY = false;
    return _sharedOpts;
}

/**
 * This class allows to easily create OpenGL or Canvas 2D textures from images or raw data.
 *
 * @class Texture2D
 * @uses EventTarget
 * @extends Asset
 */
var Texture2D = cc.Class({
    name: 'cc.Texture2D',
    extends: require('../assets/CCAsset'),
    mixins: [EventTarget],

    properties: {
        _nativeAsset: {
            get () {
                // maybe returned to pool in webgl
                return this._image;
            },
            set (data) {
                if (data._compressed && data._data) {
                    this.initWithData(data._data, this._format, data.width, data.height);
                }
                else {
                    this.initWithElement(data);
                }
            },
            override: true
        },
        _hasMipmap: false,
        _format: PixelFormat.RGBA8888,
        _premultiplyAlpha: false,
        _flipY: false,
        _minFilter: Filter.LINEAR,
        _magFilter: Filter.LINEAR,
        _mipFilter: Filter.LINEAR,
        _wrapS: WrapMode.CLAMP_TO_EDGE,
        _wrapT: WrapMode.CLAMP_TO_EDGE,
    },

    statics: {
        PixelFormat: PixelFormat,
        WrapMode: WrapMode,
        Filter: Filter,
        _FilterIndex: FilterIndex,

        // predefined most common extnames
        extnames: ['.png', '.jpg', '.jpeg', '.bmp', '.webp', '.pvr', '.pkm'],
    },

    ctor () {
        // Id for generate hash in material
        this._id = idGenerater.getNewId();
        
        /**
         * !#en
         * The url of the texture, this could be empty if the texture wasn't created via a file.
         * !#zh
         * 贴图文件的 url，当贴图不是由文件创建时值可能为空
         * @property url
         * @type {String}
         * @readonly
         */
        // TODO - use nativeUrl directly
        this.url = "";

        /**
         * !#en
         * Whether the texture is loaded or not
         * !#zh
         * 贴图是否已经成功加载
         * @property loaded
         * @type {Boolean}
         */
        this.loaded = false;
        /**
         * !#en
         * Texture width in pixel
         * !#zh
         * 贴图像素宽度
         * @property width
         * @type {Number}
         */
        this.width = 0;
        /**
         * !#en
         * Texture height in pixel
         * !#zh
         * 贴图像素高度
         * @property height
         * @type {Number}
         */
        this.height = 0;

        this._hashDirty = true;
        this._hash = 0;
        this._texture = null;
        
        if (CC_EDITOR) {
            this._exportedExts = null;
        }
    },

    /**
     * !#en
     * Get renderer texture implementation object
     * extended from render.Texture2D
     * !#zh  返回渲染器内部贴图对象
     * @method getImpl
     */
    getImpl () {
        return this._texture;
    },

    getId () {
        return this._id;
    },

    toString () {
        return this.url || '';
    },

    /**
     * Update texture options, not available in Canvas render mode.
     * image, format, premultiplyAlpha can not be updated in native.
     * @method update
     * @param {Object} options
     * @param {DOMImageElement} options.image
     * @param {Boolean} options.mipmap
     * @param {PixelFormat} options.format
     * @param {Filter} options.minFilter
     * @param {Filter} options.magFilter
     * @param {WrapMode} options.wrapS
     * @param {WrapMode} options.wrapT
     * @param {Boolean} options.premultiplyAlpha
     */
    update (options) {
        if (options) {
            let updateImg = false;
            if (options.width !== undefined) {
                this.width = options.width;
            }
            if (options.height !== undefined) {
                this.height = options.height;
            }
            if (options.minFilter !== undefined) {
                this._minFilter = options.minFilter;
                options.minFilter = FilterIndex[options.minFilter];
            }
            if (options.magFilter !== undefined) {
                this._magFilter = options.magFilter;
                options.magFilter = FilterIndex[options.magFilter];
            }
            if (options.mipFilter !== undefined) {
                this._mipFilter = options.mipFilter;
                options.mipFilter = FilterIndex[options.mipFilter];
            }
            if (options.wrapS !== undefined) {
                this._wrapS = options.wrapS;
            }
            if (options.wrapT !== undefined) {
                this._wrapT = options.wrapT;
            }
            if (options.format !== undefined) {
                this._format = options.format;
            }
            if (options.flipY !== undefined) {
                this._flipY = options.flipY;
                updateImg = true;
            }
            if (options.premultiplyAlpha !== undefined) {
                this._premultiplyAlpha = options.premultiplyAlpha;
                updateImg = true;
            }
            if (options.mipmap !== undefined) {
                this._hasMipmap = options.mipmap;
            }

            if (updateImg && this._image) {
                options.image = this._image;
            }
            if (options.images && options.images.length > 0) {
                this._image = options.images[0];
            }
            else if (options.image !== undefined) {
                this._image = options.image;
                if (!options.images) {
                    _images.length = 0;
                    options.images = _images;
                }
                // webgl texture 2d uses images
                options.images.push(options.image);
            }

            if (options.images && options.images.length > 0) {
                this._texture.update(options);
            }

            this._hashDirty = true;
        }
    },

    /**
     * !#en
     * Init with HTML element.
     * !#zh 用 HTML Image 或 Canvas 对象初始化贴图。
     * @method initWithElement
     * @param {HTMLImageElement|HTMLCanvasElement} element
     * @example
     * var img = new Image();
     * img.src = dataURL;
     * texture.initWithElement(img);
     */
    initWithElement (element) {
        if (!element)
            return;
        this._image = element;
        if (CC_WECHATGAME || CC_QQPLAY || element.complete || element instanceof HTMLCanvasElement) {
            this.handleLoadedTexture();
        }
        else {
            var self = this;
            element.addEventListener('load', function () {
                self.handleLoadedTexture();
            });
            element.addEventListener('error', function (err) {
                cc.warnID(3119, err.message);
            });
        }
    },

    /**
     * !#en
     * Intializes with a texture2d with data in Uint8Array.
     * !#zh 使用一个存储在 Unit8Array 中的图像数据（raw data）初始化数据。
     * @method initWithData
     * @param {TypedArray} data
     * @param {Number} pixelFormat
     * @param {Number} pixelsWidth
     * @param {Number} pixelsHeight
     * @return {Boolean}
     */
    initWithData (data, pixelFormat, pixelsWidth, pixelsHeight) {
        var opts = _getSharedOptions();
        opts.image = data;
        // webgl texture 2d uses images
        opts.images = [opts.image];
        opts.hasMipmap = this._hasMipmap;
        opts.premultiplyAlpha = this._premultiplyAlpha;
        opts.flipY = this._flipY;
        opts.minFilter = FilterIndex[this._minFilter];
        opts.magFilter = FilterIndex[this._magFilter];
        opts.wrapS = this._wrapS;
        opts.wrapT = this._wrapT;
        opts.format = pixelFormat;
        if (pixelFormat === PixelFormat.RGBA_ETC1) {
            opts.format = PixelFormat.RGB_ETC1;
        }
        opts.width = pixelsWidth;
        opts.height = pixelsHeight;
        if (!this._texture) {
            this._texture = new renderer.Texture2D(renderer.device, opts);
        }
        else {
            this._texture.update(opts);
        }
        this.width = pixelsWidth;
        this.height = pixelsHeight;
        this.loaded = true;
        this.emit("load");
        return true;
    },

    /**
     * !#en
     * HTMLElement Object getter, available only on web.
     * !#zh 获取当前贴图对应的 HTML Image 或 Canvas 对象，只在 Web 平台下有效。
     * @method getHtmlElementObj
     * @return {HTMLImageElement|HTMLCanvasElement}
     */
    getHtmlElementObj () {
        return this._image;
    },
    
    /**
     * !#en
     * Destory this texture and immediately release its video memory. (Inherit from cc.Object.destroy)<br>
     * After destroy, this object is not usable any more.
     * You can use cc.isValid(obj) to check whether the object is destroyed before accessing it.
     * !#zh
     * 销毁该贴图，并立即释放它对应的显存。（继承自 cc.Object.destroy）<br/>
     * 销毁后，该对象不再可用。您可以在访问对象之前使用 cc.isValid(obj) 来检查对象是否已被销毁。
     * @method destroy
     * @return {Boolean} inherit from the CCObject
     */
    destroy () {
        this._image = null;
        this._texture && this._texture.destroy();
        // TODO cc.textureUtil ?
        // cc.textureCache.removeTextureForKey(this.url);  // item.rawUrl || item.url
        this._super();
    },

    /**
     * !#en
     * Pixel format of the texture.
     * !#zh 获取纹理的像素格式。
     * @method getPixelFormat
     * @return {Number}
     */
    getPixelFormat () {
        //support only in WebGl rendering mode
        return this._format;
    },

    /**
     * !#en
     * Whether or not the texture has their Alpha premultiplied.
     * !#zh 检查纹理在上传 GPU 时预乘选项是否开启。
     * @method hasPremultipliedAlpha
     * @return {Boolean}
     */
    hasPremultipliedAlpha () {
        return this._premultiplyAlpha || false;
    },

    /**
     * !#en
     * Whether or not use mipmap.
     * !#zh 检查问题在上传 GPU 时是否生成 mipmap。
     * @method hasMipmap
     * @return {Boolean}
     */
    hasMipmap () {
        return this._hasMipmap || false;
    },

    /**
     * !#en
     * Handler of texture loaded event.
     * Since v2.0, you don't need to invoke this function, it will be invoked automatically after texture loaded.
     * !#zh 贴图加载事件处理器。v2.0 之后你将不在需要手动执行这个函数，它会在贴图加载成功之后自动执行。
     * @method handleLoadedTexture
     * @param {Boolean} [premultiplied]
     */
    handleLoadedTexture () {
        if (!this._image || !this._image.width || !this._image.height)
            return;
        
        this.width = this._image.width;
        this.height = this._image.height;
        let opts = _getSharedOptions();
        opts.image = this._image;
        // webgl texture 2d uses images
        opts.images = [opts.image];
        opts.width = this.width;
        opts.height = this.height;
        opts.hasMipmap = this._hasMipmap;
        opts.format = this._format;
        if (this._format === PixelFormat.RGBA_ETC1) {
            opts.format = PixelFormat.RGB_ETC1;
        }
        opts.premultiplyAlpha = this._premultiplyAlpha;
        opts.flipY = this._flipY;
        opts.minFilter = FilterIndex[this._minFilter];
        opts.magFilter = FilterIndex[this._magFilter];
        opts.wrapS = this._wrapS;
        opts.wrapT = this._wrapT;
        
        if (!this._texture) {
            this._texture = new renderer.Texture2D(renderer.device, opts);
        }
        else {
            this._texture.update(opts);
        }

        //dispatch load event to listener.
        this.loaded = true;
        this.emit("load");

        if (cc.macro.CLEANUP_IMAGE_CACHE && this._image instanceof HTMLImageElement) {
            this._clearImage();
        }
    },

    /**
     * !#en
     * Description of cc.Texture2D.
     * !#zh cc.Texture2D 描述。
     * @method description
     * @returns {String}
     */
    description () {
        return "<cc.Texture2D | Name = " + this.url + " | Dimensions = " + this.width + " x " + this.height + ">";
    },

    /**
     * !#en
     * Release texture, please use destroy instead.
     * !#zh 释放纹理，请使用 destroy 替代。
     * @method releaseTexture
     * @deprecated since v2.0
     */
    releaseTexture () {
        this._image = null;
        this._texture && this._texture.destroy();
    },

    /**
     * !#en Sets the wrap s and wrap t options. <br/>
     * If the texture size is NPOT (non power of 2), then in can only use gl.CLAMP_TO_EDGE in gl.TEXTURE_WRAP_{S,T}.
     * !#zh 设置纹理包装模式。
     * 若纹理贴图尺寸是 NPOT（non power of 2），则只能使用 Texture2D.WrapMode.CLAMP_TO_EDGE。
     * @method setTexParameters
     * @param {Texture2D.WrapMode} wrapS
     * @param {Texture2D.WrapMode} wrapT
     */
    setWrapMode (wrapS, wrapT) {
        if (this._wrapS !== wrapS || this._wrapT !== wrapT) {
            var opts = _getSharedOptions();
            opts.wrapS = wrapS;
            opts.wrapT = wrapT;
            this.update(opts);
        }
    },

    /**
     * !#en Sets the minFilter and magFilter options
     * !#zh 设置纹理贴图缩小和放大过滤器算法选项。
     * @method setFilters
     * @param {Texture2D.Filter} minFilter
     * @param {Texture2D.Filter} magFilter
     */
    setFilters (minFilter, magFilter) {
        if (this._minFilter !== minFilter || this._magFilter !== magFilter) {
            var opts = _getSharedOptions();
            opts.minFilter = minFilter;
            opts.magFilter = magFilter;
            this.update(opts);
        }
    },

    /**
     * !#en
     * Sets the flipY options
     * !#zh 设置贴图的纵向翻转选项。
     * @method setFlipY
     * @param {Boolean} flipY
     */
    setFlipY (flipY) {
        if (this._flipY !== flipY) {
            var opts = _getSharedOptions();
            opts.flipY = flipY;
            this.update(opts);
        }
    },

    /**
     * !#en
     * Sets the premultiply alpha options
     * !#zh 设置贴图的预乘选项。
     * @method setPremultiplyAlpha
     * @param {Boolean} premultiply
     */
    setPremultiplyAlpha (premultiply) {
        if (this._premultiplyAlpha !== premultiply) {
            var opts = _getSharedOptions();
            opts.premultiplyAlpha = premultiply;
            this.update(opts);
        }
    },
    
    /**
     * !#en
     * Sets whether generate mipmaps for the texture
     * !#zh 是否为纹理设置生成 mipmaps。
     * @method setMipmap
     * @param {Boolean} mipmap
     */
    setMipmap (mipmap) {
        if (this._hasMipmap !== mipmap) {
            var opts = _getSharedOptions();
            opts.hasMipmap = mipmap;
            this.update(opts);
        }
    },

    _getOpts() {
        let opts = _getSharedOptions();
        opts.width = this.width;
        opts.height = this.height;
        opts.mipmap = this._genMipmap;
        opts.format = this._format;
        opts.premultiplyAlpha = this._premultiplyAlpha;
        opts.anisotropy = this._anisotropy;
        opts.flipY = this._flipY;
        opts.minFilter = FilterIndex[this._minFilter];
        opts.magFilter = FilterIndex[this._magFilter];
        opts.mipFilter = FilterIndex[this._mipFilter];
        opts.wrapS = this._wrapS;
        opts.wrapT = this._wrapT;
        return opts;
    },

    _resetUnderlyingMipmaps(mipmapSources) {
        const opts = this._getOpts();
        opts.images = mipmapSources || [null];
        if (!this._texture) {
            this._texture = new renderer.Texture2D(renderer.device, opts);
        } else {
            this._texture.update(opts);
        }
    },

    // SERIALIZATION

    _serialize: (CC_EDITOR || CC_TEST) && function () {
        let extId = "";
        let exportedExts = this._exportedExts;
        if (!exportedExts && this._native) {
            exportedExts = [this._native];
        }
        if (exportedExts) {
            let exts = [];
            for (let i = 0; i < exportedExts.length; i++) {
                let extId = "";
                let ext = exportedExts[i]
                if (ext) {
                    // ext@format
                    let extFormat = ext.split('@');
                    extId = Texture2D.extnames.indexOf(extFormat[0]);
                    if (extId < 0) {
                        extId = ext;
                    }
                    if (extFormat[1]) {
                        extId += '@' + extFormat[1];
                    }
                }
                exts.push(extId);
            }
            extId = exts.join('_');
        }
        let asset = "" + extId + "," + 
                    this._minFilter + "," + this._magFilter + "," + 
                    this._wrapS + "," + this._wrapT + "," + 
                    (this._premultiplyAlpha ? 1 : 0);
        return asset;
    },

    _deserialize: function (data, handle) {
        let device = cc.renderer.device;

        let fields = data.split(',');
        // decode extname
        let extIdStr = fields[0];
        if (extIdStr) {
            let extIds = extIdStr.split('_');

            let extId = 999;
            let ext = '';
            let format = this._format;
            let SupportTextureFormats = cc.macro.SUPPORT_TEXTURE_FORMATS;
            for (let i = 0; i < extIds.length; i++) {
                let extFormat = extIds[i].split('@');
                let tmpExt = extFormat[0];
                tmpExt = tmpExt.charCodeAt(0) - CHAR_CODE_0;
                tmpExt = Texture2D.extnames[tmpExt] || extFormat;

                let index = SupportTextureFormats.indexOf(tmpExt);
                if (index !== -1 && index < extId) {
                    
                    let tmpFormat = extFormat[1] ? parseInt(extFormat[1]) : this._format;

                    // check whether or not support compressed texture
                    if ( tmpExt === '.pvr' && !device.ext('WEBGL_compressed_texture_pvrtc')) {
                        continue;
                    }
                    else if ((tmpFormat === PixelFormat.RGB_ETC1 || tmpFormat === PixelFormat.RGBA_ETC1) && !device.ext('WEBGL_compressed_texture_etc1')) {
                        continue;
                    }
                    else if ((tmpFormat === PixelFormat.RGB_ETC2 || tmpFormat === PixelFormat.RGBA_ETC2) && !device.ext('WEBGL_compressed_texture_etc')) {
                        continue;
                    }

                    extId = index;
                    ext = tmpExt;
                    format = tmpFormat;
                }
            }

            if (ext) {
                this._setRawAsset(ext);
                this._format = format;
            }

            // preset uuid to get correct nativeUrl
            let loadingItem = handle.customEnv;
            let uuid = loadingItem && loadingItem.uuid;
            if (uuid) {
                this._uuid = uuid;
                var url = this.nativeUrl;
                this.url = url;
            }
        }
        if (fields.length === 6) {
            // decode filters
            this._minFilter = parseInt(fields[1]);
            this._magFilter = parseInt(fields[2]);
            // decode wraps
            this._wrapS = parseInt(fields[3]);
            this._wrapT = parseInt(fields[4]);
            // decode premultiply alpha
            this._premultiplyAlpha = fields[5].charCodeAt(0) === CHAR_CODE_1;
        }
    },

    _getHash () {
        if (!this._hashDirty) {
            return this._hash;
        }
        let hasMipmap = this._hasMipmap ? 1 : 0;
        let premultiplyAlpha = this._premultiplyAlpha ? 1 : 0;
        let flipY = this._flipY ? 1 : 0;
        let minFilter = this._minFilter === Filter.LINEAR ? 1 : 2;
        let magFilter = this._magFilter === Filter.LINEAR ? 1 : 2;
        let wrapS = this._wrapS === WrapMode.REPEAT ? 1 : (this._wrapS === WrapMode.CLAMP_TO_EDGE ? 2 : 3);
        let wrapT = this._wrapT === WrapMode.REPEAT ? 1 : (this._wrapT === WrapMode.CLAMP_TO_EDGE ? 2 : 3);
        let pixelFormat = this._format;
        let image = this._image;
        if (CC_JSB && image) {
            if (image._glFormat !== GL_RGBA)
                pixelFormat = 0;
            premultiplyAlpha = image._premultiplyAlpha;
        }

        this._hash = Number(`${minFilter}${magFilter}${pixelFormat}${wrapS}${wrapT}${hasMipmap}${premultiplyAlpha}${flipY}`);
        this._hashDirty = false;
        return this._hash;
    },

    _isCompressed () {
        return this._texture && this._texture._compressed;
    },
    
    _clearImage () {
        // wechat game platform will cache image parsed data, 
        // so image will consume much more memory than web, releasing it
        // Release image in loader cache
        // native image element has not image.id, release by image.src.
        cc.loader.removeItem(this._image.id || this._image.src);
        this._image.src = "";
    }
});

/**
 * !#zh
 * 当该资源加载成功后触发该事件
 * !#en
 * This event is emitted when the asset is loaded
 *
 * @event load
 */

cc.Texture2D = module.exports = Texture2D;
