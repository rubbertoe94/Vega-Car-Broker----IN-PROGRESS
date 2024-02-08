using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Models.ViewModels;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;

        public VehicleController(IMapper mapper, VegaDbContext context) 
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleViewModel vehicleViewModel) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var vehicle = mapper.Map<VehicleViewModel, Vehicle>(vehicleViewModel);
            vehicle.LastUpdated = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            var result = mapper.Map<Vehicle, VehicleViewModel>(vehicle);
            return Ok(result);
        }
    }
}
