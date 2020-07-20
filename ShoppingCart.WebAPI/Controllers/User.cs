using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.ManagerClasses;
using ShoppingCart.Common;

namespace ShoppingCart.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        #region private properties
        UserManager userManager = new UserManager();
        #endregion
        /// <summary>
        /// Method to register new user
        /// </summary>
        [HttpPost("Register")]
        public OperationResult Register(string firstname, string lastname, string email, string contatctno, string addressline1, string addressline2, string state, string postalcode, string password)
        {
            OperationResult operationResult = userManager.Insert(firstname, lastname, email, contatctno, addressline1, addressline2, state, postalcode, password);

            return operationResult;
        }
    }
}
