using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Models;
using TrickingLibrary.Data;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/difficulties")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public DifficultyController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Difficulty> All() => _dbContext.Difficulties.ToList();

        [HttpGet("id")]
        public Difficulty Get(string Id) =>
            _dbContext.Difficulties
                .FirstOrDefault(x => x.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListDifficultyTricks(string Id)
        {
            return _dbContext.Tricks
                    .Where(x => x.Difficulty.Equals(Id, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
        }
        [HttpPost]
        public async Task<Difficulty> Create([FromBody] Difficulty difficulty)
        {
            difficulty.Id = difficulty.Name.Replace(" ", "-").ToLowerInvariant();
            _dbContext.Add(difficulty);
            await _dbContext.SaveChangesAsync();
            return difficulty;
        }
    }
}
