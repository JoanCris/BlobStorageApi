namespace BlobStorageApi.Models
{
  public class BlobInf
  {
    public Stream content { get; set; }
    public string contentType { get; set; }

    public BlobInf(Stream content, string contentType)
    {
			this.content = content;
			this.contentType = contentType;
    }
  }
}
