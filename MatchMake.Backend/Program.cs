using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using MatchMake.Backend.Contracts;
using MatchMake.Backend.Processes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddSingleton<IParallelProcess, HelloWorldTestProcess>();

builder.Services.AddHangfire(configuration =>
                    {
                        configuration.UsePostgreSqlStorage("Host=localhost;Port=25432;Database=xo_admin;User Id=xo_admin;Password=xo_admin;", new PostgreSqlStorageOptions() { SchemaName = "matchmake_jobs"});
                    });

builder.Services.AddHangfireServer();

var app = builder.Build();

IParallelProcess process = app.Services.GetRequiredService<IParallelProcess>();
ILogger logger = app.Logger;
IHostApplicationLifetime lifetime = app.Lifetime;
IWebHostEnvironment env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} started" +
        $" with injected {process}")
);

app.Logger.LogInformation("Hi! The MatchMake.Backend is Running!");
app.UseHangfireDashboard("/jobs");

#region Scheduling Jobs

RecurringJob.AddOrUpdate<IParallelProcess>("HelloWorldTestProcess", x => x.StartAsync(new CancellationToken()), Cron.Minutely);

//BackgroundJob.Enqueue(
//                () => Console.WriteLine("Test Job")
//                );

#endregion

app.Run();