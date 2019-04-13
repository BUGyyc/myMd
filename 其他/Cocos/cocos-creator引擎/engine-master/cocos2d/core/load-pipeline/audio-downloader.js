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

const sys = require('../platform/CCSys');
const debug = require('../CCDebug');

var __audioSupport = sys.__audioSupport;
var formatSupport = __audioSupport.format;
var context = __audioSupport.context;

function loadDomAudio (item, callback) {
    var dom = document.createElement('audio');
    dom.src = item.url;

    const isBaiduGame = (cc.sys.platform === cc.sys.BAIDU_GAME);
    if (CC_WECHATGAME || isBaiduGame) {
        callback(null, dom);
        return;
    }

    var clearEvent = function () {
        clearTimeout(timer);
        dom.removeEventListener("canplaythrough", success, false);
        dom.removeEventListener("error", failure, false);
        if(__audioSupport.USE_LOADER_EVENT)
            dom.removeEventListener(__audioSupport.USE_LOADER_EVENT, success, false);
    };
    var timer = setTimeout(function () {
        if (dom.readyState === 0)
            failure();
        else
            success();
    }, 8000);
    var success = function () {
        clearEvent();
        callback(null, dom);
    };
    var failure = function () {
        clearEvent();
        var message = 'load audio failure - ' + item.url;
        cc.log(message);
        callback(message);
    };
    dom.addEventListener("canplaythrough", success, false);
    dom.addEventListener("error", failure, false);
    if(__audioSupport.USE_LOADER_EVENT)
        dom.addEventListener(__audioSupport.USE_LOADER_EVENT, success, false);
}

function loadWebAudio (item, callback) {
    if (!context)
        callback(new Error(debug.getError(4926)));

    var request = cc.loader.getXMLHttpRequest();
    request.open("GET", item.url, true);
    request.responseType = "arraybuffer";

    // Our asynchronous callback
    request.onload = function () {
        context["decodeAudioData"](request.response, function(buffer){
            //success
            callback(null, buffer);
        }, function(){
            //error
            callback('decode error - ' + item.id, null);
        });
    };

    request.onerror = function(){
        callback('request error - ' + item.id, null);
    };

    request.send();
}

function downloadAudio (item, callback) {
    if (formatSupport.length === 0) {
        return new Error(debug.getError(4927));
    }

    var loader;
    if (!__audioSupport.WEB_AUDIO) {
        // If WebAudio is not supported, load using DOM mode
        loader = loadDomAudio;
    }
    else {
        var loadByDeserializedAudio = item._owner instanceof cc.AudioClip;
        if (loadByDeserializedAudio) {
            loader = (item._owner.loadMode === cc.AudioClip.LoadMode.WEB_AUDIO) ? loadWebAudio : loadDomAudio;
        }
        else {
            loader = (item.urlParam && item.urlParam['useDom']) ? loadDomAudio : loadWebAudio;
        }
    }
    loader(item, callback);
}

module.exports = downloadAudio;
