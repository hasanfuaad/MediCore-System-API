using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Domain.Entites
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Roles? Role { get; set; }
        public int PermissionId { get; set; }
        public Permissions? Permission { get; set; }
    }

}
