using MonitoringSystem.Models;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IStatusRepository
    {
        Task<Status> GetStatus(int? id, bool includeRelated = true);
        void AddStatus(Status status);
        void RemoveStatus(Status status);
        Task<QueryResult<Status>> GetStatuses(Query queryObj);
    }
}
