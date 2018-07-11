using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/rooms")]
    //[ApiController]
    public class RoomController : Controller
    {
        private IRoomRepository roomRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public RoomController(IRoomRepository roomRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        // GET: api/rooms/getall
        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<RoomResource>> Getrooms(QueryResource queryResource)
        {
            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the rooms with filter and sorting form of the query
            var queryResult = await roomRepository.GetRooms(query);

            //convert all of rooms into roomResource json
            return mapper.Map<QueryResult<Room>, QueryResultResource<RoomResource>>(queryResult);
        }

        // GET: api/rooms/getroom/5
        [HttpGet]
        [Route("getroom/{id}")]
        public async Task<IActionResult> Getroom(int id)
        {
            //get room for converting to json result
            var room = await roomRepository.GetRoom(id);

            //check if room with the id dont exist in the database
            if (room == null)
            {
                return NotFound();
            }

            // converting room object to json result
            var roomResource = mapper.Map<Room, RoomResource>(room);

            return Ok(roomResource);
        }

        // POST: api/rooms/add
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Createroom([FromBody] RoomResource roomResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map roomResource json into room model
            var room = mapper.Map<RoomResource, Room>(roomResource);

            //add room into database
            roomRepository.AddRoom(room);

            //update fans of room
            await roomRepository.UpdateFans(room, roomResource);

            //update racks of room
            await roomRepository.UpdateRacks(room, roomResource);

            //update sensors of room
            await roomRepository.UpdateSensors(room, roomResource);

            await unitOfWork.Complete();

            //get room for converting to json result
            room = await roomRepository.GetRoom(room.RoomId);
            var result = mapper.Map<Room, RoomResource>(room);

            return Ok(result);
        }

        // PUT: api/rooms/update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Updateroom(int id, [FromBody]RoomResource roomResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = await roomRepository.GetRoom(id);

            //check if room with the id dont exist in the database
            if (room == null)
            {
                return NotFound();
            }

            //map roomResource json into room model
            mapper.Map<RoomResource, Room>(roomResource, room);

            //update fans of room
            await roomRepository.UpdateFans(room, roomResource);

            //update racks of room
            await roomRepository.UpdateRacks(room, roomResource);

            //update sensors of room
            await roomRepository.UpdateSensors(room, roomResource);

            await unitOfWork.Complete();

            // converting room object to json result
            var result = mapper.Map<Room, RoomResource>(room);
            return Ok(result);
        }

        // DELETE: api/rooms/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Deleteroom(int id)
        {
            var room = await roomRepository.GetRoom(id, includeRelated: false);

            //check if room with the id dont exist in the database
            if (room == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of room into true
            roomRepository.RemoveRoom(room);
            await unitOfWork.Complete();

            return Ok(id);
        }
    }
}
