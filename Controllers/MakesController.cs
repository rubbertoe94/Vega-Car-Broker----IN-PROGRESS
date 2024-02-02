using System.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Models.ViewModels;
using vega.Persistence;

namespace vega.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeViewModel>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<MakeViewModel>>(makes);
        }
    }
}