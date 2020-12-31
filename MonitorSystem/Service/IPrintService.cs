using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public interface IPrintService<T>
    {
        public IList<T> objects { get; set; }

        void Print(IList<T> t);
    }
}
