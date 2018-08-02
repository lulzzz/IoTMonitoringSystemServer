using System.Linq;
using AutoMapper;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using MonitoringSystem.Resources.SubResources;

namespace MonitoringSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ////Domain to API Resource
            CreateMap<Temperature, TemperatureResource>()
                .ForMember(tr => tr.StatusId, opt => opt.MapFrom(t => t.Status.StatusId))
                .ForMember(tr => tr.Status, opt => opt.MapFrom(t => new StatusResource
                {
                    StatusId = t.Status.StatusId,
                    DateTime = t.Status.DateTime,
                    TemperatureId = t.Status.Temperature.TemperatureId,
                    Temperature = new TemperatureResource
                    {
                        TemperatureId = t.Status.Temperature.TemperatureId,
                        Value = t.Status.Temperature.Value,
                        IsDeleted = t.Status.Temperature.IsDeleted,
                        StatusId = t.Status.StatusId
                    },
                    HumidityId = t.Status.Humidity.HumidityId,
                    Humidity = new HumidityResource
                    {
                        HumidityId = t.Status.Temperature.TemperatureId,
                        Value = t.Status.Temperature.Value,
                        IsDeleted = t.Status.Temperature.IsDeleted,
                        StatusId = t.Status.StatusId
                    },
                    IsDeleted = t.Status.IsDeleted,
                    SensorId = t.Status.Sensor.SensorId
                }));

            CreateMap<Humidity, HumidityResource>()
                .ForMember(hr => hr.StatusId, opt => opt.MapFrom(h => h.Status.StatusId))
                .ForMember(hr => hr.Status, opt => opt.MapFrom(h => new StatusResource
                {
                    StatusId = h.Status.StatusId,
                    DateTime = h.Status.DateTime,
                    TemperatureId = h.Status.Temperature.TemperatureId,
                    Temperature = new TemperatureResource
                    {
                        TemperatureId = h.Status.Temperature.TemperatureId,
                        Value = h.Status.Temperature.Value,
                        IsDeleted = h.Status.Temperature.IsDeleted,
                        StatusId = h.Status.StatusId
                    },
                    HumidityId = h.Status.Humidity.HumidityId,
                    Humidity = new HumidityResource
                    {
                        HumidityId = h.Status.Temperature.TemperatureId,
                        Value = h.Status.Temperature.Value,
                        IsDeleted = h.Status.Temperature.IsDeleted,
                        StatusId = h.Status.StatusId
                    },
                    IsDeleted = h.Status.IsDeleted,
                    SensorId = h.Status.Sensor.SensorId
                }));

            CreateMap<Fan, FanResource>()
                .ForMember(fr => fr.FanStatuses, opt => opt.MapFrom(f => f.FanStatuses.Select(ff => ff.FanStatusId)))
                .ForMember(fr => fr.RoomId, opt => opt.MapFrom(f => f.Room.RoomId))
                .ForMember(fr => fr.RoomName, opt => opt.MapFrom(f => f.Room.RoomName));

            CreateMap<FanStatus, FanStatusResource>()
                .ForMember(fr => fr.FanId, opt => opt.MapFrom(f => f.Fan.FanId))
                .ForMember(fr => fr.FanCode, opt => opt.MapFrom(f => f.Fan.FanCode));

            CreateMap<Rack, RackResource>()
                .ForMember(rr => rr.RoomId, opt => opt.MapFrom(r => r.Room.RoomId))
                .ForMember(rr => rr.RoomName, opt => opt.MapFrom(r => r.Room.RoomName))
                .ForMember(rr => rr.SensorId, opt => opt.MapFrom(r => r.Sensor.SensorId));

            CreateMap<Room, RoomResource>()
                .ForMember(rr => rr.Sensors, opt => opt.MapFrom(r => r.Sensors.Select(rf => rf.SensorId)))
                .ForMember(rr => rr.Racks, opt => opt.MapFrom(r => r.Racks.Select(rf => rf.RackId)))
                .ForMember(rr => rr.Fans, opt => opt.MapFrom(r => r.Fans.Select(rf => rf.FanId)));

            CreateMap<Status, StatusResource>()
                .ForMember(sr => sr.SensorId, opt => opt.MapFrom(s => s.Sensor.SensorId))
                .ForMember(sr => sr.TemperatureId, opt => opt.MapFrom(s => s.Temperature.TemperatureId))
                .ForMember(sr => sr.HumidityId, opt => opt.MapFrom(s => s.Humidity.HumidityId));

            CreateMap<Sensor, SensorResource>()
                .ForMember(sr => sr.Statuses, opt => opt.MapFrom(s => s.Statuses.Select(sf => sf.StatusId)))
                .ForMember(sr => sr.Racks, opt => opt.MapFrom(s => s.Racks.Select(sf => sf.RackId)))
                .ForMember(sr => sr.RackNames, opt => opt.MapFrom(s => s.Racks.Select(sf => sf.RackName)))
                .ForMember(sr => sr.RoomId, opt => opt.MapFrom(s => s.Room.RoomId))
                .ForMember(sr => sr.RoomName, opt => opt.MapFrom(s => s.Room.RoomName))
                .ForMember(sr => sr.LatestStatus, opt => opt.MapFrom(s => new LatestStatusResource
                {
                    text = s.SensorCode,
                    sensor = s.SensorName,
                    temperature = s.Statuses.OrderByDescending(t => t.DateTime)
                                .FirstOrDefault().Temperature.Value,
                    humidity = s.Statuses.OrderByDescending(t => t.DateTime)
                                .FirstOrDefault().Humidity.Value,
                }));

            CreateMap<Plot, PlotResource>();

            CreateMap<Log, LogResource>();

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            //API Resource to domain            
            CreateMap<TemperatureResource, Temperature>()
                    .ForMember(m => m.TemperatureId, opt => opt.Ignore());

            CreateMap<HumidityResource, Humidity>()
                    .ForMember(m => m.HumidityId, opt => opt.Ignore());

            CreateMap<FanResource, Fan>()
                    .ForMember(m => m.FanStatuses, opt => opt.Ignore())
                    .ForMember(m => m.Room, opt => opt.Ignore())
                    .ForMember(m => m.FanId, opt => opt.Ignore());

            CreateMap<FanStatusResource, FanStatus>()
                    .ForMember(m => m.FanStatusId, opt => opt.Ignore());

            CreateMap<RackResource, Rack>()
                    .ForMember(m => m.RackId, opt => opt.Ignore());

            CreateMap<RoomResource, Room>()
                    .ForMember(m => m.RoomId, opt => opt.Ignore());

            CreateMap<StatusResource, Status>()
                    .ForMember(m => m.StatusId, opt => opt.Ignore());

            CreateMap<SensorResource, Sensor>()
                    .ForMember(m => m.SensorId, opt => opt.Ignore());

            CreateMap<LogResource, Log>()
                    .ForMember(m => m.LogId, opt => opt.Ignore());
        }
    }
}
