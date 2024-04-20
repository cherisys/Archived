using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Global;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private IRepository<Participant> Repo;

        public ParticipantsController(IRepository<Participant> _repo)
        {
            Repo = _repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var QuestionList = await Repo.GetAll();
            return Ok(QuestionList);
        }

        [HttpGet("{id}", Name = "GetParticipant")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await Repo.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Participant item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            await Repo.Create(item);
            return CreatedAtRoute("GetParticipant", new { Controller = "Participants", id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Participant item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            await Repo.Update(item);
            return NoContent();
        }
    }
}