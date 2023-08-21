using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext _fundooContext;
        public UserRepo(FundooContext fundooContext)
        {
            this._fundooContext = fundooContext;
        }
        public Users UserRegistration(UserRegisterModel userRegisterModel)
        {
            Users users = new Users();
            users.FirstName = userRegisterModel.FirstName;
            users.LastName = userRegisterModel.LastName;
            users.DateOfBirth = userRegisterModel.DateOfBirth;
            users.Email = userRegisterModel.Email;
            users.Password = userRegisterModel.Password;
            _fundooContext.User.Add(users);
            _fundooContext.SaveChanges();
            return users;
        }
    }
}
