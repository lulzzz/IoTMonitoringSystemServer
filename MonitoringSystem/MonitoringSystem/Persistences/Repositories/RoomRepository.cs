using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Extensions;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
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
                .Include(r => r.Statuses)
                .Include(r => r.Racks)
                .Include(r => r.Fans)
                .SingleOrDefaultAsync(h => h.RoomId == id);
        }

        public async Task<QueryResult<Room>> GetRooms(Query queryObj)
        {
            var result = new QueryResult<Room>();
            var query = context.Rooms
                        .Include(r => r.Statuses)
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
    }
}
