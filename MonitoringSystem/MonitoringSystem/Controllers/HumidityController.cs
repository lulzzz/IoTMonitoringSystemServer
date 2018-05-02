using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/humidities")]
    public class HumidityController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IHumidityRepository repository;

        public HumidityController(IMapper mapper, IHumidityRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateHumidity([FromBody]HumidityResource humidityResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var humidity = mapper.Map<HumidityResource, Humidity>(humidityResource);

            repository.AddHumidity(humidity);
            await unitOfWork.Complete();

            humidity = await repository.GetHumidity(humidity.HumidityId);

            var result = mapper.Map<Humidity, HumidityResource>(humidity);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateHumidity(int id, [FromBody]HumidityResource humidityResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var humidity = await repository.GetHumidity(id);

            if (humidity == null)
                return NotFound();

            mapper.Map<HumidityResource, Humidity>(humidityResource, humidity);
            await unitOfWork.Complete();

            var result = mapper.Map<Humidity, HumidityResource>(humidity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteHumidity(int id)
        {
            var humidity = await repository.GetHumidity(id, includeRelated: false);

            if (humidity == null)
            {
                return NotFound();
            }

            repository.RemoveHumidity(humidity);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getActivity/{id}")]
        public async Task<IActionResult> GetHumidity(int id)
        {
            var humidity = await repository.GetHumidity(id);

            if (humidity == null)
            {
                return NotFound();
            }

            var humidityResource = mapper.Map<Humidity, HumidityResource>(humidity);

            return Ok(humidityResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetHumiditys()
        {
            var humidities = await repository.GetHumidities();
            var humiditiesResource = mapper.Map<IEnumerable<Humidity>, IEnumerable<HumidityResource>>(humidities);
            return Ok(humiditiesResource);
        }
    }
}
