using Autofac;
using Hangfire;
using Hangfire.PostgreSql;
using MatchMake.Backend.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using static MatchMake.Backend.Storage.DbaseMapping.SomeExampleContextMap;

namespace MatchMake.Backend
{
    public class Startup : ConfigModule<Settings>
    {

        private readonly Settings _config;
        private readonly ILogger<Startup> _logger;
        
        public Startup()
        {

        }
        
        protected override void Load(ContainerBuilder builder)
        {

            #region Logger

            Log.Logger = new LoggerConfiguration().CreateLogger();
            _logger.LogInformation("Стартуем логирование MatchMake.Backend!");

            #endregion

            _config.SetFromEnvironmentVariables(_logger);

            #region Database

            builder.Register(context => new DbContextOptionsBuilder<SomeExampleContext>()
                   .UseNpgsql(Config.ConnectionStrings.MatchMakeContext).Options)
                   .As<DbContextOptions<SomeExampleContext>>().SingleInstance();

            builder.RegisterType<SomeExampleContext>()
                   .InstancePerDependency();

            //builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IAsyncRepositoryWithTypedId<,>))
            //       .InstancePerDependency();

            // Add a custom scoped service
            //services.AddScoped<ITodoRepository, TodoRepository>();

            #endregion

            #region Message Broker

            #endregion

            #region Match Make Services



            #endregion

        }

        public void ConfigureServices(IServiceCollection services)
        {

            #region Job Management

            services.AddHangfire(x => x.UsePostgreSqlStorage("")); // 
            services.AddHangfireServer();

            #endregion

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/jobs_board");
           
        }
    }
}