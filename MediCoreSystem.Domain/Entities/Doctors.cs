using Domain.Common;

namespace MediCoreSystem.Domain.Entities
{
    public class Doctors : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Specialty { get; set; } = null!;

        public int DepartmentId { get; set; }
        public Departments Department { get; set; } = null!;

        public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
    }
}
