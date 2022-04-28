using Autofac.Extensions.DependencyInjection;
using TicTacToeService;

var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webHostBuilder => {
        webHostBuilder.UseStartup<Startup>();
    })
    .Build();

host.Run();