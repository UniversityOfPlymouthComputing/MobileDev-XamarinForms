namespace HelloBindings
{
    interface SayingsModel
    {
        string CurrentSaying { get; }
        int SayingNumber { get; }
        void NextSaying();
    }
}
