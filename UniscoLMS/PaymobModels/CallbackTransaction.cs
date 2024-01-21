using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace UniscoLMS.PaymobModels
{
    public class CallbackTransaction
    {
        private readonly IReadOnlyList<TransactionProcessedCallbackResponseModel>? _transactionProcessedCallbackResponses;

        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("pending")]
        public bool Pending { get; init; }

        
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("is_auth")]
        public bool IsAuth { get; init; }

        [JsonPropertyName("is_capture")]
        public bool IsCapture { get; init; }

        [JsonPropertyName("is_voided")]
        public bool IsVoided { get; init; }

        [JsonPropertyName("is_refunded")]
        public bool IsRefunded { get; init; }

        [JsonPropertyName("is_3d_secure")]
        public bool Is3dSecure { get; init; }

        [JsonPropertyName("is_standalone_payment")]
        public bool IsStandalonePayment { get; init; }

        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; init; }

        [JsonPropertyName("profile_id")]
        public int ProfileId { get; init; }

        [JsonPropertyName("has_parent_transaction")]
        public bool HasParentTransaction { get; init; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; init; } = default!;

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("api_source")]
        public string ApiSource { get; init; } = default!;

        [JsonPropertyName("merchant_commission")]
        public int MerchantCommission { get; init; }

        [JsonPropertyName("is_void")]
        public bool IsVoid { get; init; }

        [JsonPropertyName("is_refund")]
        public bool IsRefund { get; init; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; init; }

        [JsonPropertyName("error_occured")]
        public bool ErrorOccured { get; init; }

        [JsonPropertyName("is_live")]
        public bool IsLive { get; init; }

        [JsonPropertyName("refunded_amount_cents")]
        public int RefundedAmountCents { get; init; }

        [JsonPropertyName("source_id")]
        public int SourceId { get; init; }

        [JsonPropertyName("is_captured")]
        public bool IsCaptured { get; init; }

        [JsonPropertyName("captured_amount")]
        public int CapturedAmount { get; init; }

        [JsonPropertyName("owner")]
        public int Owner { get; init; }

        [JsonPropertyName("terminal_id")]
        public string? TerminalId { get; init; }

        [JsonPropertyName("data")]
        public CallbackTransactionData? Data { get; init; }

        [JsonPropertyName("order")]
        public CallbackTransactionOrder Order { get; init; } = default!;

        [JsonPropertyName("payment_key_claims")]
        public PayPaymentKeyClaims? PaymentKeyClaims { get; init; }

        [JsonPropertyName("source_data")]
        public CallbackTransactionSourceData? SourceData { get; init; }

        [JsonPropertyName("transaction_processed_callback_responses")]
        public IReadOnlyList<TransactionProcessedCallbackResponseModel> TransactionProcessedCallbackResponses
        {
            get => _transactionProcessedCallbackResponses ?? Array.Empty<TransactionProcessedCallbackResponseModel>();
            init => _transactionProcessedCallbackResponses = value;
        }

        [JsonPropertyName("other_endpoint_reference")]
        public object? OtherEndpointReference { get; init; }

        [JsonPropertyName("merchant_staff_tag")]
        public object? MerchantStaffTag { get; init; }

        [JsonPropertyName("parent_transaction")]
        public int? ParentTransaction { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }

        public string ToConcatenatedString()
        {
            static string toString(bool value) => value ? "true" : "false";

            return
                AmountCents.ToString() +
                CreatedAt +
                Currency +
                toString(ErrorOccured) +
                toString(HasParentTransaction) +
                Id.ToString() +
                IntegrationId.ToString() +
                toString(Is3dSecure) +
                toString(IsAuth) +
                toString(IsCapture) +
                toString(IsRefunded) +
                toString(IsStandalonePayment) +
                toString(IsVoided) +
                Order.Id.ToString() +
                Owner.ToString() +
                toString(Pending) +
                SourceData?.Pan?.ToLowerInvariant() +
                SourceData?.SubType +
                SourceData?.Type?.ToLowerInvariant() +
                toString(Success);
        }
    }
}
