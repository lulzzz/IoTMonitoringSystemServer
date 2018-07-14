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
    public class TemperatureRepository : ITemperatureRepository
    {
        private ApplicationDbContext context;

        public TemperatureRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddTemperature(Temperature temperature)
        {
            context.Temperatures.Add(temperature);
        }

        public async Task<Temperature> GetTemperature(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Temperatures.FindAsync(id);
            }
            return await context.Temperatures
                .Include(t => t.Status)
                    .ThenInclude(s => s.Sensor)
                .Include(t => t.Status)
                    .ThenInclude(s => s.Temperature)
                .Include(t => t.Status)
                    .ThenInclude(s => s.Humidity)
                .SingleOrDefaultAsync(h => h.TemperatureId == id);
        }

        public async Task<QueryResult<Temperature>> GetTemperatures(Query queryObj)
        {
            var result = new QueryResult<Temperature>();
            var query = context.Temperatures
                    .Where(h => h.IsDeleted == false)
                    .Include(t => t.Status)
                        .ThenInclude(s => s.Sensor)
                    .Include(t => t.Status)
                        .ThenInclude(s => s.Temperature)
                    .Include(t => t.Status)
                        .ThenInclude(s => s.Humidity)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Temperature, object>>>()
            {
                ["value"] = s => s.Value
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.TemperatureId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveTemperature(Temperature temperature)
        {
            temperature.IsDeleted = true;
        }
    }
}
