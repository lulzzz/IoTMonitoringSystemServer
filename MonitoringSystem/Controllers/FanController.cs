using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using MonitoringSystem.Resources.SubResources;
using PMS.Hubs;
using System;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Authorize]
    [Route("/api/fans")]
    //[ApiController]
    public class FanController : Controller
    {
        private IFanRepository fanRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IRoomRepository roomRepository;
        private IFanStatusRepository fanStatusRepository;
        private IConfiguration config;
        private IHubContext<MonitoringSystemHub> hubContext;

        public FanController(IFanRepository fanRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IRoomRepository roomRepository, IFanStatusRepository fanStatusRepository, IConfiguration config,
            IHubContext<MonitoringSystemHub> hubContext)
        {
            this.fanRepository = fanRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roomRepository = roomRepository;
            this.fanStatusRepository = fanStatusRepository;
            this.config = config;
            this.hubContext = hubContext;
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
        [Authorize(Roles = "Admin")]
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

            //if room id is undefined in json which post to server
            if (!String.IsNullOrEmpty(fanResource.RoomName))
            {
                fan.Room = await roomRepository.GetRoomByRoomName(fanResource.RoomName);
            }

            //add log
            fanRepository.AddFanLog(fan);

            //add fan into database
            fanRepository.AddFan(fan);
            await unitOfWork.Complete();

            //get fan for converting to json result
            fan = await fanRepository.GetFan(fan.FanId);

            await hubContext.Clients.All.SendAsync("LoadData");

            var result = mapper.Map<Fan, FanResource>(fan);

            return Ok(result);
        }

        // PUT: api/fans/update/5
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateFan(int id, [FromBody]FanResource fanResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fan = await fanRepository.GetFan(id);

            //old fan use for add log
            var oldFan = fan;

            //check if fan with the id dont exist in the database
            if (fan == null)
            {
                return NotFound();
            }
            //map FanResource json into Fan model
            mapper.Map<FanResource, Fan>(fanResource, fan);

            //edit room for fan
            fan.Room = await roomRepository.GetRoom(fanResource.RoomId, true);

            //if room id is undefined in json which post to server
            if (!String.IsNullOrEmpty(fanResource.RoomName) && fanResource.RoomId == null)
            {
                fan.Room = await roomRepository.GetRoomByRoomName(fanResource.RoomName);
            }

            //add log
            fanRepository.UpdateFanLog(oldFan, fan);

            //clear fanStatus
            fanRepository.ClearFanStatus(fan);

            await unitOfWork.Complete();

            await hubContext.Clients.All.SendAsync("LoadData");
            await hubContext.Clients.All.SendAsync("UpdateFan");

            // converting fan object to json result
            var result = mapper.Map<Fan, FanResource>(fan);
            return Ok(result);
        }

        // DELETE: api/fans/delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
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

            await hubContext.Clients.All.SendAsync("LoadData");

            return Ok(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("checkfan")]
        public async Task<IActionResult> CheckFan([FromBody] FanStatusResource fanStatusResource)
        {
            //get fan for converting to json result
            var fan = await fanRepository.GetFanByFanCode(fanStatusResource.FanCode);

            //check if fan with the id dont exist in the database
            if (fan == null)
            {
                return NotFound();
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

            //check fan
            if (!fanRepository.checkFan(fan, fanStatusResource))
            {
                FanWarningEmail(fan);
                await unitOfWork.Complete();
            }

            // converting fan object to json result
            fanStatus = await fanStatusRepository.GetFanStatus(fanStatus.FanStatusId, true);
            var result = mapper.Map<FanStatus, FanStatusResource>(fanStatus);

            return Ok(result);
        }

        public void FanWarningEmail(Fan fan)
        {
            try
            {
                string FromAddress = "damducduy.it@gmail.com";
                string FromAdressTitle = "Email from Monitoring System!";
                //To Address  
                string ToAddress = "duy.dam.k3set@eiu.edu.vn";
                string ToAdressTitle = "Something wrong with your fan!";
                string Subject = "Something wrong with your fan!";
                string BodyContent = "Please check your fan which have code is:"
                + fan.FanCode + " and fan name is:" + fan.FanName + " in " + fan.Room.RoomName;
                //Smtp Server  
                string SmtpServer = this.config["EmailSettings:Server"];
                //Smtp Port Number  
                int SmtpPortNumber = System.Int32.Parse(this.config["EmailSettings:Port"]);

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;

                var builder = new BodyBuilder();
                builder.TextBody = BodyContent;


                // Now we just need to set the message body 
                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication  
                    // Error 5.5.1 Authentication   
                    client.Authenticate(this.config["EmailSettings:Email"], this.config["EmailSettings:Password"]);
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
