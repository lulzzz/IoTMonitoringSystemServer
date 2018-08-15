using MonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IPlotRepository
    {
        Task<Plot> GetAllTemperaturesBySensorIdForPlot(int sensorId);
        Task<QueryResult<Plot>> GetAllTemperatureOfAllSensorForPlot(Query queryObj);
        Task<QueryResult<Plot>> GetAllHumidityOfAllSensorForPlot(Query queryObj);
        Task<SensorPlot> GetLatestHumidityOfAllSensorForPlot();
        Task<SensorPlot> GetLatestTemperatureOfAllSensorForPlot();
    }
}
