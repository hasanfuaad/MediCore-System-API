using Application.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MediCoreSystem.Aplication.DTOs
{
    public class AppointmentsDTO : BaseDTO
    {
        [Required(ErrorMessage = "المريض مطلوب")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "الطبيب مطلوب")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "تاريخ الموعد مطلوب")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "حالة الموعد مطلوبة")]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Pending / Completed / Cancelled
    }
}
