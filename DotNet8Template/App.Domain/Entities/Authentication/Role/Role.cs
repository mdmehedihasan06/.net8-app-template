﻿using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Authentication.Role
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
    }
}
