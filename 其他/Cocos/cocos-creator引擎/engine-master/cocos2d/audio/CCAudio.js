/****************************************************************************
 Copyright (c) 2008-2010 Ricardo Quesada
 Copyright (c) 2011-2012 cocos2d-x.org
 Copyright (c) 2013-2016 Chukong Technologies Inc.
 Copyright (c) 2017-2018 Xiamen Yaji Software Co., Ltd.

 http://www.cocos2d-x.org

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
 ****************************************************************************/

const EventTarget = require('../core/event/event-target');
const sys = require('../core/platform/CCSys');
const LoadMode = require('../core/assets/CCAudioClip').LoadMode;

let touchBinded = false;
let touchPlayList = [
    //{ instance: Audio, offset: 0, audio: audio }
];

let Audio = function (src) {
    EventTarget.call(this);

    this._src = src;
    this._element = null;
    this.id = 0;

    this._volume = 1;
    this._loop = false;
    this._nextTime = 0;  // playback position to set

    this._state = Audio.State.INITIALZING;

    this._onended = function () {
        this._state = Audio.State.STOPPED;
        this.emit('ended');
    }.bind(this);
};

cc.js.extend(Audio, EventTarget);

/**
 * !#en Audio state.
 * !#zh 声音播放状态
 * @enum audioEngine.AudioState
 * @memberof cc
 */
// TODO - At present, the state is mixed with two states of users and systems, and it is best to split into two types. A "loading" should also be added to the system state.
Audio.State = {
    /**
     * @property {Number} ERROR
     */
    ERROR : -1,
    /**
     * @property {Number} INITIALZING
     */
    INITIALZING: 0,
    /**
     * @property {Number} PLAYING
     */
    PLAYING: 1,
    /**
     * @property {Number} PAUSED
     */
    PAUSED: 2,
    /**
     * @property {Number} STOPPED
     */
    STOPPED: 3,
};

