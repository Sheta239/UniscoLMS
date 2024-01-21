using Microsoft.AspNetCore.Mvc;
using UniscoLMS.DataBaseModels;
using ViewModels;
using Microsoft.EntityFrameworkCore;
using UniscoLMS.Controllers.Handler;
using System.IdentityModel.Tokens.Jwt;
using UniscoLMS.ViewModels;
using UniscoLMS.Errors;

namespace UniscoLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UniscoDbContext _UniscoDbContext;
        private readonly IConfiguration _configuration;
        private JWTHandler jwtHandler = null;

        public AuthController(UniscoDbContext UniscoDbContext, IConfiguration configuration)
        {
            _UniscoDbContext = UniscoDbContext;
            _configuration = configuration;
            jwtHandler = new JWTHandler(configuration);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            // make sure email not exsit ...
            var isEmailExsit = await _UniscoDbContext.Users.Where(x => x.Email.ToLower() == signUpRequest.Email.ToLower()).FirstOrDefaultAsync();
            if (isEmailExsit != null)
                throw new ErrorUserEmailAlreadyExist();

            // make sure userName not exsit ...
            string userName = signUpRequest.Email.Split("@")[0];
            var isUsrNameExsit = await _UniscoDbContext.Users.Where(x => x.Username.ToLower() == userName).FirstOrDefaultAsync();
            if (isEmailExsit != null)
            {
                Random rnd = new Random();
                userName += rnd.Next();
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(signUpRequest.Password);
            var userId = Guid.NewGuid();
            var user = await _UniscoDbContext.Users.AddAsync(new User
            {
                UserId = userId,
                Username = userName,
                FirstName = signUpRequest.FirstName,
                LastName = signUpRequest.LastName,
                Email = signUpRequest.Email,
                RoleId = signUpRequest.Role.ToLower() == "learner" ? (int)Enums.Roles.learner : (int)Enums.Roles.expert,
                Password = passwordHash
            });
            await _UniscoDbContext.SaveChangesAsync();

            // send verification code to validate email..
            await SendVerificationnCode(userId, signUpRequest.Email);

            return Ok(new SuccessBooleanModel(true));
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInRequest logInRequest)
        {
            var user = await _UniscoDbContext.Users.Include(x => x.Role)
                .Where(x => x.Email.ToLower() == logInRequest.UserEmail.ToLower()).FirstOrDefaultAsync();
            if (user == null)
                throw new ErrorUserEmailNotExist();

            bool verifiedPassword = BCrypt.Net.BCrypt.Verify(logInRequest.Password, user.Password);
            if (!verifiedPassword)
                throw new ErrorIncorrectPassword();

            if (user.EmailValidation == null || (bool)!user.EmailValidation)
            {
                // send verification code to validate email..
                await SendVerificationnCode(user.UserId, user.Email);

                return Ok(new SuccessModel(new LogInResponse
                {
                    IsEmailVerified = false
                }));
            }
            var token = await jwtHandler.GenerateToken(user.Username, user.Role.RoleName);
            if (token != null)
                return Ok(new SuccessModel(new LogInResponse
                {
                    IsEmailVerified = true,
                    UserName = user.Username,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo
                }));

            else
                throw new ErrorGenerateUserToken();
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LogInRequest logInRequest)
        {
            if (logInRequest.UserEmail != _configuration["Admin:User"])
                throw new ErrorUserEmailNotExist();

            bool verifiedPassword = BCrypt.Net.BCrypt.Verify(logInRequest.Password, _configuration["Admin:Password"]);
            if (!verifiedPassword)
                throw new ErrorIncorrectPassword();

            var token = await jwtHandler.GenerateToken(logInRequest.UserEmail, "Admin");
            if (token != null)
                return Ok(new SuccessModel(new LogInResponse
                {
                    IsEmailVerified = true,
                    UserName = "Admin",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo
                }));

            else
                throw new ErrorGenerateUserToken();
        }

        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode(SendVerivifactionCodeRequest sendVerivifactionCodeRequest)
        {
            var requestedUser = await _UniscoDbContext.Users.Include(x => x.Role)
                .Where(x => x.Email.ToLower() == sendVerivifactionCodeRequest.userEmail.ToLower()).FirstOrDefaultAsync();
            if (requestedUser == null)
                throw new ErrorUserEmailNotExist();

            // send verification code to validate email..
            await SendVerificationnCode(requestedUser.UserId, requestedUser.Email);

            return Ok(new SuccessBooleanModel(true));
        }

        [HttpPost("ValidateVerificationCodeAfterLogin")]
        public async Task<IActionResult> ValidateVerificationCodeAfterLogin(ValidateVerificationCodeRequest validateVerificationCodeRequest)
        {
            var requestedUser = await _UniscoDbContext.Users.Where(x => x.Email == validateVerificationCodeRequest.userEmail)
                .Include(x => x.Role).FirstOrDefaultAsync();
            if (requestedUser == null)
                throw new ErrorUserEmailNotExist();

            // check on verificaion code...
            var validationCode = await _UniscoDbContext.ValidationCodes.Where(x => x.UserId == requestedUser.UserId && x.ExpirationDate >= DateTime.Now)
                .OrderByDescending(x => x.GeneratedDate).FirstOrDefaultAsync();
            if (validationCode == null || validationCode.Code != validateVerificationCodeRequest.verificationCode)
                return Ok(new SuccessBooleanModel(false));

            if (validationCode.Code == validateVerificationCodeRequest.verificationCode)
            {
                requestedUser.EmailValidation = true;
                await _UniscoDbContext.SaveChangesAsync();
                var token = await jwtHandler.GenerateToken(requestedUser.Username, requestedUser.Role.RoleName);
                if (token != null)
                    return Ok(new SuccessModel(new SignUpResponse
                    {
                        UserName = requestedUser.Username,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ValidTo = token.ValidTo
                    }));

                else
                    throw new ErrorGenerateUserToken();
            }
            else
                return Ok(new SuccessBooleanModel(false));
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            var requestedUser = await _UniscoDbContext.Users.Where(x => x.Email == forgetPasswordRequest.userEmail)
                .Include(x => x.Role).FirstOrDefaultAsync();
            if (requestedUser == null)
                throw new ErrorUserEmailNotExist();

            // check on verificaion code...
            var validationCode = await _UniscoDbContext.ValidationCodes.Where(x => x.UserId == requestedUser.UserId && x.ExpirationDate >= DateTime.Now)
                .OrderByDescending(x => x.GeneratedDate).FirstOrDefaultAsync();
            if (validationCode == null || validationCode.Code != forgetPasswordRequest.verificationCode)
                return Ok(new SuccessBooleanModel(false));

            if (validationCode.Code == forgetPasswordRequest.verificationCode)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(forgetPasswordRequest.newPassword);
                requestedUser.Password = passwordHash;
                await _UniscoDbContext.SaveChangesAsync();
                return Ok(new SuccessBooleanModel(true));
            }
            else
                return Ok(new SuccessBooleanModel(false));
        }

        private async Task SendVerificationnCode(Guid userId, string userEmail)
        {
            // generate random 6 digits...
            Random generator = new Random();
            string validationCode = generator.Next(0, 1000000).ToString("D6");

            // insert validation code...
            await _UniscoDbContext.ValidationCodes.AddAsync(new ValidationCode
            {
                UserId = userId,
                Code = validationCode,
                GeneratedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(5)
            });
            await _UniscoDbContext.SaveChangesAsync();

            // send validation code to user...
        }

    }
}
