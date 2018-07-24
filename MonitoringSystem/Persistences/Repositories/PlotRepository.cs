using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Extensions;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.Repositories
{
    public class PlotRepository : IPlotRepository
    {
        private ApplicationDbContext context;

        public PlotRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<Plot> GetAllTemperaturesBySensorIdForPlot(int sensorId)
        {
            var query = context.Temperatures
                    .Where(h => h.IsDeleted == false && h.Status.Sensor.SensorId == sensorId)
                    .OrderBy(t => t.Status.DateTime)
                    .AsQueryable();

            var sensor = await context.Sensors.FindAsync(sensorId);

            var plot = new Plot
            {
                x = await query.Select(q => q.Status.DateTime).ToListAsync(),
                y = await query.Select(q => q.Value).ToListAsync(),
                name = sensor.SensorName
            };
            return plot;
        }

        public async Task<QueryResult<Plot>> GetAllTemperatureOfAllSensorForPlot(Query queryObj)
        {
            var result = new QueryResult<Plot>();
            var query = context.Sensors
                    .AsQueryable();

            var plots = new Collection<Plot>();
            foreach (var sensor in query)
            {
                var temperature = context.Temperatures
                    .Where(h => h.IsDeleted == false && h.Status.Sensor.SensorId == sensor.SensorId)
                    .OrderBy(t => t.Status.DateTime)
                    .AsQueryable();
                var plot = new Plot
                {
                    x = await temperature.Select(q => q.Status.DateTime).ToListAsync(),
                    y = await temperature.Select(q => q.Value).ToListAsync(),
                    name = sensor.SensorName
                };
                plots.Add(plot);
            }

            result.Items = plots.ToList();
            result.TotalItems = result.Items.Count();

            return result;
        }
    }
}