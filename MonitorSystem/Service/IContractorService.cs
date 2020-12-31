using MonitorSystem.Entity;
using System;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public interface IContractorService : IDisposable
    {
        Task<Contractor> GetByName(string name);
        bool ValidName(string name);
    }
}
