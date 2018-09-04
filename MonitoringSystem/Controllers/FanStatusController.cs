using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using MonitoringSystem.Resources.SubResources;
using System;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/api/fanStatus")]
    //[ApiController]
    public class FanStatusController : Controller
    {
        private IFanStatusRepository fanStatusRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IFanRepository fanRepository;

        public FanStatusController(IFanStatusRepository fanStatusRepository, IMapper mapper,
         IUnitOfWork unitOfWork, IFanRepository fanRepository)
        {
            this.fanStatusRepository = fanStatusRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.fanRepository = fanRepository;
        }
        // GET: api/fanStatuss/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<FanStatusResource>> GetfanStatuss(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the fanStatuss with filter and sorting form of the query
            var queryResult = await fanStatusRepository.GetFanStatuses(query);

            //convert all of fanStatuss into fanStatusResource json
            return mapper.Map<QueryResult<FanStatus>, QueryResultResource<FanStatusResource>>(queryResult);
        }

        // GET: api/fanStatuss/getfanStatus/5
        [HttpGet]
        [Route("getfanstatus/{id}")]
        public async Task<IActionResult> GetFanStatus(int id)
        {
            //get fanStatus for converting to json result
            var fanStatus = await fanStatusRepository.GetFanStatus(id);

            //check if fanStatus with the id dont exist in the database
            if (fanStatus == null)
            {
                return NotFound();
            }

            // converting fanStatus object to json result
            var fanStatusResource = mapper.Map<FanStatus, FanStatusResource>(fanStatus);

            return Ok(fanStatusResource);
        }

        // POST: api/fanStatuss/add
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add")]
        public async Task<IActionResult> CreatefanStatus([FromBody] FanStatusResource fanStatusResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (fanStatusResource.DateTime == null)
            {
                fanStatusResource.DateTime = DateTime.Now;
            }

            //map fanStatusResource json into fanStatus model
            var fanStatus = mapper.Map<FanStatusResource, FanStatus>(fanStatusResource);

            //add Fan for fanStatus
            fanStatus.Fan = await fanRepository.GetFanByFanCode(fanStatusResource.FanCode, true);

            //add log
            fanStatusRepository.AddFanStatusLog(fanStatus);

            //add fanStatus into database
            fanStatusRepository.AddFanStatus(fanStatus);
            await unitOfWork.Complete();

            //get fanStatus for converting to json result
            fanStatus = await fanStatusRepository.GetFanStatus(fanStatus.FanStatusId);
            var result = mapper.Map<FanStatus, FanStatusResource>(fanStatus);

            return Ok(result);
        }

        // PUT: api/fanStatuss/update/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateFanStatus(int id, [FromBody]FanStatusResource fanStatusResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fanStatus = await fanStatusRepository.GetFanStatus(id);

            //check if fanStatus with the id dont exist in the database
            if (fanStatus == null)
            {
                return NotFound();
            }

            if (fanStatusResource.DateTime == null)
            {
                fanStatusResource.DateTime = DateTime.Now;
            }

            //map fanStatusResource json into fanStatus model
            mapper.Map<FanStatusResource, FanStatus>(fanStatusResource, fanStatus);

            //edit room for fanStatus
            fanStatus.Fan = await fanRepository.GetFanByFanCode(fanStatusResource.FanCode, true);

            await unitOfWork.Complete();

            // converting fanStatus object to json result
            var result = mapper.Map<FanStatus, FanStatusResource>(fanStatus);
            return Ok(result);
        }

        // DELETE: api/fanStatuss/delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteFanStatus(int id)
        {
            var fanStatus = await fanStatusRepository.GetFanStatus(id, includeRelated: false);

            //check if fanStatus with the id dont exist in the database
            if (fanStatus == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of fanStatus into true
            fanStatusRepository.RemoveFanStatus(fanStatus);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
