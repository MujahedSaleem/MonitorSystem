using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.DataAccess.ObjectBinding;
using Microsoft.Extensions.Hosting;
using MonitorSystem.Entity;
using MonitorSystem.Service;

namespace MonitorSystem.Data.ReportDatasource
{
    [DisplayName("MovementDataSource")]
    [HighlightedClass]
    public class MovementDatasource
    {
        public static List<Movement> Movements { get; set; }
        public MovementDatasource()
        {
        }

        [HighlightedMember]
        public static IEnumerable<Movement> GetEmployeeList()
        {
            return Movements;
        }
    }
}
