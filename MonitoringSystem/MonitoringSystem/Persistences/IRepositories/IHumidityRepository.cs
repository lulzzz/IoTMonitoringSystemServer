using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IHumidityRepository
    {
        Task<Humidity> GetHumidity(int? id, bool includeRelated = true);
        void AddHumidity(Humidity humidity);
        void RemoveHumidity(Humidity humidity);
        Task<IEnumerable<Humidity>> GetHumidities();
    }
}
