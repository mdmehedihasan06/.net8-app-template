using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication
{
    public class ForgetPasswordToken
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }        
        public int? AttemptCount { get; set; }
        public bool IsValid { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }        
        public DateTime ExpiredAt { get; set; }        
        public DateTime? UsedAt { get; set; }
    }
}
