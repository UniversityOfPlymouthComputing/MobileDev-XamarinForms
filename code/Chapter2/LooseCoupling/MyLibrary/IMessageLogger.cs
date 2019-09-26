namespace MyLibrary
{
    public interface IMessageLogger
    {
        void LogMessage(string msg);
        void Complete(bool b);
    }

}
