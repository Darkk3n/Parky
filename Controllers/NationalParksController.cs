using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.Models.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/[controller]")]
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
    }
}