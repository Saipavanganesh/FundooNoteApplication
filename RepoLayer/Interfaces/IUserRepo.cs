using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using static RepoLayer.Services.UserRepo;

namespace RepoLayer.Interfaces
{
    public interface IUserRepo
    {
        public Users UserRegistration(UserRegisterModel userRegisterModel);
        public string UserLogin(UserLoginModel model);
        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel);
        public bool ResetPassword(string Email, string NewPassword, string ConfirmPassword);
    }
}
