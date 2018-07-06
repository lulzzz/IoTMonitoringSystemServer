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
    public class HumidityRepository : IHumidityRepository
    {
        private ApplicationDbContext context;

        public HumidityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddHumidity(Humidity humidity)
        {
            context.Humidities.Add(humidity);
        }

        public async Task<QueryResult<Humidity>> GetHumidities(Query queryObj)
        {
            var result = new QueryResult<Humidity>();
            var query = context.Humidities
                    .Where(h => h.IsDeleted == false)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Humidity, object>>>()
            {
                ["value"] = s => s.Value
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.HumidityId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<Humidity> GetHumidity(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Humidities.FindAsync(id);
            }
            return await context.Humidities
                .SingleOrDefaultAsync(h => h.HumidityId == id);
        }

        public void RemoveHumidity(Humidity humidity)
        {
            humidity.IsDeleted = true;
        }
    }
}
