using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication.Menu
{
    public class UserMenuPermission : BaseEntity
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
    }
}
