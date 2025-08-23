using AIServiceCore.Configuration;
using Microsoft.Extensions.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;

namespace AIServiceCore.AIService
{
    public class HuggingFaceService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent, List<object> plugins    )
        {
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();

            AppConfiguration appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("huggingface");

            hfConfig.ModelId = config.GetValue<string>("ModelId");
            hfConfig.Uri = config.GetValue<string>("Uri");
            hfConfig.ApiKey = config.GetValue<string>("ApiKey");

            return RunChatService(hfConfig, chatCompletionConnector, yamContent, plugins);

        }
    }
}