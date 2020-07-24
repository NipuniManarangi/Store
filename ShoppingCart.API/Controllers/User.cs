using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ShoppingCart.Business.ManagerClasses;
using ShoppingCart.Common;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        #region private properties
        UserManager userManager = new UserManager();
        UserDTO user = new UserDTO();
        LoginDTO loginuser = new LoginDTO();
        #endregion
        /// <summary>
        /// Method to register new user
        /// </summary>
        [HttpPost("Register")]
        public OperationResult Register(UserDTO userDTO)
        {
            Log.Information("Register method called at {logtime}", DateTime.Now);
            OperationResult operationResult = new OperationResult();
            try
            {


                if (userDTO != null)
                {
                    user.FirstName = userDTO.FirstName;
                    user.LastName = userDTO.LastName;
                    user.Email = userDTO.Email;
                    user.ContatctNo = userDTO.ContatctNo;
                    user.Address_Line1 = userDTO.Address_Line1;
                    user.Address_Line2 = userDTO.Address_Line2;
                    user.State = userDTO.State;
                    user.PostalCode = userDTO.PostalCode;
                    user.Password = userDTO.Password;
                }
                    operationResult = userManager.Register(user);
                Log.Information("Register method executed at {logtime}", DateTime.Now);
                return operationResult;
            }
#pragma warning disable CA1031 
            catch (Exception ex)

            {
                Log.Error("Error Occured :{ex}", ex);
                operationResult.Message = ex.Message;
                return operationResult;
              
            }
            finally
            {
                Log.CloseAndFlush();
            }

           
        }
        [HttpPost("Login")]
        public OperationResult Login(LoginDTO loginDTO)
        {
            Log.Information("Login method called at {logtime}",DateTime.Now);
            OperationResult operationResult = new OperationResult();
            try
            {
                if (loginDTO != null)
                {
                    loginuser.Email = loginDTO.Email;
                    loginuser.Password = loginDTO.Password;
                }
                operationResult = userManager.Login(loginuser);
                Log.Information("Login method executed at {logtime}", DateTime.Now);
                return operationResult;
            }
            catch(Exception ex)
            {
                Log.Error("Error Occured :{ex}", ex);
                operationResult.Message = ex.Message;
                return operationResult;
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }
        [HttpGet("GetUserDetails")]
        [Authorize]
        public OperationResult GetUserDetails()
        {
            string Email = User.Claims.First(c => c.Type == "Email").Value;
            OperationResult user = userManager.GetUserDetails(Email);
            return user;
        }


    }
}
