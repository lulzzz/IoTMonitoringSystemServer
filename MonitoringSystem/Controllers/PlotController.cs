using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Resources;
using MonitoringSystem.Resources.SubResources;
using System.Threading.Tasks;

namespace MonitoringSystem.Controllers
{
    [Route("/api/plots")]
    //[ApiController]
    public class PlotController : Controller
    {
        private IPlotRepository plotRepository;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public PlotController(IPlotRepository plotRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.plotRepository = plotRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("temperature/getbysensor/{id}")]
        public async Task<IActionResult> GetAllBySensorIdForPlot(int id)
        {

            var plot = await plotRepository.GetAllTemperaturesBySensorIdForPlot(id);

            var plotResource = mapper.Map<Plot, PlotResource>(plot);

            return Ok(plotResource);

        }

        [HttpGet]
        [Route("temperature/getall")]
        public async Task<QueryResultResource<PlotResource>> GetAllTemperatureOfAllSensorForPlot(QueryResource queryResource)
        {

            //convert queryresource json into query object
            var query = mapper.Map<QueryResource, Query>(queryResource);

            //get all the plot with filter and sorting form of the query
            var queryResult = await plotRepository.GetAllTemperatureOfAllSensorForPlot(query);

            //convert all of sensor into sensorResource json
            return mapper.Map<QueryResult<Plot>, QueryResultResource<PlotResource>>(queryResult);
        }
    }
}