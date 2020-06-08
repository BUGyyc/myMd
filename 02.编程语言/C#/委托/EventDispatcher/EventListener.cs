public class EventListener {
    public EventListener () { }
    public delegate void EventListenerDelegate (Event evt);
    public event EventListenerDelegate OnEvent;
    public void Excute (Event evt) {
        if (OnEvent != null) {
            OnEvent (evt);
        }
    }
}