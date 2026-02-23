using Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.DTOs
{
    public class PermissionsDTO : BaseDTO
    {
        public string? Name_Ar { get; set; }
        public string? Name_En { get; set; }
    }
}
