using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MonitorSystem.Data.ReportDatasource;
using MonitorSystem.Entity;

namespace MonitorSystem.Service
{
    public class PrintService<T>: IPrintService<T>
    {
        private readonly NavigationManager Manager;
        public PrintService(NavigationManager manager)
        {
            Manager = manager;
        }

        public IList<T> objects { get; set; }

        public void Print(IList<T> t)
        {
            objects =  t;
            MovementDatasource.Movements = (List < Movement > )t;
            Manager.NavigateTo("print");
        }
    }
}
