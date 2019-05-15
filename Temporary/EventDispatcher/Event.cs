public class Event {
    public string eventName;
    public object eventParams;
    public object target;
    public Event (string eventName, object eventParams = null) {
        this.eventName = eventName;
        this.eventParams = eventParams;
    }
}