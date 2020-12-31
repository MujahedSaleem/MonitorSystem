using MonitorSystem.Entity;
using System;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public interface IProjectService : IDisposable
    {
        Task<Project> GetByName(string name);
        bool ValidName(string name);
    }
}
