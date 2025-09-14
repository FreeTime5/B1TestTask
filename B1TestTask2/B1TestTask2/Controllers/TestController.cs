using B1TestTask2.Services.Services.FileLoader;
using B1TestTask2.Services.Services.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace B1TestTask2.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IFileLoader fileLoader;
        private readonly IFileManager fileManager;

        public TestController(IFileLoader fileLoader,
            IFileManager fileManager)
        {
            this.fileLoader = fileLoader;
            this.fileManager = fileManager;
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IFormFile file)
        {
            await fileLoader.LoadFileAsync(file.FileName, file.OpenReadStream());
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFilesAsync(CancellationToken cancellationToken)
        {
            var names = await fileManager.GetAllFilesAsync(cancellationToken);
            return Ok(names);
        }

        [HttpGet]
        [Route("{fileId}")]
        public async Task<IActionResult> GetFileInfoAsync([FromRoute] int fileId, CancellationToken cancellationToken)
        {
            var fileInformation = await fileManager.GetFileInfoAsync(fileId, cancellationToken);
            return Ok(fileInformation);
        }
    }
}
