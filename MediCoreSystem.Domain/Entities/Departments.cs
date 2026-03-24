using Domain.Common;

namespace MediCoreSystem.Domain.Entities
{
    public class Departments : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Doctors> Doctors { get; set; } = new List<Doctors>();
    }
}
