using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication.User
{
    public class UserType : BaseEntity
    {
        public string UserTypeName { get; set; }
        public string? TypeDescription { get; set; }
    }
}
