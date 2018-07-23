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
    public class LogRepository : ILogRepository
    {
        private ApplicationDbContext context;

        public LogRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddLog(Log log)
        {
            context.Logs.Add(log);
        }

        public async Task<Log> GetLog(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Logs.FindAsync(id);
            }
            return await context.Logs
                .SingleOrDefaultAsync(f => f.LogId == id);
        }

        public async Task<QueryResult<Log>> GetLogs(Query queryObj)
        {
            var result = new QueryResult<Log>();
            var query = context.Logs
                    .Where(a => a.IsDeleted == false)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Log, object>>>()
            {
                ["datetime"] = s => s.DateTime,
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.LogId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveLog(Log log)
        {
            log.IsDeleted = false;
        }
    }
}
