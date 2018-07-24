using MonitoringSystem.Models;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IFanRepository
    {
        Task<Fan> GetFan(int? id, bool includeRelated = true);
        void AddFan(Fan fan);
        void RemoveFan(Fan fan);
        Task<QueryResult<Fan>> GetFans(Query queryObj);
        void AddFanLog(Fan fan);
        void UpdateFanLog(Fan oldFan, Fan fan);
    }
}
