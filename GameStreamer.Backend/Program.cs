using Autofac;
using MassTransit;
using Autofac.Extensions.DependencyInjection;
using GameStreamer.Backend.Persistance.GameStreamerDbase;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.Run();


//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
//            .ConfigureContainer<ContainerBuilder>(ConfigueGameStreamerHost)
//            .ConfigureServices((hostContext, services) => {

//                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

//                services.AddMassTransit(x =>
//                {
//                    x.UsingRabbitMq((rmqContext, cfg) =>
//                    {
//                        cfg.Host("localhost", "xo_game", h =>
//                        {
//                            h.Username("xo_admin");
//                            h.Password("xo_admin");
//                        });
//                        cfg.ConfigureEndpoints(rmqContext);

//                    });

//                });

//                services.AddControllers();


//            });

//var app = builder.Build();
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//            name: "default",
//            pattern: "{controller=Home}/{action=Index}/{id?}");
//});

//var logger = app.Logger;
//var lifetime = app.Lifetime;
//var env = app.Environment;

//lifetime.ApplicationStarted.Register(() =>
//    logger.LogInformation(
//        $"The application {env.ApplicationName} is started.")
//);

//app.Logger.LogInformation("Hi! The GameStreamer.Backend is Running!");

//InitializeDatabase(app);

//app.Run();


//void ConfigueGameStreamerHost(HostBuilderContext builderContext, ContainerBuilder containerBuilder)//, IServiceCollection services)
//{

//    //containerBuilder.Populate(services);

//    #region Persistance Setup

//    containerBuilder.Register(context =>
//                    new DbContextOptionsBuilder<GameStreamerContext>()
//                        .UseNpgsql("Host=localhost;Database=xo_game_gamestreamerservice;User Id=local;Password=local")
//                        .Options)
//                .As<DbContextOptions<GameStreamerContext>>().SingleInstance();

//    containerBuilder.RegisterType<GameStreamerContext>().InstancePerDependency();

//    #endregion
//}

//void InitializeDatabase(IApplicationBuilder application)
//{
//    using (var scope = application.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
//    {
//        scope.ServiceProvider.GetRequiredService<GameStreamerContext>().Database.Migrate();
//    }
//}