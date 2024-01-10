using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Entities.Authentication.User;

namespace App.Domain.Entities.Authentication
{
    public class UserToken
    {
        [Key]
        public int UserTokenId { get; set; }
        [Required]
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string? IpAddress { get; set; }
        public bool IsInvalidated { get; set; }
        [NotMapped]
        public bool IsActive => ExpiredAt > DateTime.Now;

        public int UserId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? AppUser { get; set; }
    }
}
