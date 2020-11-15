using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route("api/difficulties")]
    public class DifficultyController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public DifficultyController(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        
        [HttpGet]
        public IEnumerable<Difficutly> All() => _ctx.Difficutlies.ToList();
        
        [HttpGet("{id}")]
        public Difficutly Get(string id) => 
            _ctx.Difficutlies.FirstOrDefault(x => x.Id.Equals(id,StringComparison.InvariantCultureIgnoreCase));
        
        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListDifficutlyTricks(string id) => 
            _ctx.Tricks
                .Where(x => x.Difficulty.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

        [HttpPost]
        public async Task<Difficutly> Create([FromBody] Difficutly difficutly)
        {
            difficutly.Id = difficutly.Name.Replace(" ", "-").ToLowerInvariant();
            _ctx.Add(difficutly);
            await _ctx.SaveChangesAsync();
            return difficutly;
        }
    }
}