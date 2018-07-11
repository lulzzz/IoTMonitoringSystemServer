using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/sensors")]
    //[ApiController]
    public class SensorController : Controller
    {
        private ISensorRepository sensorRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public SensorController(ISensorRepository sensorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.sensorRepository = sensorRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        // GET: api/sensors/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<SensorResource>> GetSensors(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the rooms with filter and sorting form of the query
            var queryResult = await sensorRepository.GetSensors(query);

            //convert all of sensor into sensorResource json
            return mapper.Map<QueryResult<Sensor>, QueryResultResource<SensorResource>>(queryResult);
        }

        // GET: api/sensor/getsensor/5
        [HttpGet]
        [Route("getsensor/{id}")]
        public async Task<IActionResult> GetSensor(int id)
        {
            //get sensor for converting to json result
            var sensor = await sensorRepository.GetSensor(id);

            //check if sensor with the id dont exist in the database
            if (sensor == null)
            {
                return NotFound();
            }

            // converting sensor object to json result
            var sensorResource = mapper.Map<Sensor, SensorResource>(sensor);

            return Ok(sensorResource);
        }

        // POST: api/sensor/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateSensor([FromBody] SensorResource sensorResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map sensorResource json into Sensor model
            var sensor = mapper.Map<SensorResource, Sensor>(sensorResource);

            //add sensor into database
            sensorRepository.AddSensor(sensor);

            //update racks of sensor
            await sensorRepository.UpdateRacks(sensor, sensorResource);

            //update statuses of sensor
            await sensorRepository.UpdateStatuses(sensor, sensorResource);

            await unitOfWork.Complete();

            //get sensor for converting to json result
            sensor = await sensorRepository.GetSensor(sensor.SensorId);
            var result = mapper.Map<Sensor, SensorResource>(sensor);

            return Ok(result);
        }

        // PUT: api/sensors/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody]SensorResource sensorResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await sensorRepository.GetSensor(id);

            //check if sensor with the id dont exist in the database
            if (sensor == null)
            {
                return NotFound();
            }

            //map sensorResource json into sensor model
            mapper.Map<SensorResource, Sensor>(sensorResource, sensor);

            //update racks of sensor
            await sensorRepository.UpdateRacks(sensor, sensorResource);

            //update statuses of sensor
            await sensorRepository.UpdateStatuses(sensor, sensorResource);

            await unitOfWork.Complete();

            // converting sensor object to json result
            var result = mapper.Map<Sensor, SensorResource>(sensor);
            return Ok(result);
        }

        // DELETE: api/sensors/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await sensorRepository.GetSensor(id, includeRelated: false);

            //check if sensor with the id dont exist in the database
            if (sensor == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of sensor into true
            sensorRepository.RemoveSensor(sensor);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
