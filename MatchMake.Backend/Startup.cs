using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MatchMake.Backend
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            Console.WriteLine("ConfigureServices in Startup works!");

            //services.AddHangfire(configuration =>
            //{
            //    configuration.UsePostgreSqlStorage("Host=postgres;Port=30000;Database=xo_admin;User Id=xo_admin;Password=xo_admin;");
            //});

            //services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IRecurringJobManager recurringJobManager)
        {
            
            //app.UseHangfireDashboard("/jobs");

            #region Match Make Domain Processes

            //BackgroundJob.Enqueue(
            //                () => Console.WriteLine("Hello Single Job from Hangfire!")
            //                );

            //RecurringJob.AddOrUpdate<IParallelProcess>("HelloWorldTestProcess", x => x.StartAsync(new CancellationToken()), Cron.Minutely);
            //RecurringJob.AddOrUpdate<IParallelProcess>("NotificationProcess", x => x.StartAsync(new CancellationToken()), Cron.Minutely);

            //RecurringJob.AddOrUpdate(
            //                () => Console.WriteLine("Hello Recurring Job from Hangfire!"),
            //                Cron.Minutely
            //                );

            //RecurringJob.AddOrUpdate<IParallelProcess>("healthCheckProcess", x => x.StartAsync(new CancellationToken()), Cron.Minutely);

            // Create Auto Factory which register dependencies as function

            //builder.Register<Func<Type, IParallelProcess>>(x =>
            //{
            //    var context = x.Resolve<IComponentContext>();
            //    return y =>
            //    {
            //        return (IParallelProcess)context.Resolve(y);
            //    };
            //})
            //    .SingleInstance();

            #endregion

            // Hangfire Docs
            // https://www.youtube.com/watch?v=iilRdmNILC8
            // https://github.com/codaza/Hangfire
        }

    }
}