using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Domain.Entites
{
    public class Users : BaseEntity
    {
        public int AccountId { get; set; }
        public Account? Account { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Roles? Role { get; set; }
        public bool IsActive { get; set; }

       

    }
}
