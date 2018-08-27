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

        public void AddHumidity(Status status, double? humidityValue)
        {
            status.Humidity = new Humidity { IsDeleted = false, Value = humidityValue.Value, Status = status };
        }

        public void AddStatus(Status status)
        {
            context.Statuses.Add(status);
        }

        public void AddTemperature(Status status, double? temperatureValue)
        {
            status.Temperature = new Temperature { IsDeleted = false, Value = temperatureValue.Value, Status = status };
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
                .Include(s => s.Sensor)
                    .ThenInclude(se => se.Logs)
                .Include(s => s.Sensor)
                    .ThenInclude(se => se.Racks)
                .SingleOrDefaultAsync(h => h.StatusId == id);
        }

        public async Task<QueryResult<Status>> GetStatuses(Query queryObj)
        {
            var result = new QueryResult<Status>();
            var query = context.Statuses
                    .Where(h => h.IsDeleted == false)
                    .Include(s => s.Humidity)
                    .Include(s => s.Temperature)
                    .Include(s => s.Sensor)
                    // .ThenInclude(se => se.Logs)
                    //.Include(s => s.Sensor)
                    //.ThenInclude(se => se.Racks)
                    .AsQueryable();
            //filter
            if (queryObj.SensorId.HasValue)
            {
                query = query.Where(q => q.Sensor.SensorId == queryObj.SensorId);
            }
            if (queryObj.RackId.HasValue)
            {
                query = query.Where(q => q.Sensor.Racks.Any(r => r.RackId == queryObj.RackId));
            }
            if (queryObj.StartDate.HasValue && queryObj.EndDate.HasValue)
            {
                query = query.Where(q => q.DateTime >= queryObj.StartDate && q.DateTime <= queryObj.EndDate);
            }
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

        public void AddStatusLog(Status status)
        {
            status.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "status have temperature's value is: " + status.Temperature.Value + ", status have humidity's value is: " + status.Humidity.Value +
                " was added into sensor " + status.Sensor.SensorName + "."
            });

            status.Sensor.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "status have temperature's value is: " + status.Temperature.Value + ", status have humidity's value is: " + status.Humidity.Value +
                " was added into sensor " + status.Sensor.SensorName + "."
            });

            foreach (var rack in status.Sensor.Racks)
            {
                rack.Logs.Add(new Log
                {
                    DateTime = DateTime.Now,
                    Description = "status have temperature's value is: " + status.Temperature.Value + ", status have humidity's value is: " + status.Humidity.Value +
                " was added into sensor " + status.Sensor.SensorName + "whick is watching rack name: " + rack.RackName + " - rack code: " + rack.RackCode
                });
            }
        }

        public void UpdateStatusLog(Status oldStatus, Status status)
        {
            status.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "status have temperature's value change from " + oldStatus.Temperature.Value + " to " + status.Temperature.Value +
                 ", status have humidity's value change from " + oldStatus.Humidity.Value + " to " + status.Humidity.Value +
                " was added into sensor change from " + oldStatus.Sensor.SensorName + " to " + status.Sensor.SensorName + "."
            });

            status.Sensor.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "status have temperature's value change from " + oldStatus.Temperature.Value + " to " + status.Temperature.Value +
                 ", status have humidity's value change from " + oldStatus.Humidity.Value + " to " + status.Humidity.Value +
                " was added into sensor change from " + oldStatus.Sensor.SensorName + " to " + status.Sensor.SensorName + "."
            });
        }
    }
}
