/****************************************************************************
 Copyright (c) 2016 Chukong Technologies Inc.
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

const Fs = require('fire-fs');
const Path = require('fire-path');
const DRAGONBONES_ENCODING = { encoding: 'utf-8' };
const CustomAssetMeta = Editor.metas['custom-asset'];

class DragonBonesAtlasMeta extends CustomAssetMeta {
    constructor (assetdb) {
        super(assetdb);
        this.atlasJson = '';
        this.texture = '';
    }

    static version () { return '1.0.0'; }
    static defaultType () {
        return 'dragonbones-atlas';
    }

    static validate (assetpath) {
        var json;
        var text = Fs.readFileSync(assetpath, 'utf8');
        try {
            json = JSON.parse(text);
        }
        catch (e) {
            return false;
        }

        return typeof json.imagePath === 'string' && Array.isArray(json.SubTexture);
    }

    postImport (fspath, cb) {
        Fs.readFile(fspath, DRAGONBONES_ENCODING, (err, data) => {
            if (err) {
                return cb(err);
            }

            var json;
            try {
                json = JSON.parse(data);
            }
            catch (e) {
                return cb(e);
            }

            // record the raw file data
            this.atlasJson = data;

            // parse the depended texture
            var imgPath = Path.resolve(Path.dirname(fspath), json.imagePath);
            var uuid = this._assetdb.fspathToUuid(imgPath);
            if (uuid) {
                console.log('UUID is initialized for "%s".', imgPath);
                this.texture = uuid;
            }
            else if (!Fs.existsSync(imgPath)) {
                Editor.error('Can not find texture "%s" for atlas "%s"', json.imagePath, fspath);
            }
            else {
                // AssetDB may call postImport more than once, we can get uuid in the next time.
                console.warn('WARN: UUID not yet initialized for "%s".', json.imagePath);
            }

            var asset = new dragonBones.DragonBonesAtlasAsset();
            asset.name = Path.basenameNoExt(fspath);
            asset.atlasJson = this.atlasJson;
            asset.texture = Editor.serialize.asAsset(this.texture);
            this._assetdb.saveAssetToLibrary(this.uuid, asset);
            cb();
        });
    }
}

module.exports = DragonBonesAtlasMeta;
