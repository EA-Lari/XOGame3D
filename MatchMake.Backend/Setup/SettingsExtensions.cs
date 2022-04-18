using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MatchMake.Backend.Setup
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Чтение конфигурации json и переменных окружения
        /// </summary>
        /// <param name="settings">Конфигурация, будет заполнена значениями.</param>
        /// <param name="logger">Логер</param>
        public static void SetFromEnvironmentVariables(this Settings settings, ILogger logger)
        {
            logger.LogInformation("Configuration is loading.");
            IConfiguration config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .AddJsonFile("appsettings.development.json")
                  .AddEnvironmentVariables()
                  .Build();
            config.Bind(settings);

            logger.LogTrace("Configuration loaded.", new { settings = settings });
        }
    }
}