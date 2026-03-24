using Application.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MediCoreSystem.Aplication.DTOs
{
    public class PatientsDTO : BaseDTO
    {
        [Required(ErrorMessage = "اسم المريض مطلوب")]
        [MaxLength(200, ErrorMessage = "الاسم يجب ألا يزيد عن 200 حرف")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [MaxLength(20, ErrorMessage = "رقم الهاتف يجب ألا يزيد عن 20 رقم")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(300, ErrorMessage = "العنوان يجب ألا يزيد عن 300 حرف")]
        public string? Address { get; set; }
    }
}
