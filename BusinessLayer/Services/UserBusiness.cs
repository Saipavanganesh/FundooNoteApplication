﻿using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo _UserRepo;
        public UserBusiness(IUserRepo UserRepo)
        {
            _UserRepo = UserRepo;
        }
        public Users UserRegistration(UserRegisterModel userRegisterModel)
        {
            try
            {
                return _UserRepo.UserRegistration(userRegisterModel);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                return _UserRepo.UserLogin(model);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                return _UserRepo.ForgotPassword(forgotPasswordModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string Email, string NewPassword, string ConfirmPassword)
        {
            try
            {
                return _UserRepo.ResetPassword(Email, NewPassword, ConfirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
