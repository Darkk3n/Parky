using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.Models;
using Parky.Models.Dtos;
using Parky.Models.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks() => Ok(parkRepo.GetNationalParks().Select(r => mapper.Map<NationalParkDto>(r)));

        [HttpGet("{parkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int parkId)
        {
            var park = parkRepo.GetNationalPark(parkId);
            if (park == null)
            {
                return NotFound();
            }
            var parkDto = mapper.Map<NationalParkDto>(park);
            return Ok(parkDto);
        }

        [HttpPost(Name = "CreatePark")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreatePark([FromBody] NationalParkDto parkDto)
        {
            if (parkDto == null)
            {
                return BadRequest(ModelState);
            }
            if (parkRepo.NationalParkExists(parkDto.Name))
            {
                ModelState.AddModelError(string.Empty, "National Park Exists!");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }

            var parkToCreate = mapper.Map<NationalPark>(parkDto);

            if (!parkRepo.CreateNationalPark(parkToCreate))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when creating Park: {parkDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { parkId = parkToCreate.Id }, parkToCreate);
        }

        [HttpPatch("{parkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int parkId, [FromBody] NationalParkDto parkDto)
        {
            if (parkDto == null || parkId != parkDto.Id)
            {
                return BadRequest(ModelState);
            }
            var parkToUpdate = mapper.Map<NationalPark>(parkDto);
            if (!parkRepo.UpdateNationalPark(parkToUpdate))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when updating Park: {parkDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{parkId:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int parkId)
        {
            if (!parkRepo.NationalParkExists(parkId))
            {
                return NotFound();
            }
            if (!parkRepo.DeleteNationalPark(parkId))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when deleting Park with Id: {parkId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
    }
}