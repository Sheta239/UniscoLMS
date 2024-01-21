using UniscoLMS.PaymobModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UniscoLMS.ViewModels.Responses;
using UniscoLMS.ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniscoLMS.Enums;
using UniscoLMS;
using ViewModels;
using System.Linq.Expressions;

namespace UniscoLMS.Services
{
    public class PaymobService
    {
        private readonly IConfiguration _configuration; private string authToken;

        public PaymobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> PaymobPay(PaymentRequest request)
        {
            try
            {
                
                var amountCents = request.amount * 100;
                CreateOrderRequest orderRequest = CreateOrderRequest.CreateOrder((long)amountCents, "EGP", request.trn);
                var orderResponse = await CreateOrderAsync(orderRequest);


                InitializeRequest initializeRequest = new InitializeRequest();
                initializeRequest.trn = request.trn;
                initializeRequest.OrderId = orderResponse.Id.ToString();
                var response = new initializeResponse();
                //await _HttpRequest.PostRequest<initializeResponse>("almentor_b2b_get_user_service", "sso_private_service", uuid,
                //        _configuration["Urls:InitializeUrl"], initializeRequest);

                var Name = request.user.firstName;
                var lastName = request.user.lastName;


                var billingData = new BillingData(
                         firstName: Name,
                         lastName: lastName,
                         phoneNumber: "NA",
                         email: request.user.email);

                var integrtion_id = Convert.ToInt32(_configuration["Paymob:integratinId"]);
                var paymentKeyRequest = new PaymentKeyRequest(
                        integrationId: integrtion_id,
                        orderId: orderResponse.Id,
                        billingData: billingData,
                        amountCents: (int)amountCents

                       );

                var paymentKeyResponse = await RequestPaymentKeyAsync(paymentKeyRequest);
                FrameRequest frameresponse = new FrameRequest();
                //if (request.remember_me.ToLower() == "yes")
                //{
                var link = await CreateIframeSrc(_configuration["Paymob:frameId"], paymentKeyResponse.PaymentKey);
                //}
                //else if (request.remember_me.ToLower() == "no")
                //{
                //    frameresponse.frame = await _adapter.CreateIframeSrc("402748", paymentKeyResponse.PaymentKey);
                //}
                return link;

            }
            catch (Exception ex)
            {
                var error = new UniscoException(ex.Message);
                Console.WriteLine(error.GetErrorModel());
                throw error;

            }
        }


        

    public async Task<KeyResponseBody> AuthToken()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(_configuration["Urls:authToken"]);
            request.Method = HttpMethod.Post;
            KeyRequestBody keyRequestBody = new KeyRequestBody();
            keyRequestBody.api_key = _configuration["Paymob:api_key"];

