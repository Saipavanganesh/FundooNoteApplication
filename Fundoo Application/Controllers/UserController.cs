using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundoo_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Registration(UserRegisterModel userRegisterModel)
        {
            var result = _userBusiness.UserRegistration(userRegisterModel);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "User Registration Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "User Registration Unsuccessful", data = result });
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(UserLoginModel model)
        {
            var result = _userBusiness.UserLogin(model);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "User Login Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "User Login Unsuccessful", data = result });
            }
        }
        [HttpPost]
        [Route("forgotPassword")]

        public IActionResult ForgotPwd(ForgotPasswordModel forgotPasswordModel)
        {
            var result = _userBusiness.ForgotPassword(forgotPasswordModel);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Token Sent", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Something went wrong", data = result });
            }
        }
    }
}
