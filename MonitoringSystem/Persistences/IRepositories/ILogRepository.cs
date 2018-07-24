using MonitoringSystem.Models;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface ILogRepository
    {
        Task<Log> GetLog(int? id, bool includeRelated = true);
        void AddLog(Log log);
        void RemoveLog(Log log);
        Task<QueryResult<Log>> GetLogs(Query queryObj);
    }
}
