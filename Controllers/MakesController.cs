using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<MakeViewModel> GetMakes()
        {
            var makes =  context.Makes
                .Include(m => m.Models)
                .ToList();

            Console.WriteLine("makes object on server: ", makes);

            return mapper.Map<IEnumerable<MakeViewModel>>(makes);
        }
    }
}