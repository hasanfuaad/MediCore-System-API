using Application.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.DTOs
{

    public class AccountDTO : BaseDTO
    {
        public string ProfilePic { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        [MaxLength(100, ErrorMessage = "الاسم الأول يجب أن لا يزيد عن 100 حرف")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الثاني مطلوب")]
        [MaxLength(100, ErrorMessage = "الاسم الثاني يجب أن لا يزيد عن 100 حرف")]
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الثالث مطلوب")]
        [MaxLength(100, ErrorMessage = "الاسم الثالث يجب أن لا يزيد عن 100 حرف")]
        public string ThirdName { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم العائلة مطلوب")]
        [MaxLength(100, ErrorMessage = "اسم العائلة يجب أن لا يزيد عن 100 حرف")]
        public string LastName { get; set; } = string.Empty;

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
        public string Desc { get; set; } = string.Empty;



    }

}
