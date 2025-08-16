namespace SemanticKernelCore.Connectors.Configuration
{
    public class HuggingFaceConnectorChatCompletionConfig : IAIConnectorConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
        public string ApiKey { get; set; }
    }
}
