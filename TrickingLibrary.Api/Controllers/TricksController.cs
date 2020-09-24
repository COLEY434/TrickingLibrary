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
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TricksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //api/tricks
        [HttpGet]
        public IEnumerable<Trick> All() => _dbContext.Tricks.ToList();

        //api/tricks/{Id}
        [HttpGet("{Id}")]
        public Trick Get(string Id) => _dbContext.Tricks.FirstOrDefault(x => x.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase));

        //api/tricks/{Id}/submissions
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId)
        {
           var tricks = _dbContext.Submissions.Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return tricks;
        }
          

        //api/tricks
        [HttpPost]
        public async Task<Trick> Create([FromBody] Trick trick)
        {
            trick.Id = trick.Name.Replace(" ", "-").ToLowerInvariant();
            _dbContext.Add(trick);
           await  _dbContext.SaveChangesAsync();
            return trick;
        }

        //api/tricks
        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if(string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }

            _dbContext.Add(trick);
            await _dbContext.SaveChangesAsync();
            return trick;
        }

        //api/tricks/{Id}
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var trick = _dbContext.Tricks.FirstOrDefault(x => x.Id.Equals(Id));
            trick.Deleted = true;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}