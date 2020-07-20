using System;
using System.Collections.Generic;

namespace ShoppingCart.Data.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? QuantityInStock { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
