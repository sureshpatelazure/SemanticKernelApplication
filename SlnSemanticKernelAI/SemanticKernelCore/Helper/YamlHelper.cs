using YamlDotNet.Serialization;

namespace SemanticKernelCore.Helper
{
    public static class YamlHelper
    {
        public static Dictionary<string, object> ReadYaml(string yamlContent)
        {
            var deserializer = new DeserializerBuilder().Build();
            var result = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
            return result;
        }
    }
}