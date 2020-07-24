using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            Log.Information("GetProducts method called at {logtime}", DateTime.Now);
            OperationResult operationResult = new OperationResult();
            try 
            {
                operationResult = productManager.GetAllProducts(id);
                return operationResult;
            }
#pragma warning disable CA1031 
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
    }
}
