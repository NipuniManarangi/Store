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
using Microsoft.IdentityModel.Tokens;
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
            OperationResult operationResult = userManager.Register(user);

            return operationResult;
        }
        [HttpPost("Login")]
        public OperationResult Login(LoginDTO loginDTO)
        {
            if (loginDTO != null)
            {
                loginuser.Email = loginDTO.Email;
                loginuser.Password = loginDTO.Password;
            }
                OperationResult operation = userManager.Login(loginuser);
                return operation;
            
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
