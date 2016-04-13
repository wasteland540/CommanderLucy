namespace CommanderLucy.Messages
{
    public class CannotAddPluginMsg
    {
        public CannotAddPluginMsg(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}