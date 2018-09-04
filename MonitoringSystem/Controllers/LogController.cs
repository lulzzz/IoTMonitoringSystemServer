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
    [Route("/api/logs")]
    //[ApiController]
    public class LogController : Controller
    {
        private ILogRepository logRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IRoomRepository roomRepository;

        public LogController(ILogRepository logRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IRoomRepository roomRepository)
        {
            this.logRepository = logRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roomRepository = roomRepository;
        }
        // GET: api/logs/getall
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("getall")]
        public async Task<QueryResultResource<LogResource>> GetLogs(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the logs with filter and sorting form of the query
            var queryResult = await logRepository.GetLogs(query);

            //convert all of fans into FanResource json
            return mapper.Map<QueryResult<Log>, QueryResultResource<LogResource>>(queryResult);
        }

        // GET: api/logs/getlog/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("getlog/{id}")]
        public async Task<IActionResult> GetLog(int id)
        {
            //get log for converting to json result
            var log = await logRepository.GetLog(id);

            //check if log with the id dont exist in the database
            if (log == null)
            {
                return NotFound();
            }

            // converting log object to json result
            var logResource = mapper.Map<Log, LogResource>(log);

            return Ok(logResource);
        }

        // POST: api/logs/add
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add")]
        public async Task<IActionResult> CreateLog([FromBody] LogResource logResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map LogResource json into log model
            var log = mapper.Map<LogResource, Log>(logResource);

            //add log into database
            logRepository.AddLog(log);
            await unitOfWork.Complete();

            //get log for converting to json result
            log = await logRepository.GetLog(log.LogId);
            var result = mapper.Map<Log, LogResource>(log);

            return Ok(result);
        }

        // PUT: api/logs/update/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateLog(int id, [FromBody]LogResource logResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var log = await logRepository.GetLog(id);

            //check if log with the id dont exist in the database
            if (log == null)
            {
                return NotFound();
            }

            //map LogResource json into Log model
            mapper.Map<LogResource, Log>(logResource, log);

            await unitOfWork.Complete();

            // converting log object to json result
            var result = mapper.Map<Log, LogResource>(log);
            return Ok(result);
        }

        // DELETE: api/logs/delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var log = await logRepository.GetLog(id, includeRelated: false);

            //check if log with the id dont exist in the database
            if (log == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of fan into true
            logRepository.RemoveLog(log);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
