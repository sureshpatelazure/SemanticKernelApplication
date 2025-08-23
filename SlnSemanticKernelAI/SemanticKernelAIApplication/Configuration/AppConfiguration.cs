using Microsoft.Extensions.Configuration;

namespace SemanticKernelAIApplication.Configuration
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
        public IConfigurationSection GetChatCompletionConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("chatcompletion").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for chatcompletion:{ConnectorName}");

            return section;

        }

        public IConfigurationSection GetEmbeddingConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("embedding").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for embedding:{ConnectorName}");

            return section;

        }

        public IConfigurationSection GetVectorStoreConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("vectorstore").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for vectorstore:{ConnectorName}");

            return section;

        }
    }
}