using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Temperature> GetTemperature(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Temperatures.FindAsync(id);
            }
            return await context.Temperatures
                .SingleOrDefaultAsync(g => g.TemperatureId == id);
        }

        public void AddTemperature(Temperature temperature)
        {
            context.Temperatures.Add(temperature);
        }

        public void RemoveTemperature(Temperature temperature)
        {
            temperature.IsDeleted = true;
            //context.Remove(Activity);
        }

        public async Task<IEnumerable<Temperature>> GetTemperatures()
        {
            return await context.Temperatures
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }
    }
}
