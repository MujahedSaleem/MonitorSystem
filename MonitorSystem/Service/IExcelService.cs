using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitorSystem.Entity;

namespace MonitorSystem.Service
{
    public interface IExcelService
    {
        void Export(List<Movement> m);
    }
}
