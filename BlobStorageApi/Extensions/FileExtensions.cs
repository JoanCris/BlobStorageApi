using Microsoft.AspNetCore.StaticFiles;

namespace BlobStorageApi.Extensions
{
  public static class FileExtensions
  {
    private static readonly FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

		public static string getContentType(this string fileName)
    {
      if (!provider.TryGetContentType(fileName, out var contentType))
      {
				contentType = "application/octect-stream";
      }

			return contentType;
    }
  }
}
