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

const utils = require('../../platform/utils');
const macro = require('../../platform/CCMacro');
const Types = require('./types');
const Label = require('../CCLabel');

const EditBox = cc.EditBox;
const js = cc.js;
const InputMode = Types.InputMode;
const InputFlag = Types.InputFlag;
const KeyboardReturnType = Types.KeyboardReturnType;
const math = cc.vmath;

// polyfill
let polyfill = {
    zoomInvalid: false
};

if (cc.sys.OS_ANDROID === cc.sys.os &&
    (cc.sys.browserType === cc.sys.BROWSER_TYPE_SOUGOU ||
    cc.sys.browserType === cc.sys.BROWSER_TYPE_360)) {
    polyfill.zoomInvalid = true;
}

// https://segmentfault.com/q/1010000002914610
const DELAY_TIME = 800;
const SCROLLY = 100;
const LEFT_PADDING = 2;

 // private static property
 let _domCount = 0;
 let _vec3 = cc.v3();
 let _currentEditBoxImpl = null;


// on mobile
let _fullscreen = false;
let _autoResize = false;

 // This is an adapter for EditBoxImpl on web platform.
 // For more adapters on other platforms, please inherit from EditBoxImplBase and implement the interface.
function WebEditBoxImpl () {
    this._domId = `EditBoxId_${++_domCount}`;
    this._placeholderStyleSheet = null;
    this._elem = null;
    this._isTextArea = false;
    this._editing = false;

    // matrix
    this._worldMat = math.mat4.create();
    this._cameraMat = math.mat4.create();
    // matrix cache
    this._m00 = 0;
    this._m01 = 0;
    this._m04 = 0;
    this._m05 = 0;
    this._m12 = 0;
    this._m13 = 0;
    this._w = 0;
    this._h = 0;

    // inputType cache
    this._inputMode = null;
    this._inputFlag = null;
    this._returnType = null;

    // event listeners
    this._eventListeners = {};

    // update style sheet cache
    this._textLabelFont = null;
    this._textLabelFontSize = null;
    this._textLabelFontColor = null;
    this._textLabelAlign = null;

    this._placeholderLabelFont = null;
    this._placeholderLabelFontSize = null;
    this._placeholderLabelFontColor = null;
    this._placeholderLabelAlign = null;
    this._placeholderLineHeight = null;
}

js.extend(WebEditBoxImpl, EditBox._ImplClass);
EditBox._ImplClass = WebEditBoxImpl;

