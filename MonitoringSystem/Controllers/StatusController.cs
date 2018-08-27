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
    [Route("/api/statuses")]
    //[ApiController]
    public class StatusController : Controller
    {
        private IStatusRepository statusRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private ISensorRepository sensorRepository;
        private IHubContext<MonitoringSystemHub> hubContext;

        public StatusController(IStatusRepository statusRepository, IMapper mapper, IUnitOfWork unitOfWork,
        ISensorRepository sensorRepository, IHubContext<MonitoringSystemHub> hubContext)
        {
            this.statusRepository = statusRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.sensorRepository = sensorRepository;
            this.hubContext = hubContext;
        }
        // GET: api/statuses/getall
        [HttpGet]
        [AllowAnonymous]
        [Route("getall")]
        public async Task<QueryResultResource<StatusResource>> Getstatuses(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the statuss with filter and sorting form of the query
            var queryResult = await statusRepository.GetStatuses(query);

            //convert all of statuss into statusResource json
            return mapper.Map<QueryResult<Status>, QueryResultResource<StatusResource>>(queryResult);
        }

        // GET: api/statuses/getstatus/5
        [HttpGet]
        [Route("getstatus/{id}")]
        public async Task<IActionResult> Getstatus(int id)
        {
            //get status for converting to json result
            var status = await statusRepository.GetStatus(id);

            //check if status with the id dont exist in the database
            if (status == null)
            {
                return NotFound();
            }

            // converting status object to json result
            var statusResource = mapper.Map<Status, StatusResource>(status);

            return Ok(statusResource);
        }

        // POST: api/statuses/add
        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateStatus([FromBody] StatusResource statusResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map statusResource json into status model
            var status = mapper.Map<StatusResource, Status>(statusResource);

            //add sensor for status
            status.Sensor = await sensorRepository.GetSensor(statusResource.SensorId);

            //if sensor id is undefined in son which post to server
            if (!String.IsNullOrEmpty(statusResource.SensorCode))
            {
                status.Sensor = await sensorRepository.GetSensorBySensorCode(statusResource.SensorCode);
            }

            //case sensor is not defined
            if (status.Sensor == null)
            {
                return BadRequest("cannot find any sensor with this sensorId or sensorName");
            }

            //add status into database
            statusRepository.AddStatus(status);

            //add temperature
            statusRepository.AddTemperature(status, statusResource.TemperatureValue);

            //add humidity
            statusRepository.AddHumidity(status, statusResource.HumidityValue);

            //add log
            statusRepository.AddStatusLog(status);

            await unitOfWork.Complete();

            await hubContext.Clients.All.SendAsync("LoadData");

            //get status for converting to json result
            status = await statusRepository.GetStatus(status.StatusId);
            var result = mapper.Map<Status, StatusResource>(status);

            return Ok(result);
        }

        // PUT: api/statuss/update/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{id}")]
        public async Task<IActionResult> Updatestatus(int id, [FromBody]StatusResource statusResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await statusRepository.GetStatus(id);

            //old status for log
            var oldStatus = status;

            //check if status with the id dont exist in the database
            if (status == null)
            {
                return NotFound();
            }
            //map statusResource json into status model
            mapper.Map<StatusResource, Status>(statusResource, status);

            //add sensor for status
            status.Sensor = await sensorRepository.GetSensor(statusResource.SensorId);

            //if sensor id is undefined in son which post to server
            if (!String.IsNullOrEmpty(statusResource.SensorCode))
            {
                status.Sensor = await sensorRepository.GetSensorBySensorCode(statusResource.SensorCode);
            }

            //case sensor is not defined
            if (status.Sensor == null)
            {
                return BadRequest("cannot find any sensor with this sensorId or sensorName");
            }

            //add temperature
            statusRepository.AddTemperature(status, statusResource.TemperatureValue);

            //add humidity
            statusRepository.AddHumidity(status, statusResource.HumidityValue);

            //add log
            statusRepository.UpdateStatusLog(oldStatus, status);

            await unitOfWork.Complete();

            // converting status object to json result
            var result = mapper.Map<Status, StatusResource>(status);
            return Ok(result);
        }

        // DELETE: api/statuss/delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> Deletestatus(int id)
        {
            var status = await statusRepository.GetStatus(id, includeRelated: false);

            //check if status with the id dont exist in the database
            if (status == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of status into true
            statusRepository.RemoveStatus(status);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
