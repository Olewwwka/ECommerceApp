using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class ShoppingCartProductEntity
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCartEntity ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
