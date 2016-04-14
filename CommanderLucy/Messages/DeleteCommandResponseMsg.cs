namespace CommanderLucy.Messages
{
    public class DeleteCommandResponseMsg
    {
        public DeleteCommandResponseMsg(bool isSure)
        {
            IsSure = isSure;
        }

        public bool IsSure { get; private set; }
    }
}