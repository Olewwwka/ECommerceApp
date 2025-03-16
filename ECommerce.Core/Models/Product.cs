using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public Product()
        {
            
        }
        public Product(int categoryId, string name, string description, decimal price, int stockQuantity)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Price = price;
            Quantity = stockQuantity;
        }
    }
}
