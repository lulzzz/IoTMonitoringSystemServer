using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Authorize]
    [Route("/api/humidities")]
    //[ApiController]
    public class HumidityController : Controller
    {
        private IHumidityRepository humidityRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public HumidityController(IHumidityRepository humidityRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.humidityRepository = humidityRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        // GET: api/humidities/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<HumidityResource>> GetHumidities(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the Humiditys with filter and sorting form of the query
            var queryResult = await humidityRepository.GetHumidities(query);

            //convert all of Humiditys into HumidityResource json
            return mapper.Map<QueryResult<Humidity>, QueryResultResource<HumidityResource>>(queryResult);
        }

        // GET: api/humidities/gethumidity/5
        [HttpGet]
        [Route("gethumidity/{id}")]
        public async Task<IActionResult> GetHumidity(int id)
        {
            //get Humidity for converting to json result
            var humidity = await humidityRepository.GetHumidity(id);

            //check if Humidity with the id dont exist in the database
            if (humidity == null)
            {
                return NotFound();
            }

            // converting Humidity object to json result
            var HumidityResource = mapper.Map<Humidity, HumidityResource>(humidity);

            return Ok(HumidityResource);
        }

        // POST: api/humidities/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateHumidity([FromBody] HumidityResource humidityResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map HumidityResource json into Humidity model
            var humidity = mapper.Map<HumidityResource, Humidity>(humidityResource);

            //add Humidity into database
            humidityRepository.AddHumidity(humidity);
            await unitOfWork.Complete();

            //get Humidity for converting to json result
            humidity = await humidityRepository.GetHumidity(humidity.HumidityId);
            var result = mapper.Map<Humidity, HumidityResource>(humidity);

            return Ok(result);
        }

        // PUT: api/humidities/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateHumidity(int id, [FromBody]HumidityResource humidityResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var humidity = await humidityRepository.GetHumidity(id);

            //check if Humidity with the id dont exist in the database
            if (humidity == null)
            {
                return NotFound();
            }
            //map HumidityResource json into Humidity model
            mapper.Map<HumidityResource, Humidity>(humidityResource, humidity);
            await unitOfWork.Complete();

            // converting Humidity object to json result
            var result = mapper.Map<Humidity, HumidityResource>(humidity);
            return Ok(result);
        }

        // DELETE: api/Humidities/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteHumidity(int id)
        {
            var Humidity = await humidityRepository.GetHumidity(id, includeRelated: false);

            //check if Humidity with the id dont exist in the database
            if (Humidity == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of Humidity into true
            humidityRepository.RemoveHumidity(Humidity);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
