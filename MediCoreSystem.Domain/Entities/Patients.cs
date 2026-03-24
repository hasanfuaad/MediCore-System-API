using Domain.Common;

namespace MediCoreSystem.Domain.Entities
{
    public class Patients : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }

        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
    }
}
