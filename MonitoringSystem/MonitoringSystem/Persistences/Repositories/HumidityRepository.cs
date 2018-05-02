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
    public class HumidityRepository : IHumidityRepository
    {
        private ApplicationDbContext context;

        public HumidityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Humidity> GetHumidity(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Humidities.FindAsync(id);
            }
            return await context.Humidities
                .SingleOrDefaultAsync(g => g.HumidityId == id);
        }

        public void AddHumidity(Humidity humidity)
        {
            context.Humidities.Add(humidity);
        }

        public void RemoveHumidity(Humidity humidity)
        {
            humidity.IsDeleted = true;
            //context.Remove(Activity);
        }

        public async Task<IEnumerable<Humidity>> GetHumidities()
        {
            return await context.Humidities
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }
    }
}
