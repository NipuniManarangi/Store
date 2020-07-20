using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Common
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// ConnectionString for DB
        /// </summary>
        public string ConnectionString { get; set; }

    }
}
