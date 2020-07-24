using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ShoppingCart.Common;
using ShoppingCart.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Enum = ShoppingCart.Common.Enum;

namespace ShoppingCart.Business.ManagerClasses
{
    /// <summary>
    /// Business manager class with all the user related logic
   
    /// </summary>
    public class UserManager : BaseManager
    {
      
        User user = new User();
        //key used to encrypt the password
        public static string key = "asdfrtgbvcqwe@59#";
        public static string tokenKey = "1234567890123456";
        
        /// <summary>
        /// method to register user
        /// </summary>
        public OperationResult Register(UserDTO userDTO)
        {
            
            //new operarion result object to hold responce data
            OperationResult operationResult = new OperationResult();
            Log.Information("Need to check whether user exists in the system at {logtime}", DateTime.Now);
            operationResult = UserExists(userDTO.Email);

            if(operationResult.Status == Enum.Status.Success)
            {
                Log.Information("User exists at {logtime}", DateTime.Now);
                operationResult.Message = "user already exists!";
                operationResult.Status = Enum.Status.Error;
            }
            
            else
            {

                Log.Information("Started to register new user at {logtime}", DateTime.Now);
                operationResult = PasswordEncrypt(userDTO.Password);
                user.Password = operationResult.Data;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.ContatctNo = userDTO.ContatctNo;
                user.Address_Line1 = userDTO.Address_Line1;
                user.Address_Line2 = userDTO.Address_Line2;
                user.State = userDTO.State;
                user.PostalCode = userDTO.PostalCode;

                UserRepository.Insert(user);
                UserRepository.Save();
                operationResult.Message = Constant.SuccessMessage;
                Log.Information("New user registration successful at {logtime}", DateTime.Now);

            }



            return operationResult;


        }
        public OperationResult UserExists(string email)
        {
            OperationResult operationResult = new OperationResult();
            Log.Information("Checking user exists in the system at {logtime}", DateTime.Now);
            operationResult.Data = UserRepository.GetById(email);

            if (operationResult.Data == null)
            {
                operationResult.Status = Enum.Status.Error;
                return operationResult;
            }
            else
            {
                operationResult.Status = Enum.Status.Success;
                operationResult.Data = null;
                return operationResult;
            }
        }
        public OperationResult Login(LoginDTO loginDTO)
        {
            OperationResult operationResult = new OperationResult();
            
            user.Email = loginDTO.Email;
            operationResult = PasswordEncrypt(loginDTO.Password);
            user.Password = operationResult.Data;
            Log.Information("Matching user credentials at {logtime}", DateTime.Now);
            var userinfo = UserRepository.GetById(user.Email);
            if(userinfo is null)
            {
                Log.Information("User does not exists {logtime}", DateTime.Now);
                operationResult.Message = "User does not exists";
                operationResult.Status = Enum.Status.Error;
                operationResult.Data = null;

                Log.Information("Login method failed at {logtime}", DateTime.Now);
            }
            else if ((userinfo.Email==user.Email) && (userinfo.Password ==user.Password))
            {
                operationResult.Status = Enum.Status.Success;
                Log.Information("JWT token authentication started at {logtime}", DateTime.Now);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                       new Claim("Email", userinfo.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                operationResult.Message = token;
                operationResult.Data = null;

                Log.Information("Login method successful at {logtime}", DateTime.Now);

            }
            else
            {
                operationResult.Message = "Username or Password is incorrect";
                operationResult.Status = Enum.Status.Error;
                operationResult.Data = null;

                Log.Information("Login method failed at {logtime}", DateTime.Now);
            }
               
            return operationResult;

        }
        public OperationResult GetUserDetails(string email)
        {
            // new operarion result object to hold responce data
             OperationResult operationResult = new OperationResult();
            operationResult.Status = Enum.Status.Success;
            operationResult.Message = Constant.SuccessMessage;
            var signinUser = UserRepository.GetById(email);
            operationResult.Data = signinUser;
            return operationResult;

        }
        public OperationResult PasswordEncrypt(string password)
        {
            OperationResult operationResult = new OperationResult();
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            operationResult.Data = Convert.ToBase64String(passwordBytes);

            Log.Information("Password Encryption successful at {logtime}", DateTime.Now);

            return operationResult;
            


        }
    }
}
