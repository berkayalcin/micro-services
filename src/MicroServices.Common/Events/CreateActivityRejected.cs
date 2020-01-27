namespace MicroServices.Common.Events
{
    public class CreateActivityRejected:IRejectedEvent
    {
        public string Name { get; }
        public string Reason { get; }
        public string Code { get; }

        protected CreateActivityRejected() { }

        public CreateActivityRejected(string name, string reason, string code)
        {
            Name = name;
            Reason = reason;
            Code = code;
        }
    }
}