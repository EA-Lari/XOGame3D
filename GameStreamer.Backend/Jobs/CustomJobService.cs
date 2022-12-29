namespace GameStreamer.Backend.Jobs
{
    public class CustomJobService : ICustomJobService
    {
        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }

        public void ReccuringJob()
        {
            Console.WriteLine("Hello from a Scheduled job!");
        }
    }
}