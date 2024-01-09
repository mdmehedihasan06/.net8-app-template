using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication.Role
{
    public class RoleMenuPermission : BaseEntity
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
