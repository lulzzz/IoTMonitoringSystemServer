using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/fans")]
    //[ApiController]
    public class FanController : Controller
    {
        private IFanRepository fanRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IRoomRepository roomRepository;

        public FanController(IFanRepository fanRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IRoomRepository roomRepository)
        {
            this.fanRepository = fanRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roomRepository = roomRepository;
        }
        // GET: api/fans/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<FanResource>> GetFans(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the fans with filter and sorting form of the query
            var queryResult = await fanRepository.GetFans(query);

            //convert all of fans into FanResource json
            return mapper.Map<QueryResult<Fan>, QueryResultResource<FanResource>>(queryResult);
        }

        // GET: api/fans/getfan/5
        [HttpGet]
        [Route("getfan/{id}")]
        public async Task<IActionResult> GetFan(int id)
        {
            //get fan for converting to json result
            var fan = await fanRepository.GetFan(id);

            //check if fan with the id dont exist in the database
            if (fan == null)
            {
                return NotFound();
            }

            // converting fan object to json result
            var fanResource = mapper.Map<Fan, FanResource>(fan);

            return Ok(fanResource);
        }

        // POST: api/fans/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateFan([FromBody] FanResource fanResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map FanResource json into Fan model
            var fan = mapper.Map<FanResource, Fan>(fanResource);

            //add room for fan
            fan.Room = await roomRepository.GetRoom(fanResource.RoomId, true);

            //add fan into database
            fanRepository.AddFan(fan);
            await unitOfWork.Complete();

            //get fan for converting to json result
            fan = await fanRepository.GetFan(fan.FanId);
            var result = mapper.Map<Fan, FanResource>(fan);

            return Ok(result);
        }

        // PUT: api/fans/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateFan(int id, [FromBody]FanResource fanResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fan = await fanRepository.GetFan(id);

            //check if fan with the id dont exist in the database
            if (fan == null)
            {
                return NotFound();
            }
            //map FanResource json into Fan model
            mapper.Map<FanResource, Fan>(fanResource, fan);

            //edit room for fan
            fan.Room = await roomRepository.GetRoom(fanResource.RoomId, true);

            await unitOfWork.Complete();

            // converting fan object to json result
            var result = mapper.Map<Fan, FanResource>(fan);
            return Ok(result);
        }

        // DELETE: api/fans/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteFan(int id)
        {
            var fan = await fanRepository.GetFan(id, includeRelated: false);

            //check if fan with the id dont exist in the database
            if (fan == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of fan into true
            fanRepository.RemoveFan(fan);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
