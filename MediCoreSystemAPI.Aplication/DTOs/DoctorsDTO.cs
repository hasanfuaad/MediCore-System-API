using Application.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MediCoreSystem.Aplication.DTOs
{
    public class DoctorsDTO : BaseDTO
    {
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "التخصص مطلوب")]
        [MaxLength(200)]
        public string Specialty { get; set; } = null!;

        [Required(ErrorMessage = "القسم مطلوب")]
        public int DepartmentId { get; set; }
    }
}
