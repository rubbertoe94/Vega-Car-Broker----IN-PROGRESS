using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Models.ViewModels;
using vega.Persistence;
using vega.ViewModels;

namespace vega.Controllers
{
    public class ModelsController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public ModelsController(VegaDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("/api/models")]
        public async Task<IEnumerable<KeyValuePairViewModel>> GetModels()
        {
            var models = await context.Models.ToListAsync();
            return mapper.Map<List<KeyValuePairViewModel>>(models);
        }
    }
}