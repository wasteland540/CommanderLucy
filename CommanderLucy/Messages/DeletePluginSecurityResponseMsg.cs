namespace CommanderLucy.Messages
{
    public class DeletePluginSecurityResponseMsg
    {
        public bool IsSure { get; private set; }

        public DeletePluginSecurityResponseMsg(bool isSure)
        {
            IsSure = isSure;
        }
    }
}