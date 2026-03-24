using Domain.Common;

namespace MediCoreSystem.Domain.Entities
{
    public class Appointments : BaseEntity
    {
        public int PatientId { get; set; }
        public Patients Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctors Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

    }
}
