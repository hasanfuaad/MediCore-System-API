using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public int AccountId { get; set; }
        public int? UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsAccountOwner { get; set; }
    }

}
