using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Form;
using TrickingLibrary.Api.Models;
using TrickingLibrary.Api.ViewModels;
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
        public IEnumerable<object> All()
        {
            var tricks = _dbContext.Tricks.Select(TrickViewModels.Default).ToList();
            return tricks;
        }

        //api/tricks/{Id}
        [HttpGet("{Id}")]
        public object Get(string Id) => 
            _dbContext.Tricks
                      .Where(x => x.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase))
                      .Select(TrickViewModels.Default)
                      .FirstOrDefault();


        //api/tricks/{Id}/submissions
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId)
        {
           var tricks = _dbContext.Submissions.Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return tricks;
        }
          

        //api/tricks
        [HttpPost]
        public async Task<object> Create([FromBody] TrickForm trickForm)
        {

            var trick = new Trick
            {
                Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
                Name = trickForm.Name,
                Description = trickForm.Description,
                Difficulty = trickForm.Difficulty,
                TrickCategories = trickForm.Categories.Select(x => new TrickCategory { CategoryId = x }).ToList()
            };
            _dbContext.Add(trick);
           await  _dbContext.SaveChangesAsync();
            return TrickViewModels.Default.Compile().Invoke(trick);
        }

        //api/tricks
        [HttpPut]
        public async Task<object> Update([FromBody] Trick trick)
        {
            if(string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }

            _dbContext.Add(trick);
            await _dbContext.SaveChangesAsync();
            return TrickViewModels.Default.Compile().Invoke(trick);
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