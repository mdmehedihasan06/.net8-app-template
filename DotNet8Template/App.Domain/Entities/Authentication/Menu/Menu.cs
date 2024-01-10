using App.Domain.Common;
using App.Domain.Entities.Authentication.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication.Menu
{
    public class Menu : BaseEntity
    {
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string? Description { get; set; }
        public string? MenuIcon {  get; set; }
        public int SortOrder { get; set; } = 0;
        public bool IsModule { get; set; }
        public bool IsParent { get; set; }
        public int? ParentMenuId { get; set; }
        public bool ShowMenu { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public int? MenuLevel { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(ParentMenuId))]
        public virtual Menu? ParentMenu { get; set; }

        // List Objects
        public virtual List<RoleMenuPermission>? RoleMenuPermissions { get; set; }
    }
}
