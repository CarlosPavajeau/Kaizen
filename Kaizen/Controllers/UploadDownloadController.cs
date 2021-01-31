using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Kaizen.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDownloadController : ControllerBase
    {
        private const string UploadsFolder = "Uploads";

        private readonly IHostEnvironment _hostEnvironment;

        public UploadDownloadController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<FileResponseModel>>> Upload()
        {
            Microsoft.AspNetCore.Http.IFormFileCollection files = Request.Form.Files;
            string upload = Path.Combine(_hostEnvironment.ContentRootPath, UploadsFolder);
            if (!Directory.Exists(upload))
            {
                Directory.CreateDirectory(upload);
            }

            List<FileResponseModel> fileNames = new List<FileResponseModel>();
            foreach (Microsoft.AspNetCore.Http.IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(upload, fileName);

                    using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fileStream);

                    fileNames.Add(new FileResponseModel { FileName = fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            return fileNames;
        }

        [HttpGet]
        public async Task<ActionResult<FileStream>> Download(string fileName, string downloadName)
        {
            string upload = Path.Combine(_hostEnvironment.ContentRootPath, UploadsFolder);
            string file = Path.Combine(upload, fileName);

            if (!System.IO.File.Exists(file))
            {
                return NotFound();
            }

            MemoryStream memory = new MemoryStream();
            await using FileStream stream = new FileStream(file, FileMode.Open);
            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return File(memory, GetContentType(file), downloadName);
        }

        private static string GetContentType(string file)
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
