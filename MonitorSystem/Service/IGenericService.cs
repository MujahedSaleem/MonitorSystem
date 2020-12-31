using MonitorSystem.Entity;

namespace MonitorSystem.Service
{
    public interface IGenericService<T> : IBaseEntityService<T> where T : EntityBase
    {
    }
}
