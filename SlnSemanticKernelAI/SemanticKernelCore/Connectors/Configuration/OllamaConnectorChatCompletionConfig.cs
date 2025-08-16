namespace SemanticKernelCore.Connectors.Configuration
{
    public class OllamaConnectorChatCompletionConfig : IAIConnectorConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
    }
}
