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
    public class FanStatusRepository : IFanStatusRepository
    {
        private ApplicationDbContext context;

        public FanStatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddFanStatus(FanStatus fanStatus)
        {
            context.FanStatuses.Add(fanStatus);
        }

        public async Task<FanStatus> GetFanStatus(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.FanStatuses.FindAsync(id);
            }
            return await context.FanStatuses
                .Include(f => f.Fan)
                .SingleOrDefaultAsync(f => f.FanStatusId == id);
        }

        public async Task<QueryResult<FanStatus>> GetFanStatuses(Query queryObj)
        {
            var result = new QueryResult<FanStatus>();
            var query = context.FanStatuses
                    .Where(a => a.IsDeleted == false)
                    .Include(f => f.Fan)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<FanStatus, object>>>()
            {
                ["datetime"] = s => s.DateTime
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.FanStatusId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveFanStatus(FanStatus fanStatus)
        {
            fanStatus.IsDeleted = false;
        }

        public void AddFanStatusLog(FanStatus fanStatus)
        {
            fanStatus.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "fan name: " + fanStatus.Fan.FanName + ", fan code: " + fanStatus.Fan.FanCode +
                ", vibration: " + fanStatus.Vibration + " when  " + fanStatus.DateTime + "."
            });
        }

    }
}
