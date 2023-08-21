using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public Users UserRegistration(UserRegisterModel userRegisterModel);
        public string UserLogin(UserLoginModel model);
    }
}
