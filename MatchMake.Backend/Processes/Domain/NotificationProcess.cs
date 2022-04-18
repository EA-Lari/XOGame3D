using Hangfire;

namespace MatchMake.Backend.Domain.Processes.UserNotification_TestHangfire
{
    public class NotificationProcess
    {
        
        public void FireAndForgetJobStart()
        {
            string jobId = BackgroundJob.Enqueue(
                () => Console.WriteLine("Fire And Forget Job is Started")
                );

            Console.WriteLine($"Fire And Forget Job Id = {jobId}");
        }

        public void DelayedJobStart()
        {
            string jobId = BackgroundJob.Schedule(
                () => Console.WriteLine("Delayed Scheduling Job is started"),
                TimeSpan.FromSeconds(20)
                );
            
            Console.WriteLine($"Delayed Job Id = {jobId}");
        }

        /// <summary>
        /// Повторяющаяся задача
        /// </summary>
        public void RecurringJobStart()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Здесь будет ваше поздравление с ДР"), Cron.Daily);

            Console.WriteLine($"Recurring Job Is Planning");
        }

        /// <summary>
        /// Продолжающаяся задача
        /// </summary>
        public void ContinuationJobStart()
        {
            string jobId = BackgroundJob.Enqueue(()=>
                Console.WriteLine("Основная задача")
            );

            BackgroundJob.ContinueJobWith(jobId, () =>
                Console.WriteLine($"Продолжение задачи с jobId - {jobId} (Основная задача)")
            );

        }

    }
}
