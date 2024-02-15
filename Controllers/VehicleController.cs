using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Models.ViewModels;
using vega.Pages.Persistence.Interfaces;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        private readonly IVehicleRepository repository;

        public VehicleController(IMapper mapper, VegaDbContext context, IVehicleRepository repository) 
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleViewModel vehicleViewModel) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var vehicle = mapper.Map<SaveVehicleViewModel, Vehicle>(vehicleViewModel);
            vehicle.LastUpdated = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleViewModel>(vehicle!);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleViewModel data) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await repository.GetVehicle(id);

            if (vehicle != null) {
                try {
                    mapper.Map<SaveVehicleViewModel, Vehicle>(data, vehicle);
                    vehicle.LastUpdated = DateTime.Now;

                    await context.SaveChangesAsync();
                    var result = mapper.Map<Vehicle, VehicleViewModel>(vehicle);
                    return Ok(result);
                }
                catch (Exception ex) {
                    return StatusCode(500, $"an error occured: {ex.Message}");
                }
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);

            if (vehicle == null) 
            {
                return NotFound();
            }

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return Ok(id);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id) 
        {
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var mappedResult = mapper.Map<Vehicle, VehicleViewModel>(vehicle);
            return Ok(mappedResult);
        }

    }
}
