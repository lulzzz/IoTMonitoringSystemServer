using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IRoomRepository
    {
        Task<Room> GetRoom(int? id, bool includeRelated = true);
        void AddRoom(Room room);
        void RemoveRoom(Room room);
        Task<QueryResult<Room>> GetRooms(Query queryObj);
    }
}
