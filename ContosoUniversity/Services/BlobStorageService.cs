using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace ContosoUniversity.Services
{
    /// <summary>
    /// Azure Blob Storage implementation of IBlobStorageService.
    /// Replaces local file system I/O with cloud-based blob storage.
    /// </summary>
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"] ?? "teaching-materials";

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Azure Blob Storage connection string is not configured. " +
                    "Set 'AzureBlobStorage:ConnectionString' in appsettings.json or environment variables.");
            }

            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        /// <inheritdoc />
        public async Task<string> UploadAsync(Stream stream, string blobName, string contentType)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (string.IsNullOrEmpty(blobName))
                throw new ArgumentNullException(nameof(blobName));

            var blobClient = _containerClient.GetBlobClient(blobName);

            var headers = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = headers
            });

            return blobClient.Uri.ToString();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(string blobNameOrUrl)
        {
            if (string.IsNullOrEmpty(blobNameOrUrl))
                return false;

            // Extract blob name from URL if a full URL is provided
            var blobName = ExtractBlobName(blobNameOrUrl);
            var blobClient = _containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

        /// <summary>
        /// Extracts the blob name from a full URL or returns the input if already a blob name.
        /// </summary>
        private string ExtractBlobName(string blobNameOrUrl)
        {
            if (Uri.TryCreate(blobNameOrUrl, UriKind.Absolute, out var uri))
            {
                // The blob name is the path after the container name segment
                // e.g., https://account.blob.core.windows.net/container/path/to/blob
                var segments = uri.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length > 1)
                {
                    // Skip the container name (first segment) and join the rest
                    return string.Join("/", segments, 1, segments.Length - 1);
                }
            }

            // Already a blob name or relative path — strip leading slash if present
            return blobNameOrUrl.TrimStart('/');
        }
    }
}
