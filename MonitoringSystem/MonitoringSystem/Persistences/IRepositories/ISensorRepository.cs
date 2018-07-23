using MonitoringSystem.Models;
using MonitoringSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface ISensorRepository
    {
        Task<Sensor> GetSensor(int? id, bool includeRelated = true);
        void AddSensor(Sensor sensor);
        void RemoveSensor(Sensor sensor);
        Task<QueryResult<Sensor>> GetSensors(Query queryObj);
        Task UpdateRacks(Sensor sensor, SensorResource sensorResource);
        Task UpdateStatuses(Sensor sensor, SensorResource sensorResource);

    }
}