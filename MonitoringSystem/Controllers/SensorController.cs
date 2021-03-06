using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using PMS.Hubs;
using System;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Authorize]
    [Route("/api/sensors")]
    //[ApiController]
    public class SensorController : Controller
    {
        private ISensorRepository sensorRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IRoomRepository roomRepository;
        private IHubContext<MonitoringSystemHub> hubContext { get; set; }

        public SensorController(IHubContext<MonitoringSystemHub> hubContext, ISensorRepository sensorRepository, IMapper mapper, IUnitOfWork unitOfWork, IRoomRepository roomRepository)
        {
            this.sensorRepository = sensorRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roomRepository = roomRepository;
            this.hubContext = hubContext;
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
        [Authorize(Roles = "Admin")]
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

            //add room to sensor
            sensor.Room = await roomRepository.GetRoom(sensorResource.RoomId);

            //if room id is undefined in json which post to server
            if (!String.IsNullOrEmpty(sensorResource.RoomName))
            {
                sensor.Room = await roomRepository.GetRoomByRoomName(sensorResource.RoomName);
            }

            //update racks of sensor
            await sensorRepository.UpdateRacks(sensor, sensorResource);

            //update statuses of sensor
            await sensorRepository.UpdateStatuses(sensor, sensorResource);

            //add log
            sensorRepository.AddSensorLog(sensor);

            await unitOfWork.Complete();

            //get sensor for converting to json result
            sensor = await sensorRepository.GetSensor(sensor.SensorId);

            await hubContext.Clients.All.SendAsync("LoadData");

            var result = mapper.Map<Sensor, SensorResource>(sensor);

            return Ok(result);
        }

        // PUT: api/sensors/update/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody]SensorResource sensorResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await sensorRepository.GetSensor(id);

            //old sensor fpr log
            var oldSensor = sensor;

            //check if sensor with the id dont exist in the database
            if (sensor == null)
            {
                return NotFound();
            }

            //map sensorResource json into sensor model
            mapper.Map<SensorResource, Sensor>(sensorResource, sensor);

            //add room to sensor
            sensor.Room = await roomRepository.GetRoom(sensorResource.RoomId);

            //if sensor id is undefined in json which post to server
            if (!String.IsNullOrEmpty(sensorResource.RoomName))
            {
                sensor.Room = await roomRepository.GetRoomByRoomName(sensorResource.RoomName);
            }

            //update racks of sensor
            await sensorRepository.UpdateRacks(sensor, sensorResource);

            //update statuses of sensor
            await sensorRepository.UpdateStatuses(sensor, sensorResource);

            //add log
            sensorRepository.UpdateSensorLog(oldSensor, sensor);

            await unitOfWork.Complete();

            await hubContext.Clients.All.SendAsync("LoadData");
            // converting sensor object to json result
            var result = mapper.Map<Sensor, SensorResource>(sensor);
            return Ok(result);
        }

        // DELETE: api/sensors/delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
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

            await hubContext.Clients.All.SendAsync("LoadData");

            return Ok(id);
        }
    }
}
