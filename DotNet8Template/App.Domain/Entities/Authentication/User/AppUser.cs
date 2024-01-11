using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Authentication.User
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            UserTokens = new List<UserToken>();
        }        
        public string? FullName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? UserTypeId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(UserTypeId))]
        public virtual UserType? UserTypes { get; set; }

        // List Objects
        public virtual List<UserToken>? UserTokens { get; set; }
    }
}
