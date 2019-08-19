class EventDispatcher {
    constructor() {
        this.events = {};
    }

    addEventListener(name, callBack) {
        let callBacks = this.events[name] || [];
        callBacks.push(callBack);
        this.events[name] = callBacks;
    }

    removeEventListener(name, callBack) {
        let callBacks = this.events[name];
        callBacks = callBacks.filter(fn => fn != callBack);
        this.events[name] = callBacks;
    }

    once(name, callBack) {
        let func = (...args) => {
            callBack.apply(args);
            this.removeEventListener(name, func);
        }
        this.addEventListener(name, func);
    }

    dispatcher(...args) {
        let name = args[0];
        let params = [].splice.call(args, 1);
        let callBacks = this.events[name];
        callBacks.forEach(fn => fn.apply(params));
    }
}

let callBack1 = function(){
    console.log("11111111111111111111")
}

let callBack2 = function(){
    console.log("2222222")
}

let callBack3 = function(){
    console.log("3333")
}

let eventSystem = new EventDispatcher();
eventSystem.addEventListener("event1",callBack1);
eventSystem.addEventListener("event2",callBack2);
eventSystem.once("event3",callBack3);

eventSystem.dispatcher("event1")
eventSystem.dispatcher("event2")
eventSystem.dispatcher("event3")

eventSystem.removeEventListener("event1",callBack1)

eventSystem.dispatcher("event1")
eventSystem.dispatcher("event2")
eventSystem.dispatcher("event3")