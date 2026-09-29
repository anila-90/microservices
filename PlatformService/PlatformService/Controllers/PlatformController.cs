
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using platformservice.repository;

namespace platformservice.controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        public PlatformController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [Route("GetPlatforms")]
        [HttpGet]
        public IActionResult GetPlatforms()
        {
            //throw new Exception("Custom test error");
            var platforms = _repo.GetPlatforms();
            var platformResponse = _mapper.Map<IEnumerable<platformservice.viewmodels.PlatformResponse>>(platforms);
            return Ok(platformResponse);
        }

        [Route("CreatePlatform")]
        [HttpPost]
        public IActionResult CreatePlatform(platformservice.viewmodels.PlatformRequest platformRequest)
        {
            var platformModel = _mapper.Map<platformservice.models.platform>(platformRequest);
            var result = _repo.AddPlatform(platformModel);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("GetPlatformById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            var platform= await _repo.GetByIdAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            var platformResponse=_mapper.Map<platformservice.viewmodels.PlatformResponse>(platform);
            return Ok(platformResponse);
        }
    }


}