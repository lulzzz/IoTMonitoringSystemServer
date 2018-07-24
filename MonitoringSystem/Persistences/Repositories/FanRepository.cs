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
    public class FanRepository : IFanRepository
    {
        private ApplicationDbContext context;

        public FanRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddFan(Fan fan)
        {
            context.Fans.Add(fan);
        }

        public async Task<Fan> GetFan(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Fans.FindAsync(id);
            }
            return await context.Fans
                .Include(f => f.Room)
                .SingleOrDefaultAsync(f => f.FanId == id);
        }

        public async Task<QueryResult<Fan>> GetFans(Query queryObj)
        {
            var result = new QueryResult<Fan>();
            var query = context.Fans
                    .Where(a => a.IsDeleted == false)
                    .Include(f => f.Room)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Fan, object>>>()
            {
                ["fancode"] = s => s.FanCode,
                ["fanname"] = s => s.FanName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.FanId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveFan(Fan fan)
        {
            fan.IsDeleted = false;
        }

        public void AddFanLog(Fan fan)
        {
            fan.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "fan name: " + fan.FanName + ", fan code: " + fan.FanCode +
                ", capacity: " + fan.Capacity + " was added into " + fan.Room.RoomName + "."
            });
        }

        public void UpdateFanLog(Fan oldFan, Fan fan)
        {
            fan.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "fan name change from: " + oldFan.FanName + " to " + fan.FanName +
                ", fan code change from: " + oldFan.FanCode + " to " + fan.FanCode +
                ", capacity change from: " + oldFan.Capacity + " to " + fan.Capacity +
                ", room change from " + oldFan.Room.RoomName + " to " + fan.Room.RoomName + "."
            });
        }
    }
}
