using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Extensions;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private ApplicationDbContext context;

        public RoomRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddRoom(Room room)
        {
            context.Rooms.Add(room);
        }

        public async Task<Room> GetRoom(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Rooms.FindAsync(id);
            }
            return await context.Rooms
                .Include(r => r.Sensors)
                .Include(r => r.Racks)
                .Include(r => r.Fans)
                .SingleOrDefaultAsync(h => h.RoomId == id);
        }

        public async Task<QueryResult<Room>> GetRooms(Query queryObj)
        {
            var result = new QueryResult<Room>();
            var query = context.Rooms
                        .Include(r => r.Sensors)
                        .Include(r => r.Racks)
                        .Include(r => r.Fans)
                        .Where(h => h.IsDeleted == false)
                        .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Room, object>>>()
            {
                ["roomcode"] = s => s.RoomCode,
                ["roomname"] = s => s.RoomName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.RoomId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveRoom(Room room)
        {
            room.IsDeleted = true;
        }

        public async Task UpdateSensors(Room room, RoomResource roomResource)
        {
            if (roomResource.Sensors != null && roomResource.Sensors.Count >= 0)
            {
                //remove old sensors
                var oldSensors = room.Sensors.Where(s => !roomResource.Sensors.Any(id => id == s.SensorId)).ToList();
                foreach (Sensor sensor in oldSensors)
                {
                    room.Sensors.Remove(sensor);
                }

                //add new sensors
                var newSensors = roomResource.Sensors.Where(s => !room.Sensors.Any(id => id.SensorId == s)).ToList();
                foreach (var sensorId in newSensors)
                {
                    var newSensor = await context.Sensors.FirstOrDefaultAsync(s => s.SensorId == sensorId);
                    if (newSensor != null)
                    {
                        newSensor.Room = room;
                        room.Sensors.Add(newSensor);
                    }
                }
            }
        }

        public async Task UpdateRacks(Room room, RoomResource roomResource)
        {
            if (roomResource.Racks != null && roomResource.Racks.Count >= 0)
            {
                //remove old racks
                var oldRacks = room.Racks.Where(s => !roomResource.Racks.Any(id => id == s.RackId)).ToList();
                foreach (Rack rack in oldRacks)
                {
                    room.Racks.Remove(rack);
                }

                //add new racks
                var newRacks = roomResource.Racks.Where(s => !room.Racks.Any(id => id.RackId == s)).ToList();
                foreach (var rackId in newRacks)
                {
                    var newRack = await context.Racks.FirstOrDefaultAsync(s => s.RackId == rackId);
                    if (newRack != null)
                    {
                        newRack.Room = room;
                        room.Racks.Add(newRack);
                    }
                }
            }
        }

        public async Task UpdateFans(Room room, RoomResource roomResource)
        {
            if (roomResource.Fans != null && roomResource.Fans.Count >= 0)
            {
                //remove old fans
                var oldFans = room.Fans.Where(s => !roomResource.Fans.Any(id => id == s.FanId)).ToList();
                foreach (Fan fan in oldFans)
                {
                    room.Fans.Remove(fan);
                }

                //add new fans
                var newFans = roomResource.Fans.Where(s => !room.Fans.Any(id => id.FanId == s)).ToList();
                foreach (var fanId in newFans)
                {
                    var newFan = await context.Fans.FirstOrDefaultAsync(s => s.FanId == fanId);
                    if (newFan != null)
                    {
                        newFan.Room = room;
                        room.Fans.Add(newFan);
                    }
                }
            }
        }
    }
}
