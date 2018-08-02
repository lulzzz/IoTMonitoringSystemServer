using MonitoringSystem.Models;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IFanStatusRepository
    {
        Task<FanStatus> GetFanStatus(int? id, bool includeRelated = true);
        void AddFanStatus(FanStatus fanStatus);
        void RemoveFanStatus(FanStatus fanStatus);
        Task<QueryResult<FanStatus>> GetFanStatuses(Query queryObj);
        void AddFanStatusLog(FanStatus fanStatus);
    }
}