            request.Content = new StringContent(JsonConvert.SerializeObject(keyRequestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage keyResponse = await httpClient.SendAsync(request);
            if (keyResponse.IsSuccessStatusCode)
            {
                var keyresponseString = await keyResponse.Content.ReadAsStringAsync();
                KeyResponseBody keyResponseBody = JsonConvert.DeserializeObject<KeyResponseBody>(keyresponseString);
                if (!string.IsNullOrEmpty(keyResponseBody.token))
                {
                    authToken = keyResponseBody.token;
                    return keyResponseBody;
                }

            }
            return new KeyResponseBody() { token = string.Empty };
        }

        public async Task<CaptureResponse> IsCaptured(int transactionId, int amount)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage capture = new HttpRequestMessage();
            KeyResponseBody token = await AuthToken();
            capture.RequestUri = new Uri(_configuration["Urls:Capture"]);
            capture.Method = HttpMethod.Post;
            CaptureRequest captureRequest = new CaptureRequest();
            captureRequest.auth_token = token.token;
            captureRequest.transaction_id = transactionId;
            captureRequest.amount_cents = amount;

            capture.Content = new StringContent(JsonConvert.SerializeObject(captureRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage capturedResponse = await httpClient.SendAsync(capture);

            if (capturedResponse.IsSuccessStatusCode)
            {
                throw new UniscoException("payment not captured");
                var capturedResponseString = await capturedResponse.Content.ReadAsStringAsync();
                CaptureResponse captureResponseBody = JsonConvert.DeserializeObject<CaptureResponse>(capturedResponseString);
                if (captureResponseBody.Id != 0)
                {
                    return captureResponseBody;

                }

            }
            return new CaptureResponse();
        }
        private static readonly JsonSerializerOptions _IgnoreNullOptions = new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest orderRequest)
        {

            HttpClient httpClient = new HttpClient();
            KeyResponseBody token = await AuthToken();
            var requestUrl = new Uri(_configuration["Urls:CreateOrder"]);
            orderRequest.auth_token = authToken;
            using var response = await httpClient.PostAsJsonAsync(requestUrl, orderRequest, _IgnoreNullOptions);

            if (response.IsSuccessStatusCode)
            {
                var orderResponseString = await response.Content.ReadAsStringAsync();
                CreateOrderResponse orderResponseBody = JsonConvert.DeserializeObject<CreateOrderResponse>(orderResponseString);
                if (orderResponseBody.Id != 0)
                {
                    return orderResponseBody;

                }

            }
            return new CreateOrderResponse();

        }

        public async Task<PaymentKeyResponse> RequestPaymentKeyAsync(PaymentKeyRequest paymentKeyRequest)
        {
            HttpClient httpClient = new HttpClient();
            //KeyResponseBody token = await AuthToken();

            paymentKeyRequest.auth_token = authToken;
            var requestUrl = new Uri(_configuration["Urls:PaymentKey"]);


            using var response = await httpClient.PostAsJsonAsync(requestUrl, paymentKeyRequest);


            if (response.IsSuccessStatusCode)
            {
                var paymentKeyResponseString = await response.Content.ReadAsStringAsync();
                PaymentKeyResponse paymentKeyResponseBody = JsonConvert.DeserializeObject<PaymentKeyResponse>(paymentKeyResponseString);
                if (paymentKeyResponseBody.PaymentKey != null)
                {
                    return paymentKeyResponseBody;

                }

            }
            return new PaymentKeyResponse();

        }

        public async Task<string> CreateIframeSrc(string iframeId, string token)
        {
            return _configuration["Urls:Frame"] + iframeId + "?payment_token=" + token;
        }

        public async Task<SavedTokenPayResponse> CreateSavedTokenPayAsync(string paymentKey, string payToken)
        {
            try
            {


                HttpClient httpClient = new HttpClient();
                // KeyResponseBody token = await AuthToken();
                var source = new
                {
                    identifier = payToken,
                    subtype = "TOKEN"
                };
                var request = new
                {
                    source = source,
                    payment_token = paymentKey
                };

                var requestUrl = new Uri(_configuration["Urls:PayWithSavedToken"]);

                using var response = await httpClient.PostAsJsonAsync(requestUrl, request);

                if (response.IsSuccessStatusCode)
                {
                    var paymentResponseString = await response.Content.ReadAsStringAsync();
                    SavedTokenPayResponse paymentResponseBody = JsonConvert.DeserializeObject<SavedTokenPayResponse>(paymentResponseString);

                    return paymentResponseBody;


                }

                return new SavedTokenPayResponse();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PaymobPaymentResponse manageResponse(string responseId, string cardNumber, DateTime paymentDate, string trn, bool isRememberd, long payMethod, long status, string name, string email, string uuid, long amount)
        {
            PaymobPaymentResponse PaymentResponse = new PaymobPaymentResponse();
            PaymentResponse.responeId = responseId;
            PaymentResponse.cardNumber = cardNumber;
            PaymentResponse.Date = paymentDate;
            PaymentResponse.trn = trn;
            PaymentResponse.rememberMe = isRememberd;
            PaymentResponse.payMethod = payMethod;
            PaymentResponse.status = status;
            //PaymentResponse.countryPlanId = countryPlanId;
            //PaymentResponse.countryId = countryId;
            PaymentResponse.email = email;
            PaymentResponse.name = name;
            PaymentResponse.isRenewal = false;
            PaymentResponse.amount = amount;
            PaymentResponse.uuid = uuid;


            return PaymentResponse;
        }

        public PaymobPaymentResponse manageRecurringResponse(string responseId, DateTime paymentDate, string trn, long payMethod, long status, int countryId, int countryPlanId, string name, string email, string token, long amount, string uuid)
        {
            PaymobPaymentResponse PaymentResponse = new PaymobPaymentResponse();
            PaymentResponse.responeId = responseId;
            PaymentResponse.cardNumber = "";
            PaymentResponse.Date = paymentDate;
            PaymentResponse.trn = trn;
            PaymentResponse.payMethod = payMethod;
            PaymentResponse.status = status;
            PaymentResponse.countryId = countryId;
            PaymentResponse.countryPlanId = countryPlanId;
            PaymentResponse.name = name;
            PaymentResponse.email = email;
            PaymentResponse.isRenewal = true;
            PaymentResponse.token = token;
            PaymentResponse.amount = amount;
            PaymentResponse.uuid = uuid;



            return PaymentResponse;
        }

        public string TakeNDigits(int number, int N)
        {
            // this is for handling negative numbers, we are only insterested in postitve number
            string result = "";
            number = Math.Abs(number);
            // special case for 0 as Log of 0 would be infinity
            if (number == 0)
                return number.ToString();
            // getting number of digits on this input number
            int numberOfDigits = (int)Math.Floor(Math.Log10(number) + 1);
            // check if input number has more digits than the required get first N digits
            if (numberOfDigits > N)
            {
                result = number.ToString().Substring(0, 5);
                return result.ToString();
            }

            else
            {
                while (numberOfDigits < N)
                {
                    result = number.ToString().PadLeft(2, '0');
                    numberOfDigits++;
                }

            }
            return result;
        }

    }
}
