using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Entities
{
    public class ShoppingCartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<ShoppingCartProductEntity> Products { get; set; } = [];
        public ShoppingCartEntity(int userId)
        {
            UserId = userId;
        }
    }
}
