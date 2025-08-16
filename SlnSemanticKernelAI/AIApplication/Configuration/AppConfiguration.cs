using Microsoft.Extensions.Configuration;

namespace AIApplication.Configuration
{
    public class AppConfiguration
    {
        IConfiguration _connectorConfiguration;
        public AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"Configuration\\connectorsettings.json", optional: false, reloadOnChange: true);

            _connectorConfiguration = builder.Build();
        }
        public IConfigurationSection GetConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("chatcompletion").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for chatcompletion:{ConnectorName}");

            return section;

        }
    }
}