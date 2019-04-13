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

const js = require('../platform/js');
const macro = require('../platform/CCMacro');
const textUtils = require('../utils/text-utils');
const HtmlTextParser = require('../utils/html-text-parser');
const _htmlTextParser = new HtmlTextParser();

const HorizontalAlign = macro.TextAlignment;
const VerticalAlign = macro.VerticalTextAlignment;
const RichTextChildName = "RICHTEXT_CHILD";
const RichTextChildImageName = "RICHTEXT_Image_CHILD";

// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.
function debounce(func, wait, immediate) {
    let timeout;
    return function () {
        let context = this;
        let later = function () {
            timeout = null;
            if (!immediate) func.apply(context, arguments);
        };
        let callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, arguments);
    };
}

/**
 * RichText pool
 */
let pool = new js.Pool(function (node) {
    if (CC_EDITOR) {
        return false;
    }
    if (CC_DEV) {
        cc.assert(!node._parent, 'Recycling node\'s parent should be null!');
    }
    if (!cc.isValid(node)) {
        return false;
    }
    else if (node.getComponent(cc.LabelOutline)) {
        return false;
    }
    return true;
}, 20);

pool.get = function (string, richtext) {
    let labelNode = this._get();
    if (!labelNode) {
        labelNode = new cc.PrivateNode(RichTextChildName);
    }
    let labelComponent = labelNode.getComponent(cc.Label);
    if (!labelComponent) {
        labelComponent = labelNode.addComponent(cc.Label);
    }

    labelNode.setPosition(0, 0);
    labelNode.setAnchorPoint(0.5, 0.5);
    labelNode.setContentSize(128, 128);
    labelNode.skewX = 0;

    if (typeof string !== 'string') {
        string = '' + string;
    }
    let isAsset = richtext.font instanceof cc.Font;
    if (isAsset) {
        labelComponent.font = richtext.font;
    } else {
        labelComponent.fontFamily = richtext.fontFamily;
    }
    labelComponent.string = string;
    labelComponent.horizontalAlign = HorizontalAlign.LEFT;
    labelComponent.verticalAlign = VerticalAlign.TOP;
    labelComponent.fontSize = richtext.fontSize || 40;
    labelComponent.overflow = 0;
    labelComponent.enableWrapText = true;
    labelComponent.lineHeight = 40;
    labelComponent._enableBold(false);
    labelComponent._enableItalics(false);
    labelComponent._enableUnderline(false);
    return labelNode;
};

/**
 * !#en The RichText Component.
 * !#zh 富文本组件
 * @class RichText
 * @extends Component
 */
