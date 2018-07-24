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
        void AddTemperature(Status status, double? temperatureValue);
        void AddHumidity(Status status, double? humidityValue);
        void AddStatusLog(Status status);
        void UpdateStatusLog(Status oldStatus, Status status);
    }
}
