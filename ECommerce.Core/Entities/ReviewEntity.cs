using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Entities
{
    public class ReviewEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Rating { get; set; } = 0;
        public string Comment { get; set; } = string.Empty;
    }
}
