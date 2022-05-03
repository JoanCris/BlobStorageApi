using BlobStorageApi.Models;

namespace BlobStorageApi.Services
{
  public interface IBlobService
  {
		public Task<BlobInf> getBlobAsync(string name);
		public Task<IEnumerable<string>> listBlobsAsync();
		public Task uploadFileBlobAsync(string filePath, string fileName);
		public Task uploadContentBlobAsync(string content, string fileName);
		public Task deleteBlobAsync(string name);
	}
}
