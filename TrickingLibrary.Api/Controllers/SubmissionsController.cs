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
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public SubmissionsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //api/tricks
        [HttpGet]
        public IEnumerable<Submission> All() => _dbContext.Submissions.ToList();

        //api/tricks/{Id}
        [HttpGet("{Id}")]
        public Submission Get(int Id) => _dbContext.Submissions.FirstOrDefault(x => x.Id.Equals(Id));

        //api/tricks
        [HttpPost]
        public async Task<Submission> Create([FromBody] Submission submission)
        {
            _dbContext.Add(submission);
            await _dbContext.SaveChangesAsync();
            return submission;
        }

        //api/tricks
        [HttpPut]
        public async Task<Submission> Update([FromBody] Submission submission)
        {
            if (submission.Id == 0)
            {
                return null;
            }

            _dbContext.Add(submission);
            await _dbContext.SaveChangesAsync();
            return submission;
        }

        //api/tricks/{Id}
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var trick = _dbContext.Tricks.FirstOrDefault(x => x.Id.Equals(Id));
            if(trick == null)
            {
                return NotFound();
            }
            trick.Deleted = true;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}