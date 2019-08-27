public class Event {
    public string eventName;
    public object eventParams;
    public object target;
    public Event (string name, object eventParams = null) {
        this.eventName = name;
        this.eventParams = eventParams;
    }
}