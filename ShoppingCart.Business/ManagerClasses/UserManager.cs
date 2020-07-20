using Microsoft.IdentityModel.Tokens;
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
        //private readonly ApplicationSettings _appSettings;
        /// <summary>
        /// method to register user
        /// </summary>
        public OperationResult Register(UserDTO userDTO)
        {
            
            //new operarion result object to hold responce data
            OperationResult operationResult = new OperationResult();
            operationResult = UserExists(userDTO.Email);
            if(operationResult.Status == Enum.Status.Success)
            {
                operationResult.Message = "user already exists!";
                operationResult.Status = Enum.Status.Error;
            }
            //operationResult.Status = Enum.Status.Success;
            else
            {
                operationResult.Message = Constant.SuccessMessage;
                //user.UserId = userDTO.UserId;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.ContatctNo = userDTO.ContatctNo;
                user.Address_Line1 = userDTO.Address_Line1;
                user.Address_Line2 = userDTO.Address_Line2;
                user.State = userDTO.State;
                user.PostalCode = userDTO.PostalCode;
                user.Password = userDTO.Password;
                UserRepository.Insert(user);
                UserRepository.Save();
                //check id
                //if (userDTO.UserId > 0)
                //{


                //}
            }



            return operationResult;


        }
        public OperationResult UserExists(string email)
        {
            OperationResult operationResult = new OperationResult();
            var registereduser = UserRepository.GetById(email);
            if (registereduser.Email != null)
            {
                operationResult.Status = Enum.Status.Success;
                return operationResult;

            }
            else
            {
                operationResult.Status = Enum.Status.Error;
                return operationResult;
            }
        }
        public OperationResult Login(LoginDTO loginDTO)
        {
            OperationResult operationResult = new OperationResult();
            

            user.Email = loginDTO.Email;
            user.Password = loginDTO.Password;

            var email = UserRepository.GetById(user.Email);
            //var pass = UserRepository.GetById(user.Password);
            if (email != null)
            {
                operationResult.Status = Enum.Status.Success;
               // operationResult.Message = Constant.SuccessMessage;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                       new Claim("Email", email.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                operationResult.Message = token;
                operationResult.Data = email;


            }
            else
            {
                operationResult.Message = "Username or Password is incorrect";
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
    }
}
