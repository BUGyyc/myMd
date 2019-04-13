
const RenderComponent = require('../components/CCRenderComponent');
const BlendFactor = require('../platform/CCMacro').BlendFactor;
const gfx = require('../../renderer/gfx');

let BlendFunc = cc.Class({
    properties: {
        _srcBlendFactor: BlendFactor.SRC_ALPHA,
        _dstBlendFactor: BlendFactor.ONE_MINUS_SRC_ALPHA,

          /**
         * !#en specify the source Blend Factor, this will generate a custom material object, please pay attention to the memory cost.
         * !#zh 指定原图的混合模式，这会克隆一个新的材质对象，注意这带来的开销
         * @property srcBlendFactor
         * @type {macro.BlendFactor}
         * @example
         * sprite.srcBlendFactor = cc.macro.BlendFactor.ONE;
         */
        srcBlendFactor: {
            get: function() {
                return this._srcBlendFactor;
            },
            set: function(value) {
                if (this._srcBlendFactor === value) return;
                this._srcBlendFactor = value;
                this._updateBlendFunc();
            },
            animatable: false,
            type:BlendFactor,
            tooltip: CC_DEV && 'i18n:COMPONENT.sprite.src_blend_factor',
            visible: false
        },

        /**
         * !#en specify the destination Blend Factor.
         * !#zh 指定目标的混合模式
         * @property dstBlendFactor
         * @type {macro.BlendFactor}
         * @example
         * sprite.dstBlendFactor = cc.macro.BlendFactor.ONE;
         */
        dstBlendFactor: {
            get: function() {
                return this._dstBlendFactor;
            },
            set: function(value) {
                if (this._dstBlendFactor === value) return;
                this._dstBlendFactor = value;
                this._updateBlendFunc();
            },
            animatable: false,
            type: BlendFactor,
            tooltip: CC_DEV && 'i18n:COMPONENT.sprite.dst_blend_factor',
            visible: false
        },
    },

    setMaterial (index, material) {
        RenderComponent.prototype.setMaterial.call(this, index, material);
        if (material) {
            this._updateMaterialBlendFunc(material);
        }
    },

    _updateBlendFunc () {
        let materials = this._materials;
        for (let i = 0; i < materials.length; i++) {
            let material = materials[i];
            this._updateMaterialBlendFunc(material);
        }
    },

    _updateMaterialBlendFunc (material) {
        let passes = material._effect.getDefaultTechnique().passes;
        for (let j = 0; j < passes.length; j++) {
            let pass = passes[j];
            pass.setBlend(
                true,
                gfx.BLEND_FUNC_ADD,
                this._srcBlendFactor, this._dstBlendFactor,
                gfx.BLEND_FUNC_ADD,
                this._srcBlendFactor, this._dstBlendFactor
            );
        }
    },
});

module.exports = BlendFunc;
