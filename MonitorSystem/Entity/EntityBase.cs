using System;
using System.ComponentModel.DataAnnotations;

namespace MonitorSystem.Entity
{
    public class EntityBase
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
