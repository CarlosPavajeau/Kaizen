using System;
using System.IO;
using System.Threading.Tasks;
using Kaizen.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDownloadController : ControllerBase
    {
        private const string UPLOADS_FOLDER = "Uploads";

        private readonly IHostEnvironment _hostEnvironment;

        public UploadDownloadController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<FileResponseModel>> Upload(IFormFile file)
        {
            string upload = Path.Combine(_hostEnvironment.ContentRootPath, UPLOADS_FOLDER);
            if (!Directory.Exists(upload))
                Directory.CreateDirectory(upload);

            if (file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(upload, fileName);

                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return Ok(new FileResponseModel { FileName = fileName });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<FileStream>> Download(string fileName, string downloadName)
        {
            string upload = Path.Combine(_hostEnvironment.ContentRootPath, UPLOADS_FOLDER);
            string file = Path.Combine(upload, fileName);

            if (!System.IO.File.Exists(file))
                return NotFound();

            MemoryStream memory = new MemoryStream();
            using FileStream stream = new FileStream(file, FileMode.Open);
            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return File(memory, GetContentType(file), downloadName);
        }

        private string GetContentType(string file)
        {
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
