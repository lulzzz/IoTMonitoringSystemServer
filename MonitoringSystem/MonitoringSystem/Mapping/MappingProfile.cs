using System.Linq;
using AutoMapper;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;

namespace MonitoringSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ////Domain to API Resource
            CreateMap<Temperature, TemperatureResource>()
                .ForMember(tr => tr.StatusId, opt => opt.MapFrom(t => t.Status.StatusId));

            CreateMap<Humidity, HumidityResource>()
                .ForMember(hr => hr.StatusId, opt => opt.MapFrom(h => h.Status.StatusId));

            CreateMap<Fan, FanResource>()
                .ForMember(fr => fr.RoomId, opt => opt.MapFrom(f => f.Room.RoomId));

            CreateMap<Rack, RackResource>()
                .ForMember(rr => rr.RoomId, opt => opt.MapFrom(r => r.Room.RoomId))
                .ForMember(rr => rr.Statuses, opt => opt.MapFrom(r => r.Statuses.Select(rf => rf.StatusId)));

            CreateMap<Room, RoomResource>()
                .ForMember(rr => rr.Statuses, opt => opt.MapFrom(r => r.Statuses.Select(rf => rf.StatusId)))
                .ForMember(rr => rr.Racks, opt => opt.MapFrom(r => r.Racks.Select(rf => rf.RackId)))
                .ForMember(rr => rr.Fans, opt => opt.MapFrom(r => r.Fans.Select(rf => rf.FanId)));

            CreateMap<Status, StatusResource>()
                .ForMember(sr => sr.RoomId, opt => opt.MapFrom(s => s.Room.RoomId))
                .ForMember(sr => sr.RackId, opt => opt.MapFrom(s => s.Rack.RackId))
                .ForMember(sr => sr.TemperatureId, opt => opt.MapFrom(s => s.Temperature.TemperatureId))
                .ForMember(sr => sr.Humidity, opt => opt.MapFrom(s => s.Humidity.HumidityId));

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            //API Resource to domain            
            CreateMap<TemperatureResource, Temperature>()
                .ForMember(m => m.TemperatureId, opt => opt.Ignore());

            CreateMap<HumidityResource, Humidity>()
                .ForMember(m => m.HumidityId, opt => opt.Ignore());

            CreateMap<FanResource, Fan>()
                .ForMember(m => m.FanId, opt => opt.Ignore());

            CreateMap<RackResource, Rack>()
                .ForMember(m => m.RackId, opt => opt.Ignore());

            CreateMap<RoomResource, Room>()
                .ForMember(m => m.RoomId, opt => opt.Ignore());

            CreateMap<StatusResource, Status>()
                .ForMember(m => m.StatusId, opt => opt.Ignore());
        }
    }
}
