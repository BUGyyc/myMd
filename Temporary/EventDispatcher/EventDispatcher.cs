public class EventDispather {

    public Dictionary<string, EventListener> eventDic;
    public EventDispather () {
        eventDic = new Dictionary<string, EventListener> ();
    }
    public void addEventListener (string eventName, EventListenerDelegate callBack) {
        if (!this.eventDic.ContainsKey (eventName)) {
            this.eventDic.Add (eventDic, new EventListener ());
        }
        this.eventDic[eventName].OnEvent += callBack;
    }
    public void removeEventListener (string eventName, EventListenerDelegate callBack) {
        if (this.eventDic.ContainsKey (eventName)) {
            this.eventDic[eventName].OnEvent -= callBack;
        }
    }
    public void dispatchEvent (Event evt, object target) {
        EventListener eventListener = this.eventDic[evt.eventName];
        if (eventListener == null) {
            return;
        }
        evt.target = target;
        eventListener.Excute (evt);
    }

    public bool hasEventListener (string eventName) {
        return this.eventDic.ContainsKey (eventName);
    }
}