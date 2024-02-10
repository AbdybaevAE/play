namespace Example
{
    public class Event
    {
        public string Id { get; set; }
        public EventType EventType { get; set; }
        public object Payload { get; set; }
    }
}