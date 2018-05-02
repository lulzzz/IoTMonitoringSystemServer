using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using MonitoringSystem.Models;

namespace MonitoringSystem.Controllers
{
    [Route("/api/temperatures")]
    public class TemperatureController : Controller
    {
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ITemperatureRepository repository;

        public TemperatureController(IMapper mapper, ITemperatureRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTemperature([FromBody]TemperatureResource temperatureResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temperature = mapper.Map<TemperatureResource, Temperature>(temperatureResource);

            repository.AddTemperature(temperature);
            await unitOfWork.Complete();

            temperature = await repository.GetTemperature(temperature.TemperatureId);

            var result = mapper.Map<Temperature, TemperatureResource>(temperature);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTemperature(int id, [FromBody]TemperatureResource temperatureResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temperature = await repository.GetTemperature(id);

            if (temperature == null)
                return NotFound();

            mapper.Map<TemperatureResource, Temperature>(temperatureResource, temperature);
            await unitOfWork.Complete();

            var result = mapper.Map<Temperature, TemperatureResource>(temperature);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTemperature(int id)
        {
            var temperature = await repository.GetTemperature(id, includeRelated: false);

            if (temperature == null)
            {
                return NotFound();
            }

            repository.RemoveTemperature(temperature);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getActivity/{id}")]
        public async Task<IActionResult> GetTemperature(int id)
        {
            var temperature = await repository.GetTemperature(id);

            if (temperature == null)
            {
                return NotFound();
            }

            var temperatureResource = mapper.Map<Temperature, TemperatureResource>(temperature);

            return Ok(temperatureResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetTemperatures()
        {
            var temperatures = await repository.GetTemperatures();
            var temperaturesResource = mapper.Map<IEnumerable<Temperature>, IEnumerable<TemperatureResource>>(temperatures);
            return Ok(temperaturesResource);
        }
    }
}