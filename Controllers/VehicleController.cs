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
        
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehicleController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork) 
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
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

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

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
                    mapper.Map<Vehicle>(data);
                    vehicle.LastUpdated = DateTime.Now;

                    await unitOfWork.CompleteAsync();
                    var result = mapper.Map<VehicleViewModel>(vehicle);
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
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null) 
            {
                return NotFound();
            }

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
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
