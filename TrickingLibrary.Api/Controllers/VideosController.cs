using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/videos")]
    //[ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public VideosController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet("{video}")]
        public IActionResult GetVideo(string video)
        {
            var savePath = Path.Combine(_env.WebRootPath, video);
            return new FileStreamResult(new FileStream(savePath, FileMode.Open, FileAccess.Read), "video/*");
        }
        public async Task<IActionResult> UploadVideo([FromForm] IFormFile video)
        {
            var mime = video.FileName.Split('.').Last();
            var fileName = string.Concat(Path.GetRandomFileName(), ".", mime);
            var savePAth = Path.Combine(_env.WebRootPath, fileName);

            await using (var fileStream = new FileStream(savePAth, FileMode.Create, FileAccess.Write))
            {
               await video.CopyToAsync(fileStream);
            }
                return Ok(fileName);
        }
    }
}