using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Manager;
using ShoppingCart.Common;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product : ControllerBase
    {
        #region private properties
        ProductManager productManager = new ProductManager();
        #endregion

        /// <summary>
        /// Method to get products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetProducts/{id:int?}")]
        public OperationResult  Get(int? id)
        {
            OperationResult operationResult = productManager.GetAllProducts(id);
            return operationResult; 
        }
    }
}
