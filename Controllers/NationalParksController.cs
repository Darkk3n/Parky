using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.Models.Dtos;
using Parky.Models.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository parkRepo;
        private readonly IMapper mapper;

        public NationalParksController(INationalParkRepository parkRepo, IMapper mapper)
        {
            this.parkRepo = parkRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks() => Ok(parkRepo.GetNationalParks().Select(r => mapper.Map<NationalParkDto>(r)));
    }
}