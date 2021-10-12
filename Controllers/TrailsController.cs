using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.Models;
using Parky.Models.Dtos;
using Parky.Models.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository trailRepo;
        private readonly IMapper mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            this.trailRepo = trailRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {
            return Ok(trailRepo.GetTrails().Select(r => mapper.Map<TrailDto>(r)));
        }

        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailDto))]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var trail = trailRepo.GetTrail(trailId);
            if (trail == null)
            {
                return NotFound();
            }
            var trailDto = mapper.Map<TrailDto>(trail);
            return Ok(trailDto);
        }

        [HttpPost(Name = "CreateTrail")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError(string.Empty, "Trail already exists!");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }
            var trailToCreate = mapper.Map<Trail>(trailDto);
            if (!trailRepo.CreateTrail(trailToCreate))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when creating Trail: {trailDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trailToCreate.Id }, trailToCreate);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }
            var trailToUpdate = mapper.Map<Trail>(trailDto);
            if (!trailRepo.UpdateTrail(trailToUpdate))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when updated Trail: {trailDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }
            if (!trailRepo.DeleteTrail(trailId))
            {
                ModelState.AddModelError(string.Empty, $"An error ocurred when deleting Trail with Id: {trailId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
    }
}