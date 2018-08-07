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
    public class SensorRepository : ISensorRepository
    {
        private ApplicationDbContext context;

        public SensorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddSensor(Sensor sensor)
        {
            context.Sensors.Add(sensor);
        }

        public async Task<Sensor> GetSensor(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Sensors.FindAsync(id);
            }
            return await context.Sensors
                .Include(r => r.Room)
                .Include(r => r.Logs)
                .Include(r => r.Racks)
                .Include(r => r.Statuses)
                    .ThenInclude(s => s.Temperature)
                .Include(r => r.Statuses)
                    .ThenInclude(s => s.Humidity)
                .SingleOrDefaultAsync(r => r.SensorId == id);
        }

        public async Task<Sensor> GetSensorBySensorCode(string name, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Sensors.SingleOrDefaultAsync(r => r.SensorCode == name);
            }
            return await context.Sensors
                .Include(r => r.Room)
                .Include(r => r.Logs)
                .Include(r => r.Racks)
                .Include(r => r.Statuses)
                    .ThenInclude(s => s.Temperature)
                .Include(r => r.Statuses)
                    .ThenInclude(s => s.Humidity)
                .SingleOrDefaultAsync(r => r.SensorCode == name);
        }
        public async Task<QueryResult<Sensor>> GetSensors(Query queryObj)
        {
            var result = new QueryResult<Sensor>();
            var query = context.Sensors
                    .Where(r => r.IsDeleted == false)
                    .Include(r => r.Room)
                    .Include(r => r.Logs)
                    .Include(r => r.Racks)
                    .Include(r => r.Statuses)
                        .ThenInclude(s => s.Temperature)
                    .Include(r => r.Statuses)
                        .ThenInclude(s => s.Humidity)
                    .AsQueryable();
            //filter

            //sort
            var columnsMap = new Dictionary<string, Expression<Func<Sensor, object>>>()
            {
                ["sensorcode"] = s => s.SensorCode,
                ["sensorname"] = s => s.SensorName
            };
            if (queryObj.SortBy != "id" || queryObj.IsSortAscending != true)
            {
                query = query.OrderByDescending(s => s.SensorId);
            }
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            //paging
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveSensor(Sensor sensor)
        {
            sensor.IsDeleted = true;
        }

        public async Task UpdateRacks(Sensor sensor, SensorResource sensorResource)
        {
            if (sensorResource.Racks != null && sensorResource.Racks.Count >= 0)
            {
                //remove old racks
                var oldRacks = sensor.Racks.Where(s => !sensorResource.Racks.Any(id => id == s.RackId)).ToList();
                foreach (Rack rack in oldRacks)
                {
                    sensor.Racks.Remove(rack);
                }

                //add new racks
                var newRacks = sensorResource.Racks.Where(s => !sensor.Racks.Any(id => id.RackId == s)).ToList();
                foreach (var rackId in newRacks)
                {
                    var newRack = await context.Racks.FirstOrDefaultAsync(s => s.RackId == rackId);
                    if (newRack != null)
                    {
                        newRack.Sensor = sensor;
                        sensor.Racks.Add(newRack);
                    }
                }
            }
        }

        public async Task UpdateStatuses(Sensor sensor, SensorResource sensorResource)
        {
            if (sensorResource.Statuses != null && sensorResource.Statuses.Count >= 0)
            {
                //remove old statuses
                var oldStatuses = sensor.Statuses.Where(s => !sensorResource.Statuses.Any(id => id == s.StatusId)).ToList();
                foreach (Status status in oldStatuses)
                {
                    sensor.Statuses.Remove(status);
                }

                //add new statuses
                var newStatuses = sensorResource.Statuses.Where(s => !sensor.Statuses.Any(id => id.StatusId == s)).ToList();
                foreach (var statusId in newStatuses)
                {
                    var newStatus = await context.Statuses.FirstOrDefaultAsync(s => s.StatusId == statusId);
                    if (newStatus != null)
                    {
                        newStatus.Sensor = sensor;
                        sensor.Statuses.Add(newStatus);
                    }
                }
            }
        }

        public void AddSensorLog(Sensor sensor)
        {
            sensor.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "sensor name: " + sensor.SensorName + ", sensor code: " + sensor.SensorCode +
              " was added into " + sensor.Room.RoomName + "."
            });
        }

        public void UpdateSensorLog(Sensor oldSensor, Sensor sensor)
        {
            sensor.Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                Description = "sensor name change from: " + oldSensor.SensorName + " to " + sensor.SensorName +
                ", sensor code change from: " + oldSensor.SensorCode + " to " + sensor.SensorCode +
                ", room change from " + oldSensor.Room.RoomName + " to " + sensor.Room.RoomName + "."
            });
        }
    }
}
