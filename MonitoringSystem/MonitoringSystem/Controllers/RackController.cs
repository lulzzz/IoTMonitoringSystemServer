using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/racks")]
    //[ApiController]
    public class RackController : Controller
    {
        private IRackRepository rackRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IRoomRepository roomRepository;

        public RackController(IRackRepository rackRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IRoomRepository roomRepository)
        {
            this.rackRepository = rackRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roomRepository = roomRepository;
        }
        // GET: api/racks/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<RackResource>> Getracks(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the racks with filter and sorting form of the query
            var queryResult = await rackRepository.GetRacks(query);

            //convert all of racks into rackResource json
            return mapper.Map<QueryResult<Rack>, QueryResultResource<RackResource>>(queryResult);
        }

        // GET: api/racks/getrack/5
        [HttpGet]
        [Route("getrack/{id}")]
        public async Task<IActionResult> Getrack(int id)
        {
            //get rack for converting to json result
            var rack = await rackRepository.GetRack(id);

            //check if rack with the id dont exist in the database
            if (rack == null)
            {
                return NotFound();
            }

            // converting rack object to json result
            var rackResource = mapper.Map<Rack, RackResource>(rack);

            return Ok(rackResource);
        }

        // POST: api/racks/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateRack([FromBody] RackResource rackResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map rackResource json into rack model
            var rack = mapper.Map<RackResource, Rack>(rackResource);

            //add room for rack
            rack.Room = await roomRepository.GetRoom(rackResource.RoomId, true);

            //add rack into database
            rackRepository.AddRack(rack);
            await unitOfWork.Complete();

            //get rack for converting to json result
            rack = await rackRepository.GetRack(rack.RackId);
            var result = mapper.Map<Rack, RackResource>(rack);

            return Ok(result);
        }

        // PUT: api/racks/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Updaterack(int id, [FromBody]RackResource rackResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rack = await rackRepository.GetRack(id);

            //check if rack with the id dont exist in the database
            if (rack == null)
            {
                return NotFound();
            }
            //map rackResource json into rack model
            mapper.Map<RackResource, Rack>(rackResource, rack);

            //edit room for rack
            rack.Room = await roomRepository.GetRoom(rackResource.RoomId, true);

            await unitOfWork.Complete();

            // converting rack object to json result
            var result = mapper.Map<Rack, RackResource>(rack);
            return Ok(result);
        }

        // DELETE: api/racks/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Deleterack(int id)
        {
            var rack = await rackRepository.GetRack(id, includeRelated: false);

            //check if rack with the id dont exist in the database
            if (rack == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of rack into true
            rackRepository.RemoveRack(rack);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
