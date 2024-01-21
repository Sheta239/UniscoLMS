using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class Callback
    {
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("obj")]
        public object? Obj { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
