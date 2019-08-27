public class EventDispatcher {
    public Dictionary<string, EventListener> eventMap;
    public EventDispatcher () {
        eventMap = new Dictionary<string, EventListener> ();
    }

    public void AddEventListener (string eventName, EventListenerDelegate callBack) {
        if (!eventMap.ContainsKey (eventName)) {
            this.eventMap.Add (eventName, new EventListener ());
        }
        this.eventMap[eventName].OnEvent += callBack;
    }

    public void RemoveEventListener (string eventName, EventListenerDelegate callBack) {
        if (eventMap.ContainsKey (eventName)) {
            this.eventMap[eventName].OnEvent -= callBack;
        }
    }

    public void DispatcheEvent (Event evt, object target) {
        EventListener eventListener = this.eventMap[evt.eventName];
        if (eventListener != null) {
            evt.target = target;
            eventListener.Excute ();
        }
    }
}