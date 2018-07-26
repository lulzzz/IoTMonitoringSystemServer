using MonitoringSystem.Models;
using MonitoringSystem.Resources;
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
        Task UpdateSensors(Room room, RoomResource roomResource);
        Task UpdateRacks(Room room, RoomResource roomResource);
        Task UpdateFans(Room room, RoomResource roomResource);
        void AddRoomLog(Room room);
        void UpdateRoomLog(Room oldRoom, Room room);
        Task<Room> GetRoomByRoomName(string roomName);
    }
}
