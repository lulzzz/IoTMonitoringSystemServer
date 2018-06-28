using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/temperatures")]
    [ApiController]
    public class TemperatureController : Controller
    {
        private ITemperatureRepository temperatureRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IStatusRepository statusRepository;

        public TemperatureController(ITemperatureRepository temperatureRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IStatusRepository statusRepository)
        {
            this.temperatureRepository = temperatureRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.statusRepository = statusRepository;
        }
        // GET: api/temperatures/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<TemperatureResource>> GetTemperatures(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the temperatures with filter and sorting form of the query
            var queryResult = await temperatureRepository.GetTemperatures(query);

            //convert all of temperatures into temperatureResource json
            return mapper.Map<QueryResult<Temperature>, QueryResultResource<TemperatureResource>>(queryResult);
        }

        // GET: api/temperatures/gettemperature/5
        [HttpGet]
        [Route("gettemperature/{id}")]
        public async Task<IActionResult> GetTemperature(int id)
        {
            //get temperature for converting to json result
            var temperature = await temperatureRepository.GetTemperature(id);

            //check if temperature with the id dont exist in the database
            if (temperature == null)
            {
                return NotFound();
            }

            // converting temperature object to json result
            var temperatureResource = mapper.Map<Temperature, TemperatureResource>(temperature);

            return Ok(temperatureResource);
        }

        // POST: api/temperatures/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateTemperature([FromBody] TemperatureResource temperatureResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map temperatureResource json into temperature model
            var temperature = mapper.Map<TemperatureResource, Temperature>(temperatureResource);

            //add status for temperature
            temperature.Status = await statusRepository.GetStatus(temperatureResource.StatusId, true);

            //add temperature into database
            temperatureRepository.AddTemperature(temperature);
            await unitOfWork.Complete();

            //get temperature for converting to json result
            temperature = await temperatureRepository.GetTemperature(temperature.TemperatureId);
            var result = mapper.Map<Temperature, TemperatureResource>(temperature);

            return Ok(result);
        }

        // PUT: api/temperatures/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Updatetemperature(int id, [FromBody]TemperatureResource temperatureResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temperature = await temperatureRepository.GetTemperature(id);

            //check if temperature with the id dont exist in the database
            if (temperature == null)
            {
                return NotFound();
            }

            //edit status for temperature
            temperature.Status = await statusRepository.GetStatus(temperatureResource.StatusId, true);

            //map temperatureResource json into temperature model
            mapper.Map<TemperatureResource, Temperature>(temperatureResource, temperature);
            await unitOfWork.Complete();

            // converting temperature object to json result
            var result = mapper.Map<Temperature, TemperatureResource>(temperature);
            return Ok(result);
        }

        // DELETE: api/temperatures/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Deletetemperature(int id)
        {
            var temperature = await temperatureRepository.GetTemperature(id, includeRelated: false);

            //check if temperature with the id dont exist in the database
            if (temperature == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of temperature into true
            temperatureRepository.RemoveTemperature(temperature);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
