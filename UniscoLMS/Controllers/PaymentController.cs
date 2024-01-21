using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UniscoLMS.PaymobModels;
using UniscoLMS.ViewModels.Requests;
using UniscoLMS.ViewModels.Responses;
using UniscoLMS.Services;
using System.Text.Json;
using System.Text;
using ViewModels;
using UniscoLMS.Enums;
using static UniscoLMS.ViewModels.Requests.PaymentRequest;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace UniscoLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymobService _adapter;
        private readonly adapterService _dbServicee;
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration, PaymobService adapter  , adapterService dbServicee)
        {
            _configuration = configuration;
            _adapter = adapter;
            _dbServicee = dbServicee;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Pay")]
        public async Task<IActionResult> Pay(PayRequest request)
        {
            var userName = GetUserLogin();
            var user = await _dbServicee.GetUser(userName);
            var session = await _dbServicee.GetCourse(request.CourseId);

            string Trn = DateTime.UtcNow.Millisecond + _adapter.TakeNDigits(1, 2) + _adapter.TakeNDigits(Convert.ToInt32(DateTime.UtcNow.Millisecond), 4);

            var Price = session.Price;
            var Amountdeptfromcustomer = Price * 100;

            var isinitialized = await _dbServicee.InitializeOrder(Trn, 1, user.UserId, request.CourseId);

            PaymentRequest paymentRequest = new PaymentRequest();
            if (isinitialized == true)
            {
                paymentRequest.amount = (int)Amountdeptfromcustomer;
                paymentRequest.trn = Trn;
                paymentRequest.user = new User()
                {
                    firstName = user.FirstName,
                    email = user.Email,
                    lastName = user.LastName,
                };


            }

            FrameRequest response = new FrameRequest();

            paymentRequest.amount = paymentRequest.amount / 100;
            var frameResponse = await _adapter.PaymobPay(paymentRequest);
            response.frame = frameResponse;
            return Ok(new SuccessModel(response));

        }

        [HttpPost("CallBack")]
        public async Task<IActionResult> CallBack([FromBody] Callback request, [FromQuery] string Hmac)
        {

            if (request.Type == CallbackTypes.Token)
            {
                var obj = (JsonElement)request.Obj;
                var objText = obj.GetRawText();
                var token = System.Text.Json.JsonSerializer.Deserialize<CallbackToken>(objText)!;

                TokenResponse tokenResponse = new TokenResponse();
                tokenResponse.Token = token.Token;
                tokenResponse.orderId = token.OrderId;
                tokenResponse.CardNumber = token.MaskedPan;
                var response = new RegularResponse();
                return Ok(new SuccessModel(tokenResponse));

            }
            else if (request.Type == CallbackTypes.Transaction)
            {
                var obj = (JsonElement)request.Obj;
                var objText = obj.GetRawText();
                var transaction = System.Text.Json.JsonSerializer.Deserialize<CallbackTransaction>(objText)!;


                PaymobPaymentResponse paymentResponse = new PaymobPaymentResponse();

                if (transaction.Success == true)
                {

                    paymentResponse = _adapter.manageResponse(transaction.Id.ToString(),
                          transaction.Data.CardNum, transaction.Data.CreatedAt.Date,
                         transaction.Order.MerchantOrderId.ToString(),
                          false,
                         (long)PaymentMethods.PAYMOB,
                         (long)Statuses.SUCCESS, transaction.Order.ShippingData.FirstName + transaction.Order.ShippingData.LastName, transaction.Order.ShippingData.Email, null, (long)transaction.AmountCents);
                }
                else if (transaction.Success == false)
                {
                    paymentResponse = _adapter.manageResponse(transaction.Id.ToString(),
                     transaction.Data.CardNum, transaction.Data.CreatedAt.Date,
                    transaction.Order.MerchantOrderId.ToString(),
                     false,
                    (long)PaymentMethods.PAYMOB,
                    (long)Statuses.DECLINED, transaction.Order.ShippingData.FirstName + transaction.Order.ShippingData.LastName, transaction.Order.ShippingData.Email, null, (long)transaction.AmountCents);

                }

                var course = await _dbServicee.OTPResponse(paymentResponse);
                return Ok(new SuccessModel(true));

            }


            return Ok(new SuccessModel(true));
        }

        [HttpGet("CallBack")]
        public async Task<IActionResult> CallBack()
        {
            var request = Request.QueryString.Value;
            var trn = request.Split('&').FirstOrDefault(x => x.StartsWith("merchant_order_id"))?.Split('=')[1];
            GetItemTypeRequest TypeRequest = new GetItemTypeRequest
            {
                Trn = trn
            };
            var typeResponse = new GetItemTypeResponse();

            return Redirect(_configuration["Urls:redirectToPaymentStatus"] + "?orderId=" + trn);

        }
        private string GetUserLogin()
        {
            var token = HttpContext.User.Identity;
            var userName = token?.Name;
            if (string.IsNullOrEmpty(userName))
                return null;
            else
                return userName;
        }
    }
}
