using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Domain.Entites
{
    public class Account : BaseEntity
    {

        public string ProfilePic { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SecondName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ThirdName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        public string Desc { get; set; } = string.Empty;
        public ICollection<Users>? Users { get; set; }

    }

}
