using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorSystem.Entity
{
    public class Project : EntityBase
    {
        public string ProjectName { get; set; }
        public string Reference { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Guid ContractorId { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
    }
}