Object.assign(WebEditBoxImpl.prototype, {
    // =================================
    // implement EditBoxImplBase interface
    init (delegate) {
        if (!delegate) {
            return;
        }

        this._delegate = delegate;

        if (delegate.inputMode === InputMode.ANY) {
            this._createTextArea();
        }
        else {
            this._createInput();
        }
        this.setTabIndex(delegate.tabIndex);
        this._disableDom();  // enable dom when EditBox.onEnable is called
        this._initStyleSheet();
        this._registerEventListeners();
        this._addDomToGameContainer();

        _fullscreen = cc.view.isAutoFullScreenEnabled();
        _autoResize = cc.view._resizeWithBrowserSize;
    },

    enable () {
        this._enableDom();

        if (!CC_EDITOR) {
            this._delegate._unregisterEvent();  // not support touch events on web platform
        }
    },

    disable () {
        this._disableDom();
    },

    clear () {
        this._removeEventListeners();
        this._removeDomFromGameContainer();
    },

    update () {
        this._updateMatrix();
    },

    setTabIndex (index) {
        this._elem.tabIndex = index;
    },

    setSize (width, height) {
        let elem = this._elem;
        elem.style.width = width + 'px';
        elem.style.height = height + 'px';
    },

    setFocus (value) {
        if (value) {
            this._elem.focus();
        }
        else {
            this._elem.blur();
        }
    },

    isFocused () {
        return this._editing;
    },

    beginEditing () {
        this._editing = true;
        _currentEditBoxImpl = this;
        this._showDom();
        this._delegate.editBoxEditingDidBegan();  
    },

    endEditing () {
        this._editing = false;
        _currentEditBoxImpl = null;
        this._hideDom();
        this._delegate.editBoxEditingDidEnded();
    },

    // ==========================================================================
    // implement dom input
    _createInput () {
        this._isTextArea = false;
        this._elem = document.createElement('input');
    },

    _createTextArea () {
        this._isTextArea = true;
        this._elem = document.createElement('textarea');
    },

    _addDomToGameContainer () {
        cc.game.container.appendChild(this._elem);
        document.head.appendChild(this._placeholderStyleSheet);
    },

    _removeDomFromGameContainer () {
        let hasElem = utils.contains(cc.game.container, this._elem);
        if (hasElem) {
            cc.game.container.removeChild(this._elem);
        }
        let hasStyleSheet = utils.contains(document.head, this._placeholderStyleSheet);
        if (hasStyleSheet) {
            document.head.removeChild(this._placeholderStyleSheet);
        }
        
        delete this._elem;
        delete this._placeholderStyleSheet;
    },

    _enableDom () {
        this._elem.style.disaplay = '';
    },

    _disableDom () {
        this._elem.style.disaplay = 'none';
    },

    _showDom () {
        this._updateMaxLength();
        this._updateInputType();
        this._updateStyleSheet();

        this._elem.style.opacity = 1;
        this._delegate._hideLabels();
        
        if (cc.sys.isMobile) {
            this._showDomOnMobile();
        }
    },

    _hideDom () {
        let elem = this._elem;

        elem.style.opacity = 0;
        this._delegate._showLabels();
        
        if (cc.sys.isMobile) {
            this._hideDomOnMobile();
        }
    },

    _showDomOnMobile () {
        if (cc.sys.os !== cc.sys.OS_ANDROID) {
            return;
        }
        
        if (_fullscreen) {
            cc.view.enableAutoFullScreen(false);
            cc.screen.exitFullScreen();
        }
        if (_autoResize) {
            cc.view.resizeWithBrowserSize(false);
        }

        this._adjustWindowScroll();
    },

    _hideDomOnMobile () {
        if (cc.sys.os !== cc.sys.OS_ANDROID) {
            return;
        }

        if (_fullscreen) {
            cc.view.enableAutoFullScreen(true);
        }
        if (_autoResize) {
            cc.view.resizeWithBrowserSize(true);
        }
        
        this._scrollBackWindow();
    },

    // adjust view to editBox
    _adjustWindowScroll () {
        let self = this;
        setTimeout(function() {
            if (window.scrollY < SCROLLY) {
                self._elem.scrollIntoView({block: "start", inline: "nearest", behavior: "smooth"});
            }
        }, DELAY_TIME);
    },

    _scrollBackWindow () {
        setTimeout(function () {
            window.scrollTo(0, 0);
        }, DELAY_TIME);
    },

    _updateMatrix () {    
        let node = this._delegate.node;    
        node.getWorldMatrix(this._worldMat);
        let worldMat = this._worldMat;

        // check whether need to update
        if (this._m00 === worldMat.m00 && this._m01 === worldMat.m01 &&
            this._m04 === worldMat.m04 && this._m05 === worldMat.m05 &&
            this._m12 === worldMat.m12 && this._m13 === worldMat.m13 &&
            this._w === node._contentSize.width && this._h === node._contentSize.height) {
            return;
        }

        // update matrix cache
        this._m00 = worldMat.m00;
        this._m01 = worldMat.m01;
        this._m04 = worldMat.m04;
        this._m05 = worldMat.m05;
        this._m12 = worldMat.m12;
        this._m13 = worldMat.m13;
        this._w = node._contentSize.width;
        this._h = node._contentSize.height;

        let scaleX = cc.view._scaleX, scaleY = cc.view._scaleY,
            viewport = cc.view._viewportRect,
            dpr = cc.view._devicePixelRatio;

        _vec3.x = -node._anchorPoint.x * this._w;
        _vec3.y = -node._anchorPoint.y * this._h;
    
        math.mat4.translate(worldMat, worldMat, _vec3);

        // can't find camera in editor
        let cameraMat;
        if (CC_EDITOR) {
            cameraMat = this._cameraMat = worldMat;
        }
        else {
            let camera = cc.Camera.findCamera(node);
            camera.getWorldToCameraMatrix(this._cameraMat);
            cameraMat = this._cameraMat;
            math.mat4.mul(cameraMat, cameraMat, worldMat);
        }
        
    
        scaleX /= dpr;
        scaleY /= dpr;
    
        let container = cc.game.container;
        let a = cameraMat.m00 * scaleX, b = cameraMat.m01, c = cameraMat.m04, d = cameraMat.m05 * scaleY;
    
        let offsetX = container && container.style.paddingLeft && parseInt(container.style.paddingLeft);
        offsetX += viewport.x / dpr;
        let offsetY = container && container.style.paddingBottom && parseInt(container.style.paddingBottom);
        offsetY += viewport.y / dpr;
        let tx = cameraMat.m12 * scaleX + offsetX, ty = cameraMat.m13 * scaleY + offsetY;
    
        if (polyfill.zoomInvalid) {
            this.setSize(node.width * a, node.height * d);
            a = 1;
            d = 1;
        }
    
        let elem = this._elem;
        let matrix = "matrix(" + a + "," + -b + "," + -c + "," + d + "," + tx + "," + -ty + ")";
        elem.style['transform'] = matrix;
        elem.style['-webkit-transform'] = matrix;
        elem.style['transform-origin'] = '0px 100% 0px';
        elem.style['-webkit-transform-origin'] = '0px 100% 0px';
    },

    // ===========================================
    // input type and max length
    _updateInputType () {
        let delegate = this._delegate,
            inputMode = delegate.inputMode,
            inputFlag = delegate.inputFlag,
            returnType = delegate.returnType,
            elem = this._elem;

        // whether need to update
        if (this._inputMode === inputMode &&
            this._inputFlag === inputFlag &&
            this._returnType === returnType) {
            return;
        }

        // update cache
        this._inputMode = inputMode;
        this._inputFlag = inputFlag;
        this._returnType = returnType;

        // FIX ME: TextArea actually dose not support password type.
        if (this._isTextArea) {
            // input flag
            let textTransform = 'none';
            if (inputFlag === InputFlag.INITIAL_CAPS_ALL_CHARACTERS) {
                textTransform = 'uppercase';
            }
            else if (inputFlag === InputFlag.INITIAL_CAPS_WORD) {
                textTransform = 'capitalize';
            }
            elem.style.textTransform = textTransform;
            return;
        }
    
        // begin to updateInputType
        if (inputFlag === InputFlag.PASSWORD) {
            elem.type = 'password';
            return;
        }
    
        // input mode
        let type = elem.type;
        if (inputMode === InputMode.EMAIL_ADDR) {
            type = 'email';
        } else if(inputMode === InputMode.NUMERIC || inputMode === InputMode.DECIMAL) {
            type = 'number';
        } else if(inputMode === InputMode.PHONE_NUMBER) {
            type = 'number';
            elem.pattern = '[0-9]*';
        } else if(inputMode === InputMode.URL) {
            type = 'url';
        } else {
            type = 'text';
    
            if (returnType === KeyboardReturnType.SEARCH) {
                type = 'search';
            }
        }
        elem.type = type;

        // input flag
        let textTransform = 'none';
        if (inputFlag === InputFlag.INITIAL_CAPS_ALL_CHARACTERS) {
            textTransform = 'uppercase';
        }
        else if (inputFlag === InputFlag.INITIAL_CAPS_WORD) {
            textTransform = 'capitalize';
        }
        elem.style.textTransform = textTransform;
    },

    _updateMaxLength () {
        let maxLength = this._delegate.maxLength;
        if(maxLength < 0) {
            //we can't set Number.MAX_VALUE to input's maxLength property
            //so we use a magic number here, it should works at most use cases.
            maxLength = 65535;
        }
        this._elem.maxLength = maxLength;
    },

    // ===========================================
    // style sheet
    _initStyleSheet () {
        let elem = this._elem;
        elem.style.opacity = 0;
        elem.style.border = 0;
        elem.style.background = 'transparent';
        elem.style.width = '100%';
        elem.style.height = '100%';
        elem.style.active = 0;
        elem.style.outline = 'medium';
        elem.style.padding = '0';
        elem.style.textTransform = 'uppercase';
        elem.style.position = "absolute";
        elem.style.bottom = "0px";
        elem.style.left = LEFT_PADDING + "px";
        elem.className = "cocosEditBox";
        elem.id = this._domId;

        if (!this._isTextArea) {
            elem.type = 'text';
            elem.style['-moz-appearance'] = 'textfield';
        }
        else {
            elem.style.resize = 'none';
            elem.style.overflow_y = 'scroll';
        }

        this._placeholderStyleSheet = document.createElement('style');
    },
    
    _updateStyleSheet () {
        let delegate = this._delegate,
            elem = this._elem;

        elem.value = delegate.string;
        elem.placeholder = delegate.placeholder;

        this._updateTextLabel(delegate.textLabel);
        this._updatePlaceholderLabel(delegate.placeholderLabel);
    },

    _updateTextLabel (textLabel) {
        if (!textLabel) {
            return;
        }
        // get font
        let font = textLabel.font;
        if (font && !(font instanceof cc.BitmapFont)) {
            font = font._fontFamily;
        }
        else {
            font = textLabel.fontFamily;
        }

        // whether need to update
        if (this._textLabelFont === font
            && this._textLabelFontSize === textLabel.fontSize
            && this._textLabelFontColor === textLabel.fontColor
            && this._textLabelAlign === textLabel.horizontalAlign) {
                return;
        }

        // update cache
        this._textLabelFont = font;
        this._textLabelFontSize = textLabel.fontSize;
        this._textLabelFontColor = textLabel.fontColor;
        this._textLabelAlign = textLabel.horizontalAlign;

        let elem = this._elem;
        // font size
        elem.style.fontSize = `${textLabel.fontSize}px`;
        // font color
        elem.style.color = textLabel.node.color.toCSS('rgba');
        // font family
        elem.style.fontFamily = font;
        // text-align
        switch(textLabel.horizontalAlign) {
            case Label.HorizontalAlign.LEFT:
                elem.style.textAlign = 'left';
                break;
            case Label.HorizontalAlign.CENTER:
                elem.style.textAlign = 'center';
                break;
            case Label.HorizontalAlign.RIGHT:
                elem.style.textAlign = 'right';
                break;
        }
        // lineHeight
        // Can't sync lineHeight property, because lineHeight would change the touch area of input
    },

    _updatePlaceholderLabel (placeholderLabel) {
        if (!placeholderLabel) {
            return;
        }

        // get font
        let font = placeholderLabel.font;
        if (font && !(font instanceof cc.BitmapFont)) {
            font = placeholderLabel.font._fontFamily;
        }
        else {
            font = placeholderLabel.fontFamily;
        }

        // whether need to update
        if (this._placeholderLabelFont === font
            && this._placeholderLabelFontSize === placeholderLabel.fontSize
            && this._placeholderLabelFontColor === placeholderLabel.fontColor
            && this._placeholderLabelAlign === placeholderLabel.horizontalAlign
            && this._placeholderLineHeight === placeholderLabel.lineHeight) {
                return;
        }

        // update cache
        this._placeholderLabelFont = font;
        this._placeholderLabelFontSize = placeholderLabel.fontSize;
        this._placeholderLabelFontColor = placeholderLabel.fontColor;
        this._placeholderLabelAlign = placeholderLabel.horizontalAlign;
        this._placeholderLineHeight = placeholderLabel.lineHeight;

        let styleEl = this._placeholderStyleSheet;
        // font size
        let fontSize = placeholderLabel.fontSize;
        // font color
        let fontColor = placeholderLabel.node.color.toCSS('rgba');
        // line height
        let lineHeight = placeholderLabel.lineHeight;
        // horizontal align
        let horizontalAlign;
        switch (placeholderLabel.horizontalAlign) {
            case Label.HorizontalAlign.LEFT:
                horizontalAlign = 'left';
                break;
            case Label.HorizontalAlign.CENTER:
                horizontalAlign = 'center';
                break;
            case Label.HorizontalAlign.RIGHT:
                horizontalAlign = 'right';
                break;
        }

        styleEl.innerHTML = `
            #${this._domId}::-webkit-input-placeholder {
                text-transform: initial;
                font-family: ${font};
                font-size: ${fontSize}px;
                color: ${fontColor};
                line-height: ${lineHeight}px;
                text-align: ${horizontalAlign};
            }
            #${this._domId}::-moz-placeholder {
                text-transform: initial;
                font-family: ${font};
                font-size: ${fontSize}px;
                color: ${fontColor};
                line-height: ${lineHeight}px;
                text-align: ${horizontalAlign};
            }
            #${this._domId}:-ms-input-placeholder {
                text-transform: initial;
                font-family: ${font};
                font-size: ${fontSize}px;
                color: ${fontColor};
                line-height: ${lineHeight}px;
                text-align: ${horizontalAlign};
            }
        `;
    },

    // ===========================================
    // handle event listeners
    _registerEventListeners () {        
        let impl = this,
            elem = this._elem,
            inputLock = false,
            cbs = this._eventListeners;

        cbs.compositionStart = function () {
            inputLock = true;
        };
        
        cbs.compositionEnd = function () {
            inputLock = false;
            impl._delegate.editBoxTextChanged(elem.value);
        };

        cbs.onInput = function () {
            if (inputLock) {
                return;
            }
            impl._delegate.editBoxTextChanged(elem.value);
        };

        cbs.onFocus = function () {
            impl.beginEditing()
        };
        
        // There are 2 ways to focus on the input element:
        // Click the input element, or call input.focus().
        // Both need to adjust window scroll.
        cbs.onClick = function (e) {
            // In case operation sequence: click input, hide keyboard, then click again.
            if (impl._editing) {
                if (cc.sys.isMobile) {
                    impl._adjustWindowScroll();
                }
            }
            // If has last focused input, should call lastInput.blur() explicitly.
            else if (_currentEditBoxImpl && _currentEditBoxImpl !== impl) {
                _currentEditBoxImpl._elem.blur();
            }
        };
        
        cbs.onKeypress = function (e) {
            if (e.keyCode === macro.KEY.enter) {
                e.stopPropagation();
                impl._delegate.editBoxEditingReturn();

                if (!impl._isTextArea) {
                    elem.blur();  // hide keyboard and call endEditing()
                }
            }
        };

        cbs.onBlur = function () {
            impl.endEditing();
        };


        elem.addEventListener('compositionstart', cbs.compositionStart);
        elem.addEventListener('compositionend', cbs.compositionEnd);
        elem.addEventListener('input', cbs.onInput);
        elem.addEventListener('focus', cbs.onFocus);
        elem.addEventListener('keypress', cbs.onKeypress);
        elem.addEventListener('blur', cbs.onBlur);
        elem.addEventListener('touchstart', cbs.onClick);
    },

    _removeEventListeners () {
        let elem = this._elem,
            cbs = this._eventListeners;

        elem.removeEventListener('compositionstart', cbs.compositionStart);
        elem.removeEventListener('compositionend', cbs.compositionEnd);
        elem.removeEventListener('input', cbs.onInput);
        elem.removeEventListener('focus', cbs.onFocus);
        elem.removeEventListener('keypress', cbs.onKeypress);
        elem.removeEventListener('blur', cbs.onBlur);
        elem.removeEventListener('touchstart', cbs.onClick);
        
        cbs.compositionStart = null;
        cbs.compositionEnd = null;
        cbs.onInput = null;
        cbs.onFocus = null;
        cbs.onKeypress = null;
        cbs.onBlur = null;
        cbs.onClick = null;
    },
});

