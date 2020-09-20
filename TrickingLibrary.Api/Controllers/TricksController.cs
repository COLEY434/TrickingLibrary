using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Models;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController : ControllerBase
    {
        private readonly TrickStore _trickStore;

        public TricksController(TrickStore trickStore)
        {
            _trickStore = trickStore;
        }

        //api/tricks
        [HttpGet]
        public IActionResult All() => Ok(_trickStore.GetAllTricks());

        //api/tricks/{Id}
        [HttpGet("{Id}")]
        public IActionResult Get(int Id) => Ok(_trickStore.GetAllTricks().FirstOrDefault(x => x.Id.Equals(Id)));

        //api/tricks
        [HttpPost]
        public IActionResult Create([FromBody] Tricks trick)
        {
            _trickStore.AddTricks(trick);
            return Ok();
        }

        //api/tricks
        [HttpPut]
        public IActionResult Update([FromBody] Tricks trick)
        {
            throw new NotImplementedException();
        }

        //api/tricks/{Id}
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}