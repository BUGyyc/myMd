require("Pool");
(function (PureMVC) {
    class EventSystem {
        constructor() {
            this.events = {};
            this.onceList = [];
            this.isCanceled = false;
        }

        dispatchCancel() {
            this.isCanceled = true;
        }

        dispatchEvent(type, args, cancelDispatch = false) {
            if (type == null) return;
            const list = this.events[type];
            if (list == null || list.length == 1) return;
            list[0] = true;
            const isCanceled = this.isCanceled;
            this.isCanceled = false;
            for (let i = 1; i < list.length; i++) {
                const item = list[i];
                if (item.receiveOnce) {
                    this.list.push(item);
                }
                if (args === void 0) {
                    item.method.call(item.caller);
                } else if (args instanceof Array) {
                    item.method.apply(item.caller, args);
                } else {
                    item.method.call(item.caller, args);
                }
                if (cancelDispatch && this.isCanceled) {
                    break;
                }
            }
            this.isCanceled = isCanceled;
            list[0] = false;
            while (this.onceList.length) {
                const item = this.onceList.pop();
                removeEventListener(item.type, item.method, item.caller);
            }
        }

        addEventListener(type, method, caller) {

        }

        removeEventListener(type, method, caller) {

        }
    }
    PureMVC.EventSystem = EventSystem;
})(window.PureMVC || (window.PureMVC = {}));