using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorSystem.Entity
{
    public class Contractor : EntityBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        [NotMapped]
        public virtual ICollection<Movement> Movements { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public Contractor Clone()
        {
            return new Contractor()
            {
                Name = this.Name,
                Phone = this.Phone,
            };
        }
    }
}
