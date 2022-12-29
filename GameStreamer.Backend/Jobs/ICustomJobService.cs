namespace GameStreamer.Backend.Jobs
{
    public interface ICustomJobService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        //void DelayedJob();
        //void ContinuationJob();
    }
}