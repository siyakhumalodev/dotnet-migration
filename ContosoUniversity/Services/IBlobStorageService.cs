using System.IO;
using System.Threading.Tasks;

namespace ContosoUniversity.Services
{
    /// <summary>
    /// Abstraction for blob storage operations, replacing local file system I/O.
    /// </summary>
    public interface IBlobStorageService
    {
        /// <summary>
        /// Uploads a file to blob storage and returns the public URL of the uploaded blob.
        /// </summary>
        /// <param name="stream">The file content stream.</param>
        /// <param name="blobName">The name/path of the blob within the container.</param>
        /// <param name="contentType">The MIME content type of the file.</param>
        /// <returns>The public URL of the uploaded blob.</returns>
        Task<string> UploadAsync(Stream stream, string blobName, string contentType);

        /// <summary>
        /// Deletes a blob from storage by its name or URL.
        /// </summary>
        /// <param name="blobNameOrUrl">The blob name or full URL to delete.</param>
        /// <returns>True if the blob was deleted, false if it did not exist.</returns>
        Task<bool> DeleteAsync(string blobNameOrUrl);
    }
}
