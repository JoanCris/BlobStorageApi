using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlobStorageApi.Models;
using BlobStorageApi.Services;

namespace BlobStorageApi.Controllers
{
  [Route("blobs")]
  [ApiController]
  public class BlobAPIController : ControllerBase
  {
		private readonly IBlobService blobService;

    public BlobAPIController(IBlobService blobService)
    {
			this.blobService = blobService;
    }

		[HttpGet("{blobName}")]
		public async Task<IActionResult> get(string blobName)
		{
			var data = await blobService.getBlobAsync(blobName);
			return File(data.content, data.contentType);
		}

		[HttpGet("list")]
		public async Task<IActionResult> get()
    {
			return Ok(await blobService.listBlobsAsync());
    }

		[HttpPost("uploadFile")]
		public async Task<IActionResult> uploadFile([FromBody] UploadFileRequest request)
    {
			await blobService.uploadFileBlobAsync(request.filePath, request.fileName);
			return Ok();
    }

    [HttpPost("uploadContent")]
    public async Task<IActionResult> uploadContent([FromBody] UploadContentRequest request)
    {
      await blobService.uploadContentBlobAsync(request.content, request.fileName);
      return Ok();
    }

		[HttpDelete("{blobName}")]
		public async Task<IActionResult> deleteFile(string blobName)
    {
			await blobService.deleteBlobAsync(blobName);
			return Ok();
    }
  }
}
