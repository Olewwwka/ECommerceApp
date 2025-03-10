using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerce.Core.Entities;

namespace ECommerce.Core.Entities
{
    public class AuditLogsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }
        public string Action { get; set; } = string.Empty;
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime LogTime { get; set; }
    }
}

