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
    public class StatusRepository : IStatusRepository
    {
        private ApplicationDbContext context;

        public StatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddStatus(Status status)
        {
            context.Statuses.Add(status);
        }

        public async Task<Status> GetStatus(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Statuses.FindAsync(id);
            }
            return await context.Statuses
                .Include(s => s.Humidity)
                .Include(s => s.Temperature)
                .SingleOrDefaultAsync(h => h.StatusId == id);
        }

        public async Task<QueryResult<Status>> GetStatuses(Query queryObj)
        {
            var result = new QueryResult<Status>();
            var query = context.Statuses
                    .Where(h => h.IsDeleted == false)
                    .Include(s => s.Humidity)
                    .Include(s => s.Temperature)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Status, object>>>()
            {
                ["datetime"] = s => s.DateTime,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.StatusId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveStatus(Status status)
        {
            status.IsDeleted = true;
        }
    }
}
