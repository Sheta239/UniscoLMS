using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CallbackTransactionOrderCollector
    {
        private readonly IReadOnlyList<object?>? _companyEmails;
        private readonly IReadOnlyList<object?>? _phones;

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; init; } = default!;

        [JsonPropertyName("state")]
        public string? State { get; init; }

        [JsonPropertyName("country")]
        public string? Country { get; init; }

        [JsonPropertyName("city")]
        public string? City { get; init; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; init; }

        [JsonPropertyName("street")]
        public string? Street { get; init; }

        [JsonPropertyName("phones")]
        public IReadOnlyList<object?> Phones
        {
            get => _phones ?? Array.Empty<object?>();
            init => _phones = value;
        }

        [JsonPropertyName("company_emails")]
        public IReadOnlyList<object?> CompanyEmails
        {
            get => _companyEmails ?? Array.Empty<object?>();
            init => _companyEmails = value;
        }
    }
}
