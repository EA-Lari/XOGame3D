using Autofac;
using MassTransit;

namespace TicTacToeService
{
    public class Startup
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<XOGame3D.Controllers.TicTacToeMicroserviceController>().InstancePerDependency();
            builder.AddMassTransit(conf => conf.UsingRabbitMq());//https://masstransit-project.com/usage/configuration.html#consumers
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    options.WaitUntilStarted = true;
                    options.StartTimeout = TimeSpan.FromSeconds(10);
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}