let RichText = cc.Class({
    name: 'cc.RichText',
    extends: cc.Component,

    ctor: function () {
        this._textArray = null;
        this._labelSegments = [];
        this._labelSegmentsCache = [];
        this._linesWidth = [];

        if (CC_EDITOR) {
            this._updateRichTextStatus = debounce(this._updateRichText, 200);
        }
        else {
            this._updateRichTextStatus = this._updateRichText;
        }
    },

    editor: CC_EDITOR && {
        menu: 'i18n:MAIN_MENU.component.renderers/RichText',
        help: 'i18n:COMPONENT.help_url.richtext',
        inspector: 'packages://inspector/inspectors/comps/richtext.js',
        executeInEditMode: true
    },

    properties: {
        /**
         * !#en Content string of RichText.
         * !#zh 富文本显示的文本内容。
         * @property {String} string
         */
        string: {
            default: '<color=#00ff00>Rich</c><color=#0fffff>Text</color>',
            multiline: true,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.string',
            notify: function () {
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en Horizontal Alignment of each line in RichText.
         * !#zh 文本内容的水平对齐方式。
         * @property {macro.TextAlignment} horizontalAlign
         */
        horizontalAlign: {
            default: HorizontalAlign.LEFT,
            type: HorizontalAlign,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.horizontal_align',
            animatable: false,
            notify: function (oldValue) {
                if (this.horizontalAlign === oldValue) return;

                this._layoutDirty = true;
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en Font size of RichText.
         * !#zh 富文本字体大小。
         * @property {Number} fontSize
         */
        fontSize: {
            default: 40,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.font_size',
            notify: function (oldValue) {
                if (this.fontSize === oldValue) return;

                this._layoutDirty = true;
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en Custom System font of RichText
         * !#zh 富文本定制系统字体
         * @property {String} fontFamily
         */
        _fontFamily: "Arial",
        fontFamily: {
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.font_family',
            get () {
                return this._fontFamily;
            },
            set (value) {
                if (this._fontFamily === value) return;
                this._fontFamily = value;
                this._layoutDirty = true;
                this._updateRichTextStatus();
            },
            animatable: false
        },

        /**
         * !#en Custom TTF font of RichText
         * !#zh  富文本定制字体
         * @property {cc.TTFFont} font
         */
        font: {
            default: null,
            type: cc.TTFFont,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.font',
            notify: function (oldValue) {
                if (this.font === oldValue) return;

                this._layoutDirty = true;
                if (this.font) {
                    this.useSystemFont = false;
                    this._onTTFLoaded();
                }
                else {
                    this.useSystemFont = true;
                }
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en Whether use system font name or not.
         * !#zh 是否使用系统字体。
         * @property {Boolean} useSystemFont
         */
        _isSystemFontUsed: true,
        useSystemFont: {
            get () {
                return this._isSystemFontUsed;
            },
            set (value) {
                if (!value && !this.font || (this._isSystemFontUsed === value)) {
                    return;
                }
                this._isSystemFontUsed = value;
                this._layoutDirty = true;
                this._updateRichTextStatus();
            },
            animatable: false,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.system_font',
        },

        /**
         * !#en The maximize width of the RichText
         * !#zh 富文本的最大宽度
         * @property {Number} maxWidth
         */
        maxWidth: {
            default: 0,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.max_width',
            notify: function (oldValue) {
                if (this.maxWidth === oldValue) return;

                this._layoutDirty = true;
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en Line Height of RichText.
         * !#zh 富文本行高。
         * @property {Number} lineHeight
         */
        lineHeight: {
            default: 40,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.line_height',
            notify: function (oldValue) {
                if (this.lineHeight === oldValue) return;

                this._layoutDirty = true;
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en The image atlas for the img tag. For each src value in the img tag, there should be a valid spriteFrame in the image atlas.
         * !#zh 对于 img 标签里面的 src 属性名称，都需要在 imageAtlas 里面找到一个有效的 spriteFrame，否则 img tag 会判定为无效。
         * @property {SpriteAtlas} imageAtlas
         */
        imageAtlas: {
            default: null,
            type: cc.SpriteAtlas,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.image_atlas',
            notify: function (oldValue) {
                if (this.imageAtlas === oldValue) return;

                this._layoutDirty = true;
                this._updateRichTextStatus();
            }
        },

        /**
         * !#en
         * Once checked, the RichText will block all input events (mouse and touch) within
         * the bounding box of the node, preventing the input from penetrating into the underlying node.
         * !#zh
         * 选中此选项后，RichText 将阻止节点边界框中的所有输入事件（鼠标和触摸），从而防止输入事件穿透到底层节点。
         * @property {Boolean} handleTouchEvent
         * @default true
         */
        handleTouchEvent: {
            default: true,
            tooltip: CC_DEV && 'i18n:COMPONENT.richtext.handleTouchEvent',
            notify: function (oldValue) {
                if (this.handleTouchEvent === oldValue) return;
                if (this.enabledInHierarchy) {
                    this.handleTouchEvent ? this._addEventListeners() : this._removeEventListeners();
                }
            }
        }
    },

    statics: {
        HorizontalAlign: HorizontalAlign,
        VerticalAlign: VerticalAlign
    },

    onEnable () {
        if (this.handleTouchEvent) {
            this._addEventListeners();
        }
        this._updateRichText();
        this._activateChildren(true);
    },

    onDisable () {
        if (this.handleTouchEvent) {
            this._removeEventListeners();
        }
        this._activateChildren(false);
    },

    start () {
        this._onTTFLoaded();
    },

    _onColorChanged (parentColor) {
        let children = this.node.children;
        children.forEach(function (childNode) {
            childNode.color = parentColor;
        });
    },

    _addEventListeners () {
        this.node.on(cc.Node.EventType.TOUCH_END, this._onTouchEnded, this);
        this.node.on(cc.Node.EventType.COLOR_CHANGED, this._onColorChanged, this);
    },

    _removeEventListeners () {
        this.node.off(cc.Node.EventType.TOUCH_END, this._onTouchEnded, this);
        this.node.off(cc.Node.EventType.COLOR_CHANGED, this._onColorChanged, this);
    },

    _updateLabelSegmentTextAttributes () {
        this._labelSegments.forEach(function (item) {
            this._applyTextAttribute(item);
        }.bind(this));
    },

    _createFontLabel (string) {
        return pool.get(string, this);
    },

    _onTTFLoaded () {
        if (this.font instanceof cc.TTFFont) {
            if (this.font._nativeAsset) {
                this._layoutDirty = true;
                this._updateRichText();
            }
            else {
                let self = this;
                cc.loader.load(this.font.nativeUrl, function (err, fontFamily) {
                    self._layoutDirty = true;
                    self._updateRichText();
                });
            }
        }
        else {
            this._layoutDirty = true;
            this._updateRichText();
        }
    },

    _measureText (styleIndex, string) {
        let self = this;
        let func = function (string) {
            let label;
            if (self._labelSegmentsCache.length === 0) {
                label = self._createFontLabel(string);
                self._labelSegmentsCache.push(label);
            }
            else {
                label = self._labelSegmentsCache[0];
                label.getComponent(cc.Label).string = string;
            }
            label._styleIndex = styleIndex;
            self._applyTextAttribute(label);
            let labelSize = label.getContentSize();
            return labelSize.width;
        };
        if (string) {
            return func(string);
        }
        else {
            return func;
        }
    },

    _onTouchEnded (event) {
        let components = this.node.getComponents(cc.Component);

        for (let i = 0; i < this._labelSegments.length; ++i) {
            let labelSegment = this._labelSegments[i];
            let clickHandler = labelSegment._clickHandler;
            let clickParam = labelSegment._clickParam;
            if (clickHandler && this._containsTouchLocation(labelSegment, event.touch.getLocation())) {
                components.forEach(function (component) {
                    if (component.enabledInHierarchy && component[clickHandler]) {
                        component[clickHandler](event, clickParam);
                    }
                });
                event.stopPropagation();
            }
        }
    },

    _containsTouchLocation (label, point) {
        let myRect = label.getBoundingBoxToWorld();
        return myRect.contains(point);
    },

    _resetState () {
        let children = this.node.children;
        for (let i = children.length - 1; i >= 0; i--) {
            let child = children[i];
            if (child.name === RichTextChildName || child.name === RichTextChildImageName) {
                if (child.parent === this.node) {
                    child.parent = null;
                }
                else {
                    // In case child.parent !== this.node, child cannot be removed from children
                    children.splice(i, 1);
                }
                if (child.name === RichTextChildName) {
                    pool.put(child);
                }
            }
        }

        this._labelSegments.length = 0;
        this._labelSegmentsCache.length = 0;
        this._linesWidth.length = 0;
        this._lineOffsetX = 0;
        this._lineCount = 1;
        this._labelWidth = 0;
        this._labelHeight = 0;
        this._layoutDirty = true;
    },

    onRestore: CC_EDITOR && function () {
        // TODO: refine undo/redo system
        // Because undo/redo will not call onEnable/onDisable,
        // we need call onEnable/onDisable manually to active/disactive children nodes.
        if (this.enabledInHierarchy) {
            this.onEnable();
        }
        else {
            this.onDisable();
        }
    },

    _activateChildren (active) {
        for (let i = this.node.children.length - 1; i >= 0; i--) {
            let child = this.node.children[i];
            if (child.name === RichTextChildName || child.name === RichTextChildImageName) {
                child.active = active;
            }
        }
    },

    _addLabelSegment (stringToken, styleIndex) {
        let labelSegment;
        if (this._labelSegmentsCache.length === 0) {
            labelSegment = this._createFontLabel(stringToken);
        }
        else {
            labelSegment = this._labelSegmentsCache.pop();
            labelSegment.getComponent(cc.Label).string = stringToken;
        }
        labelSegment._styleIndex = styleIndex;
        labelSegment._lineCount = this._lineCount;
        labelSegment.active = this.node.active;

        labelSegment.setAnchorPoint(0, 0);
        this._applyTextAttribute(labelSegment);

        this.node.addChild(labelSegment);
        this._labelSegments.push(labelSegment);

        return labelSegment;
    },

    _updateRichTextWithMaxWidth (labelString, labelWidth, styleIndex) {
        let fragmentWidth = labelWidth;
        let labelSegment;

        if (this._lineOffsetX > 0 && fragmentWidth + this._lineOffsetX > this.maxWidth) {
            //concat previous line
            let checkStartIndex = 0;
            while (this._lineOffsetX <= this.maxWidth) {
                let checkEndIndex = this._getFirstWordLen(labelString,
                    checkStartIndex,
                    labelString.length);
                let checkString = labelString.substr(checkStartIndex, checkEndIndex);
                let checkStringWidth = this._measureText(styleIndex, checkString);

                if (this._lineOffsetX + checkStringWidth <= this.maxWidth) {
                    this._lineOffsetX += checkStringWidth;
                    checkStartIndex += checkEndIndex;
                }
                else {

                    if (checkStartIndex > 0) {
                        let remainingString = labelString.substr(0, checkStartIndex);
                        this._addLabelSegment(remainingString, styleIndex);
                        labelString = labelString.substr(checkStartIndex, labelString.length);
                        fragmentWidth = this._measureText(styleIndex, labelString);
                    }
                    this._updateLineInfo();
                    break;
                }
            }
        }
        if (fragmentWidth > this.maxWidth) {
            let fragments = textUtils.fragmentText(labelString,
                fragmentWidth,
                this.maxWidth,
                this._measureText(styleIndex));
            for (let k = 0; k < fragments.length; ++k) {
                let splitString = fragments[k];
                labelSegment = this._addLabelSegment(splitString, styleIndex);
                let labelSize = labelSegment.getContentSize();
                this._lineOffsetX += labelSize.width;
                if (fragments.length > 1 && k < fragments.length - 1) {
                    this._updateLineInfo();
                }
            }
        }
        else {
            this._lineOffsetX += fragmentWidth;
            this._addLabelSegment(labelString, styleIndex);
        }
    },

    _isLastComponentCR (stringToken) {
        return stringToken.length - 1 === stringToken.lastIndexOf("\n");
    },

    _updateLineInfo () {
        this._linesWidth.push(this._lineOffsetX);
        this._lineOffsetX = 0;
        this._lineCount++;
    },

    _needsUpdateTextLayout (newTextArray) {
        if (this._layoutDirty || !this._textArray || !newTextArray) {
            return true;
        }

        if (this._textArray.length !== newTextArray.length) {
            return true;
        }

        for (let i = 0; i < this._textArray.length; ++i) {
            let oldItem = this._textArray[i];
            let newItem = newTextArray[i];
            if (oldItem.text !== newItem.text) {
                return true;
            }
            else {
                if (oldItem.style) {
                    if (newItem.style) {
                        if (!!newItem.style.outline !== !!oldItem.style.outline) {
                            return true;
                        }
                        if (oldItem.style.size !== newItem.style.size
                            || oldItem.style.italic !== newItem.style.italic
                            || oldItem.style.isImage !== newItem.style.isImage) {
                            return true;
                        }
                        if (oldItem.style.isImage === newItem.style.isImage) {
                            if (oldItem.style.src !== newItem.style.src) {
                                return true;
                            }
                        }
                    }
                    else {
                        if (oldItem.style.size || oldItem.style.italic || oldItem.style.isImage || oldItem.style.outline) {
                            return true;
                        }
                    }
                }
                else {
                    if (newItem.style) {
                        if (newItem.style.size || newItem.style.italic || newItem.style.isImage || newItem.style.outline) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    },

    _addRichTextImageElement (richTextElement) {
        let spriteFrameName = richTextElement.style.src;
        let spriteFrame = this.imageAtlas.getSpriteFrame(spriteFrameName);
        if (spriteFrame) {
            let spriteNode = new cc.PrivateNode(RichTextChildImageName);
            let spriteComponent = spriteNode.addComponent(cc.Sprite);
            spriteNode.setAnchorPoint(0, 0);
            spriteComponent.type = cc.Sprite.Type.SLICED;
            spriteComponent.sizeMode = cc.Sprite.SizeMode.CUSTOM;
            this.node.addChild(spriteNode);
            this._labelSegments.push(spriteNode);

            let spriteRect = spriteFrame.getRect();
            let scaleFactor = 1;
            let spriteWidth = spriteRect.width;
            let spriteHeight = spriteRect.height;
            let expectWidth = richTextElement.style.imageWidth;
            let expectHeight = richTextElement.style.imageHeight;

            //follow the original rule, expectHeight must less then lineHeight
            if (expectHeight > 0 && expectHeight < this.lineHeight) {
                scaleFactor = expectHeight / spriteHeight;
                spriteWidth = spriteWidth * scaleFactor;
                spriteHeight = spriteHeight * scaleFactor;
            }
            else {
                scaleFactor = this.lineHeight / spriteHeight;
                spriteWidth = spriteWidth * scaleFactor;
                spriteHeight = spriteHeight * scaleFactor;
            }

            if (expectWidth > 0) spriteWidth = expectWidth;

            if (this.maxWidth > 0) {
                if (this._lineOffsetX + spriteWidth > this.maxWidth) {
                    this._updateLineInfo();
                }
                this._lineOffsetX += spriteWidth;

            }
            else {
                this._lineOffsetX += spriteWidth;
                if (this._lineOffsetX > this._labelWidth) {
                    this._labelWidth = this._lineOffsetX;
                }
            }
            spriteComponent.spriteFrame = spriteFrame;
            spriteNode.setContentSize(spriteWidth, spriteHeight);
            spriteNode._lineCount = this._lineCount;

            if (richTextElement.style.event) {
                if (richTextElement.style.event.click) {
                    spriteNode._clickHandler = richTextElement.style.event.click;
                }
                if (richTextElement.style.event.param) {
                    spriteNode._clickParam = richTextElement.style.event.param;
                }
            }
        }
        else {
            cc.warnID(4400);
        }
    },

    _updateRichText () {
        if (!this.enabled) return;

        let newTextArray = _htmlTextParser.parse(this.string);
        if (!this._needsUpdateTextLayout(newTextArray)) {
            this._textArray = newTextArray;
            this._updateLabelSegmentTextAttributes();
            return;
        }

        this._textArray = newTextArray;
        this._resetState();

        let lastEmptyLine = false;
        let label;
        let labelSize;

        for (let i = 0; i < this._textArray.length; ++i) {
            let richTextElement = this._textArray[i];
            let text = richTextElement.text;
            //handle <br/> <img /> tag
            if (text === "") {
                if (richTextElement.style && richTextElement.style.newline) {
                    this._updateLineInfo();
                    continue;
                }
                if (richTextElement.style && richTextElement.style.isImage && this.imageAtlas) {
                    this._addRichTextImageElement(richTextElement);
                    continue;
                }
            }
            let multilineTexts = text.split("\n");

            for (let j = 0; j < multilineTexts.length; ++j) {
                let labelString = multilineTexts[j];
                if (labelString === "") {
                    //for continues \n
                    if (this._isLastComponentCR(text)
                        && j === multilineTexts.length - 1) {
                        continue;
                    }
                    this._updateLineInfo();
                    lastEmptyLine = true;
                    continue;
                }
                lastEmptyLine = false;

                if (this.maxWidth > 0) {
                    let labelWidth = this._measureText(i, labelString);
                    this._updateRichTextWithMaxWidth(labelString, labelWidth, i);

                    if (multilineTexts.length > 1 && j < multilineTexts.length - 1) {
                        this._updateLineInfo();
                    }
                }
                else {
                    label = this._addLabelSegment(labelString, i);
                    labelSize = label.getContentSize();

                    this._lineOffsetX += labelSize.width;
                    if (this._lineOffsetX > this._labelWidth) {
                        this._labelWidth = this._lineOffsetX;
                    }

                    if (multilineTexts.length > 1 && j < multilineTexts.length - 1) {
                        this._updateLineInfo();
                    }
                }
            }
        }
        if (!lastEmptyLine) {
            this._linesWidth.push(this._lineOffsetX);
        }

        if (this.maxWidth > 0) {
            this._labelWidth = this.maxWidth;
        }
        this._labelHeight = (this._lineCount + textUtils.BASELINE_RATIO) * this.lineHeight;

        // trigger "size-changed" event
        this.node.setContentSize(this._labelWidth, this._labelHeight);

        this._updateRichTextPosition();
        this._layoutDirty = false;
    },

    _getFirstWordLen (text, startIndex, textLen) {
        let character = text.charAt(startIndex);
        if (textUtils.isUnicodeCJK(character)
            || textUtils.isUnicodeSpace(character)) {
            return 1;
        }

        let len = 1;
        for (let index = startIndex + 1; index < textLen; ++index) {
            character = text.charAt(index);
            if (textUtils.isUnicodeSpace(character)
                || textUtils.isUnicodeCJK(character)) {
                break;
            }
            len++;
        }

        return len;
    },

    _updateRichTextPosition () {
        let nextTokenX = 0;
        let nextLineIndex = 1;
        let totalLineCount = this._lineCount;
        for (let i = 0; i < this._labelSegments.length; ++i) {
            let label = this._labelSegments[i];
            let lineCount = label._lineCount;
            if (lineCount > nextLineIndex) {
                nextTokenX = 0;
                nextLineIndex = lineCount;
            }
            let lineOffsetX = 0;
            // let nodeAnchorXOffset = (0.5 - this.node.anchorX) * this._labelWidth;
            switch (this.horizontalAlign) {
                case HorizontalAlign.LEFT:
                    lineOffsetX = - this._labelWidth / 2;
                    break;
                case HorizontalAlign.CENTER:
                    lineOffsetX = - this._linesWidth[lineCount - 1] / 2;
                    break;
                case HorizontalAlign.RIGHT:
                    lineOffsetX = this._labelWidth / 2 - this._linesWidth[lineCount - 1];
                    break;
                default:
                    break;
            }
            label.x = nextTokenX + lineOffsetX;

            let labelSize = label.getContentSize();

            label.y = this.lineHeight * (totalLineCount - lineCount) - this._labelHeight / 2;

            if (lineCount === nextLineIndex) {
                nextTokenX += labelSize.width;
            }
        }
    },

    _convertLiteralColorValue (color) {
        let colorValue = color.toUpperCase();
        if (cc.Color[colorValue]) {
            return cc.Color[colorValue];
        }
        else {
            let out = cc.color();
            return out.fromHEX(color);
        }
    },

    _applyTextAttribute (labelNode) {
        let labelComponent = labelNode.getComponent(cc.Label);
        if (!labelComponent) {
            return;
        }

        let index = labelNode._styleIndex;

        if (this._isSystemFontUsed) {
            labelComponent.fontFamily = this._fontFamily;
        }
        labelComponent.useSystemFont = this._isSystemFontUsed;
        labelComponent.lineHeight = this.lineHeight;
        labelComponent.horizontalAlign = HorizontalAlign.LEFT;
        labelComponent.verticalAlign = VerticalAlign.CENTER;

        let textStyle = null;
        if (this._textArray[index]) {
            textStyle = this._textArray[index].style;
        }

        if (textStyle && textStyle.color) {
            labelNode.color = this._convertLiteralColorValue(textStyle.color);
        }else {
            labelNode.color = this.node.color;
        }

        labelComponent._enableBold(textStyle && textStyle.bold);

        labelComponent._enableItalics(textStyle && textStyle.italic);
        //TODO: temporary implementation, the italic effect should be implemented in the internal of label-assembler.
        if (textStyle && textStyle.italic) {
            labelNode.skewX = 12;
        }

        labelComponent._enableUnderline(textStyle && textStyle.underline);

        if (textStyle && textStyle.outline) {
            let labelOutlineComponent = labelNode.getComponent(cc.LabelOutline);
            if (!labelOutlineComponent) {
                labelOutlineComponent = labelNode.addComponent(cc.LabelOutline);
            }
            labelOutlineComponent.color = this._convertLiteralColorValue(textStyle.outline.color);
            labelOutlineComponent.width = textStyle.outline.width;
        }

        if (textStyle && textStyle.size) {
            labelComponent.fontSize = textStyle.size;
        }
        else {
            labelComponent.fontSize = this.fontSize;
        }

        labelComponent._updateRenderData(true);

        if (textStyle && textStyle.event) {
            if (textStyle.event.click) {
                labelNode._clickHandler = textStyle.event.click;
            }
            if (textStyle.event.param) {
                labelNode._clickParam = textStyle.event.param;
            }
        }
    },

    onDestroy () {
        for (let i = 0; i < this._labelSegments.length; ++i) {
            this._labelSegments[i].removeFromParent();
            pool.put(this._labelSegments[i]);
        }
    },
});

cc.RichText = module.exports = RichText;
