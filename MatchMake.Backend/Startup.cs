using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MatchMake.Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UsePostgreSqlStorage("SomeConnString"));
            services.AddHangfireServer();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard("/hangfire_board");
        }
    }
}