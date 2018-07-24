using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IRackRepository
    {
        Task<Rack> GetRack(int? id, bool includeRelated = true);
        void AddRack(Rack rack);
        void RemoveRack(Rack rack);
        Task<QueryResult<Rack>> GetRacks(Query queryObj);
        void AddRackLog(Rack rack);
        void UpdateRackLog(Rack oldRack, Rack rack);
    }
}
