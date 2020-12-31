using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorSystem.Entity
{
    public class Movement : EntityBase
    {
        public int CertificateNumber { get; set; }
        public DateTime MovementDate { get; set; }=DateTime.Now;
        public string Reference { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string CompanyName { get; set; }
        public string Place { get; set; }
        public string MaterialTypes { get; set; }
        public int Total { get; set; }
        public int AddedValue { get; set; }
        public int Net { get; set; }
        public virtual Project Project { get; set; }

        public  Guid ProjectId { get; set; }
        public virtual Contractor Contractor { get; set; }

        public  Guid ContractorId { get; set; }
        public int Number { get; set; }

    }
}
