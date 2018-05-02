using AutoMapper;
using MonitoringSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringSystem.Models;

namespace MonitoringSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Temperature, TemperatureResource>();
            CreateMap<Humidity, HumidityResource>();


            //API Resource to domain            
            CreateMap<TemperatureResource, Temperature>()
                .ForMember(m => m.TemperatureId, opt => opt.Ignore());
            CreateMap<HumidityResource, Humidity>()
                .ForMember(m => m.HumidityId, opt => opt.Ignore());

        }
    }
}