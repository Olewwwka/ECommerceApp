using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Entities
{
    public class OrderItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
