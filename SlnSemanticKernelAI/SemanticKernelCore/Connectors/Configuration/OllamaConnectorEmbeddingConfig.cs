namespace SemanticKernelCore.Connectors.Configuration
{
    public class OllamaConnectorEmbeddingConfig : IAIConnectorConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
    }
}
