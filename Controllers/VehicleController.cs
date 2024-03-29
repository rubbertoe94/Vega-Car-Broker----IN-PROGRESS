﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using vega.Core.Interfaces;
using vega.Models;
using vega.Models.ViewModels;
using vega.Pages.Persistence.Interfaces;
using vega.Core;
using vega.Core.Models.ViewModels;
using vega.Core.Models;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleViewModel newVehicle) 
        { 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var vehicle = mapper.Map<SaveVehicleViewModel, Vehicle>(newVehicle);
            vehicle.LastUpdated = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleViewModel>(vehicle!);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleViewModel data) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await repository.GetVehicle(id);

            if (vehicle != null) {
                try {
                    mapper.Map(data, vehicle);
                    vehicle.LastUpdated = DateTime.Now;

                    repository.Update(vehicle);
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


        [HttpGet("allVehicles")]
        public async Task<IActionResult> GetAllVehicles(VehicleQueryViewModel filterResource) 
        {
            var filter = mapper.Map<VehicleQueryViewModel, VehicleQuery>(filterResource);

            var (vehicles, totalCount) = await repository.GetAllVehicles(filter);

            var result = new 
            {
                page = filter.Page,
                pageSize = filter.PageSize,
                totalItems = totalCount,
                totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                vehicles = mapper.Map<IEnumerable<VehicleViewModel>>(vehicles)
            };
            return Ok(result);
        }


        [HttpGet("features")]
        public async Task<IActionResult> GetAllFeatures()
        {
            var features = await repository.GetAllFeatures();
            return Ok(features);
        }
    }
}
