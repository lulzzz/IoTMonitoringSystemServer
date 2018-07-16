using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface ITemperatureRepository
    {
        Task<Temperature> GetTemperature(int? id, bool includeRelated = true);
        void AddTemperature(Temperature temperature);
        void RemoveTemperature(Temperature temperature);
        Task<QueryResult<Temperature>> GetTemperatures(Query queryObj);
    }
}