(function (proto) {

    proto._bindEnded = function (callback) {
        callback = callback || this._onended;
        let elem = this._element;
        if (this._src && (elem instanceof HTMLAudioElement)) {
            elem.addEventListener('ended', callback);
        } else {
            elem.onended = callback;
        }
    };

    proto._unbindEnded = function () {
        let elem = this._element;
        if (elem instanceof HTMLAudioElement) {
            elem.removeEventListener('ended', this._onended);
        } else if (elem) {
            elem.onended = null;
        }
    };

    // proto.mount = function (elem) {
    //     if (CC_DEBUG) {
    //         cc.warn('Audio.mount(value) is deprecated. Please use Audio._onLoaded().');
    //     }
    // };

    proto._onLoaded = function () {
        let elem = this._src._nativeAsset;
        if (elem instanceof HTMLAudioElement) {
            // Reuse dom audio element
            if (!this._element) {
                this._element = document.createElement('audio');
            }
            this._element.src = elem.src;
        }
        else {
            this._element = new WebAudioElement(elem, this);
        }

        this.setVolume(this._volume);
        this.setLoop(this._loop);
        if (this._nextTime !== 0) {
            this.setCurrentTime(this._nextTime);
        }
        if (this._state === Audio.State.PLAYING) {
            this.play();
        }
        else {
            this._state = Audio.State.INITIALZING;
        }
    };

    proto.play = function () {
        // marked as playing so it will playOnLoad
        this._state = Audio.State.PLAYING;

        if (!this._element) {
            return;
        }

        this._bindEnded();
        this._element.play();

        if (!CC_QQPLAY && !CC_WECHATGAME) {
            if (this._src && this._src.loadMode === LoadMode.DOM_AUDIO &&
                this._element.paused) {
                touchPlayList.push({ instance: this, offset: 0, audio: this._element });
            }

            if (touchBinded) return;
            touchBinded = true;

            // Listen to the touchstart body event and play the audio when necessary.
            cc.game.canvas.addEventListener('touchstart', function () {
                let item;
                while (item = touchPlayList.pop()) {
                    item.audio.play(item.offset);
                }
            });
        }
    };

    proto.destroy = function () {
        if (CC_WECHATGAME || CC_QQPLAY) {
            this._element && this._element.destroy();
        }
        this._element = null;
    };

    proto.pause = function () {
        if (!this._element || this._state !== Audio.State.PLAYING) return;
        this._unbindEnded();
        this._element.pause();
        this._state = Audio.State.PAUSED;
    };

    proto.resume = function () {
        if (!this._element || this._state !== Audio.State.PAUSED) return;
        this._bindEnded();
        this._element.play();
        this._state = Audio.State.PLAYING;
    };

    proto.stop = function () {
        if (!this._element) return;
        this._element.pause();
        try {
            this._element.currentTime = 0;
        } catch (error) {}
        // remove touchPlayList
        for (let i = 0; i < touchPlayList.length; i++) {
            if (touchPlayList[i].instance === this) {
                touchPlayList.splice(i, 1);
                break;
            }
        }
        this._unbindEnded();
        this.emit('stop');
        this._state = Audio.State.STOPPED;
    };

    proto.setLoop = function (loop) {
        this._loop = loop;
        if (this._element) {
            this._element.loop = loop;
        }
    };
    proto.getLoop = function () {
        return this._loop;
    };

    proto.setVolume = function (num) {
        this._volume = num;
        if (this._element) {
            this._element.volume = num;
        }
    };
    proto.getVolume = function () {
        return this._volume;
    };

    proto.setCurrentTime = function (num) {
        if (this._element) {
            this._nextTime = 0;
        }
        else {
            this._nextTime = num;
            return;
        }

        if (!(CC_QQPLAY || CC_WECHATGAME)) {
            // setCurrentTime would fire 'ended' event
            // so we need to change the callback to rebind ended callback after setCurrentTime
            this._unbindEnded();
            this._bindEnded(function () {
                this._bindEnded();
            }.bind(this));
        }
        try {
            this._element.currentTime = num;
        }
        catch (err) {
            let _element = this._element;
            if (_element.addEventListener) {
                let func = function () {
                    _element.removeEventListener('loadedmetadata', func);
                    _element.currentTime = num;
                };
                _element.addEventListener('loadedmetadata', func);
            }
        }
    };
    proto.getCurrentTime = function () {
        return this._element ? this._element.currentTime : 0;
    };

    proto.getDuration = function () {
        return this._element ? this._element.duration : 0;
    };

    proto.getState = function () {
        let elem = this._element;
        if (!CC_WECHATGAME && !CC_QQPLAY && elem) {
            // HACK: in some browser, audio may not fire 'ended' event
            // so we need to force updating the Audio state
            if (Audio.State.PLAYING === this._state && elem.paused) {
                this._state = Audio.State.STOPPED;
            }
            else if (Audio.State.STOPPED === this._state && !elem.paused) {
                this._state = Audio.State.PLAYING;
            }
        }
        return this._state;
    };

    proto.__defineGetter__('src', function () {
        return this._src;
    });
    proto.__defineSetter__('src', function (clip) {
        this._unbindEnded();
        if (clip) {
            this._src = clip;
            if (clip.loaded) {
                this._onLoaded();
            }
            else {
                let self = this;
                clip.once('load', function () {
                    if (clip === self._src) {
                        self._onLoaded();
                    }
                });
                cc.loader.load({
                    url: clip.nativeUrl,
                    // For audio, we should skip loader otherwise it will load a new audioClip.
                    skips: ['Loader'],
                },
                function (err, audioNativeAsset) {
                    if (err) {
                        cc.error(err);
                        return;
                    }
                    if (!clip.loaded) {
                        clip._nativeAsset = audioNativeAsset;
                    }
                });
            }
        }
        else {
            this._src = null;
            if (this._element instanceof HTMLAudioElement) {
                this._element.src = '';
            }
            else {
                this._element = null;
            }
            this._state = Audio.State.INITIALZING;
        }
        return clip;
    });

    proto.__defineGetter__('paused', function () {
        return this._element ? this._element.paused : true;
    });

    // setFinishCallback

})(Audio.prototype);

// Encapsulated WebAudio interface
let WebAudioElement = function (buffer, audio) {
    this._audio = audio;
    this._context = sys.__audioSupport.context;
    this._buffer = buffer;

    this._gainObj = this._context['createGain']();
    this._volume = 1;
    // https://www.chromestatus.com/features/5287995770929152
    if (this._gainObj['gain'].setTargetAtTime) {
        this._gainObj['gain'].setTargetAtTime(this._volume, this._context.currentTime, 0.01);
    } else {
        this._gainObj['gain'].value = 1;
    }
    this._gainObj['connect'](this._context['destination']);

    this._loop = false;
    // The time stamp on the audio time axis when the recording begins to play.
    this._startTime = -1;
    // Record the currently playing 'Source'
    this._currentSource = null;
    // Record the time has been played
    this.playedLength = 0;

    this._currextTimer = null;

    this._endCallback = function () {
        if (this.onended) {
            this.onended(this);
        }
    }.bind(this);
};

