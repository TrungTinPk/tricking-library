﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.API.Form;
using TrickingLibrary.API.ViewModels;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route("api/tricks")]
    public class Tricks : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public Tricks(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        
        [HttpGet]
        public IEnumerable<object> All() => _ctx.Tricks.Select(TrickViewModels.Projection).ToList();
        
        [HttpGet("{id}")]
        public object Get(string id) => 
            _ctx.Tricks
                .Where(x => x.Id.Equals(id,StringComparison.InvariantCultureIgnoreCase))
                .Select(TrickViewModels.Projection)
                .FirstOrDefault();
        
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionForTrick(string trickId) => 
            _ctx.Submissions
                .Include(x => x.Video)
                .Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

        [HttpPost]
        public async Task<object> Create([FromBody] TrickForm trickForm)
        {
            var trick = new Trick
            {
                Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
                Name = trickForm.Name,
                Description = trickForm.Description,
                Difficulty = trickForm.Difficulty,
                TrickCategories = trickForm.Categories.Select(x => new TrickCategory
                {
                    CategoryId = x
                }).ToList()
            };
            _ctx.Add(trick);
            await _ctx.SaveChangesAsync();
            return TrickViewModels.Create(trick);
        }
        
        [HttpPut]
        public async Task<object> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }
            _ctx.Add(trick);
            await _ctx.SaveChangesAsync();
            return TrickViewModels.Create(trick);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _ctx.Tricks.FirstOrDefault(x => x.Id.Equals(id));
            if (trick == null)
            {
                return NotFound();
            } 
                
            trick.Deleted = true;
            await _ctx.SaveChangesAsync();
            
            return Ok();
        }
    }
}