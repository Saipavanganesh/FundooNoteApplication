using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace RepoLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext _fundooContext;
        private readonly IConfiguration _configuration;
        public UserRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this._fundooContext = fundooContext;
            this._configuration = configuration;
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
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                var userEntity = _fundooContext.User.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (userEntity != null)
                {
                    var token = GenerateJwtToken(userEntity.Email, userEntity.UserId);
                    return token;                   
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateJwtToken(string Email, long UserId)
        {
            var claims = new List<Claim>
             {
                 new Claim("UserId", UserId.ToString()),
                 new Claim("Email", Email),
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(1), creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var userEntity = _fundooContext.User.FirstOrDefault(u => u.Email == forgotPasswordModel.Email);
            if (userEntity != null)
            {
                var token = GenerateJwtToken(userEntity.Email, userEntity.UserId);
                MSMQ msmq = new MSMQ();
                msmq.SendData2Queue(token);
                return token;
            }
            return null;
        }
    }
}
