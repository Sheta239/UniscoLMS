using System;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class SavedTokenPayResponse
    {
        [JsonPropertyName("id")]
        public int id { set; get; }

        [JsonPropertyName("redirection_url")]
        public string RedirectionUrl  { set; get; }

        [JsonPropertyName("success")]
        public string success { set; get; }

        [JsonPropertyName("pending")]
        public string pending { set; get; }

        [JsonPropertyName("amount_cents")]
        public int amount_cents { set; get; }

        //[JsonPropertyName("is_auth")]
        public string is_auth { set; get; }

        //[JsonPropertyName("is_capture")]
        public string is_capture { set; get; }

        //[JsonPropertyName("is_standalone_payment")]
        public string is_standalone_payment { set; get; }

        //[JsonPropertyName("is_voided")]
        public string is_voided { set; get; }

        //[JsonPropertyName("is_refunded")]
        public string is_refunded { set; get; }

        //[JsonPropertyName("is_3d_secure")]
        public string is_3d_secure { set; get; }

        //[JsonPropertyName("integration_id")]
        public int integration_id { set; get; }

        //[JsonPropertyName("profile_id")]
        public int profile_id { set; get; }

        //[JsonPropertyName("has_parent_transaction")]
        public string has_parent_transaction { set; get; }

        //[JsonPropertyName("order")]
        public int order { set; get; }

        //[JsonPropertyName("created_at")]
        public DateTimeOffset created_at { set; get; }

        //[JsonPropertyName("currency")]
        public string currency { set; get; }

        //[JsonPropertyName("terminal_id")]
        public string terminal_id { set; get; }

        //[JsonPropertyName("merchant_commission")]
        public int merchant_commission { set; get; }

        //[JsonPropertyName("is_void")]
        public string is_void { set; get; }
        public string installment { set; get; }

        //[JsonPropertyName("is_refund")]
        public string is_refund { set; get; }

        //[JsonPropertyName("error_occured")]
        public string error_occured { set; get; }

        //[JsonPropertyName("refunded_amount_cents")]
        public int refunded_amount_cents { set; get; }

        //[JsonPropertyName("captured_amount")]
        public int captured_amount { set; get; }

        //[JsonPropertyName("merchant_staff_tag")]
        public string merchant_staff_tag { set; get; }
        public DateTimeOffset updated_at { set; get; }

        //[JsonPropertyName("owner")]
        public int owner { set; get; }

        //[JsonPropertyName("parent_transaction")]
        public string? parent_transaction { set; get; }

        //[JsonPropertyName("merchant_order_id")]
        public string? merchant_order_id { set; get; }

        [JsonPropertyName("data.message")]
        public string? DataMessage  { set; get; }

        [JsonPropertyName("source_data.type")]
        public string? SourceDataType  { set; get; }

        [JsonPropertyName("source_data.pan")]
        public string? SourceDataPan  { set; get; }

        [JsonPropertyName("source_data.sub_type")]
        public string? SourceDataSubType  { set; get; }

        [JsonPropertyName("acq_response_code")]
        public string? acq_response_code { set; get; }

        [JsonPropertyName("txn_response_code")]
        public string? txn_response_code { set; get; }

        [JsonPropertyName("hmac")]
        public string hmac { set; get; }

        [JsonPropertyName("use_redirection")]
        public bool UseRedirection  { set; get; }

        [JsonPropertyName("merchant_response")]
        public string? MerchantResponse  { set; get; }

        [JsonPropertyName("bypass_step_six")]
        public bool BypassStepSix  { set; get; }

        //public bool IsCreatedSuccessfully()
        //{
        //    return Success == "false" && Pending == "true" && ErrorOccured == "false";
        //}
    }
}