(function (proto) {
    proto.play = function (offset) {
        // If repeat play, you need to stop before an audio
        if (this._currentSource && !this.paused) {
            this._currentSource.onended = null;
            this._currentSource.stop(0);
            this.playedLength = 0;
        }

        let audio = this._context["createBufferSource"]();
        audio.buffer = this._buffer;
        audio["connect"](this._gainObj);
        audio.loop = this._loop;

        this._startTime = this._context.currentTime;
        offset = offset || this.playedLength;
        if (offset) {
            this._startTime -= offset;
        }
        let duration = this._buffer.duration;

        let startTime = offset;
        let endTime;
        if (this._loop) {
            if (audio.start)
                audio.start(0, startTime);
            else if (audio["notoGrainOn"])
                audio["noteGrainOn"](0, startTime);
            else
                audio["noteOn"](0, startTime);
        } else {
            endTime = duration - offset;
            if (audio.start)
                audio.start(0, startTime, endTime);
            else if (audio["notoGrainOn"])
                audio["noteGrainOn"](0, startTime, endTime);
            else
                audio["noteOn"](0, startTime, endTime);
        }

        this._currentSource = audio;

        audio.onended = this._endCallback;

        // If the current audio context time stamp is 0 and audio context state is suspended
        // There may be a need to touch events before you can actually start playing audio
        if ((!audio.context.state || audio.context.state === "suspended") && this._context.currentTime === 0) {
            let self = this;
            clearTimeout(this._currextTimer);
            this._currextTimer = setTimeout(function () {
                if (!(CC_QQPLAY || CC_WECHATGAME) && self._context.currentTime === 0) {
                    touchPlayList.push({
                        instance: self._audio,
                        offset: offset,
                        audio: self
                    });
                }
            }, 10);
        }
    };

    proto.pause = function () {
        clearTimeout(this._currextTimer);
        if (this.paused) return;
        // Record the time the current has been played
        this.playedLength = this._context.currentTime - this._startTime;
        // If more than the duration of the audio, Need to take the remainder
        this.playedLength %= this._buffer.duration;
        let audio = this._currentSource;
        this._currentSource = null;
        this._startTime = -1;
        if (audio)
            audio.stop(0);
    };

    proto.__defineGetter__('paused', function () {
        // If the current audio is a loop, paused is false
        if (this._currentSource && this._currentSource.loop)
            return false;

        // startTime default is -1
        if (this._startTime === -1)
            return true;

        // Current time -  Start playing time > Audio duration
        return this._context.currentTime - this._startTime > this._buffer.duration;
    });

    proto.__defineGetter__('loop', function () { return this._loop; });
    proto.__defineSetter__('loop', function (bool) {
        if (this._currentSource)
            this._currentSource.loop = bool;

        return this._loop = bool;
    });

    proto.__defineGetter__('volume', function () {
        return this._volume;
    });
    proto.__defineSetter__('volume', function (num) {
        this._volume = num;
        if (this._gainObj['gain'].setTargetAtTime) {
            this._gainObj['gain'].setTargetAtTime(this._volume, this._context.currentTime, 0.01);
        } else {
            this._volume['gain'].value = num;
        }
        if (sys.os === sys.OS_IOS && !this.paused && this._currentSource) {
            // IOS must be stop webAudio
            this._currentSource.onended = null;
            this.pause();
            this.play();
        }
        return num;
    });

    proto.__defineGetter__('currentTime', function () {
        if (this.paused) {
            return this.playedLength;
        }
        // Record the time the current has been played
        this.playedLength = this._context.currentTime - this._startTime;
        // If more than the duration of the audio, Need to take the remainder
        this.playedLength %= this._buffer.duration;
        return this.playedLength;
    });
    proto.__defineSetter__('currentTime', function (num) {
        if (!this.paused) {
            this.pause();
            this.playedLength = num;
            this.play();
        } else {
            this.playedLength = num;
        }
        return num;
    });

    proto.__defineGetter__('duration', function () {
        return this._buffer.duration;
    });

})(WebAudioElement.prototype);

module.exports = cc.Audio = Audio;
