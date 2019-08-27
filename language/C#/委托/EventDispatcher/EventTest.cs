public class EventTest {
    public static int main (string[] args) {
        System.Console.WriteLine ("C Sharp----------------------");
    }
}

class Event {
    public string eventName;
    public object eventParams;
    public object target;
    public Event (string eventName, object eventParams) {
        this.eventName = eventName;
        this.eventParams = eventParams;
    }
}

class EventName {
    public static string TEST = "test";
}

class EventListener {
    public EventListener () { }
    public delegate void EventListenerDelegate (Event evt);
    public event EventListenerDelegate OnEvent;
    public void Excute (Event evt) {
        if (OnEvent != null) {
            OnEvent (evt);
        }
    }
}

class EventDispatcher {
    public Dictionary<string, EventListener> eventDic;
    public EventDispatcher () {
        eventDic = new Dictionary<string, EventListener> ();
    }

    public void AddEventListener (string name, EventListenerDelegate callBack) {
        if (!eventDic.ContainsKey (name)) {
            eventDic.Add (name, new EventListener ());
        }
        this.eventDic[name] += callBack;
    }

    public void RemoveEventListener (string name, EventListenerDelegate callBack) {
        if (eventDic.ContainsKey (name)) {
            this.eventDic[name] -= callBack;
        }
    }

    public void DispatchEvent (Event evt, object target) {
        EventListener eventListener = this.eventDic[evt.eventName];
        if (eventListener != null) {
            evt.target = target;
            eventListener.Excute (evt);
        }
    }
}