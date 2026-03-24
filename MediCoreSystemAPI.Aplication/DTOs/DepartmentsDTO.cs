using Application.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MediCoreSystem.Aplication.DTOs
{
    public class DepartmentsDTO : BaseDTO
    {
        [Required(ErrorMessage = "اسم القسم مطلوب")]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
    }
}
