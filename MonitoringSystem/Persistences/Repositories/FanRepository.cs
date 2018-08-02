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
                .Include(f => f.FanStatuses)
                .SingleOrDefaultAsync(f => f.FanId == id);
        }

        public async Task<QueryResult<Fan>> GetFans(Query queryObj)
        {
            var result = new QueryResult<Fan>();
            var query = context.Fans
                    .Where(a => a.IsDeleted == false)
                    .Include(f => f.Room)
                    .Include(f => f.FanStatuses)
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

        public async Task<Fan> GetFanByFanCode(string fanCode, bool includeRelated = true)
        {
            return await context.Fans
                .Include(f => f.Room)
                .Include(f => f.FanStatuses)
                .SingleOrDefaultAsync(f => f.FanCode == fanCode);
        }

        public bool checkFan(Fan fan, FanStatusResource fanStatusResource)
        {
            var currentFanStatuses = fan.FanStatuses
            .OrderByDescending(fs => fs.DateTime)
            .Where(fs => fs.IsDeleted == false)
            .Take(12)
            .ToList();

            if (currentFanStatuses.Count != 12)
            {
                return true;
            }

            var vibrationTrueCount = 0;
            var vibrationFalseCount = 0;

            foreach (var fanstatus in currentFanStatuses)
            {
                if (fanstatus.Vibration == 0)
                {
                    vibrationFalseCount++;
                }
                else
                {
                    vibrationTrueCount++;
                }
            }

            if ((fan.IsOn && vibrationFalseCount >= 8) || (!fan.IsOn && vibrationTrueCount >= 8))
            {
                foreach (var fanstatus in currentFanStatuses)
                {
                    fanstatus.IsDeleted = true;
                }
                return false;
            }

            return true;
        }

        public void ClearFanStatus(Fan fan)
        {
            var currentFanStatuses = fan.FanStatuses
                .Where(fs => fs.IsDeleted == false)
                .ToList();
            foreach (var fanstatus in currentFanStatuses)
            {
                fanstatus.IsDeleted = true;
            }
        }
    }
}
