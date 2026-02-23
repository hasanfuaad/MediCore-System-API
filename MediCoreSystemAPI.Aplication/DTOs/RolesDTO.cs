using Application.Models.Base;
using MediCoreSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.DTOs
{
    public class RolesDTO : BaseDTO
    {

     
        public string Name_Ar { get; set; }

        public string? Name_En { get; set; }

    }
}
