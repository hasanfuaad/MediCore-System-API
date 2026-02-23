using Application.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.DTOs
{
    using MediCoreSystem.Domain.Entites;
    using System.ComponentModel.DataAnnotations;

    public class UserDTO : BaseDTO
    {

   
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [MaxLength(50, ErrorMessage = "الاسم الكامل يجب ألا يزيد عن 50 حرف")]
        [RegularExpression(@"^[a-zA-Z\u0600-\u06FF ]+$", ErrorMessage = "الاسم يجب أن يحتوي على حروف فقط")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [MinLength(3, ErrorMessage = "اسم المستخدم يجب ألا يقل عن 3 أحرف")]
        [MaxLength(20, ErrorMessage = "اسم المستخدم يجب ألا يزيد عن 20 حرف")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة المرور يجب ألا تقل عن 6 أحرف")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "كلمة المرور يجب أن تحتوي على حرف كبير وصغير ورقم")]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;

    }

}
