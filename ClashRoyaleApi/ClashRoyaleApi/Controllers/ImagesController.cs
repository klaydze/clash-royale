using System.Threading.Tasks;
using ClashRoyaleApi.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ClashRoyaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AzureBlobStorageSettings _azureBlobStorageSettings;
        private readonly CloudStorageAccount _storageAccount;

        public ImagesController(IOptions<AzureBlobStorageSettings> azureBlobStorageSettings)
        {
            _azureBlobStorageSettings = azureBlobStorageSettings.Value;
            _storageAccount = CloudStorageAccount.Parse(_azureBlobStorageSettings.ImageConnectionString);
        }

        [HttpGet("cards/{idName}", Name = nameof(GetCardImages))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCardImages(string idName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(_azureBlobStorageSettings.ContainerName);
            CloudBlobDirectory directory = cloudBlobContainer.GetDirectoryReference($"{_azureBlobStorageSettings.DirReference}/cards");

            var imgBlob = directory.GetBlobReference($"{idName}.png");

            if (await imgBlob.ExistsAsync())
            {
                var imgLink = imgBlob.Uri.ToString();

                return Ok(imgLink);
            }

            return NotFound(new ApiError($"Image not found!"));
        }

        [HttpGet("arenas/{idName}", Name = nameof(GetArenaImages))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetArenaImages(string idName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(_azureBlobStorageSettings.ContainerName);
            CloudBlobDirectory directory = cloudBlobContainer.GetDirectoryReference($"{_azureBlobStorageSettings.DirReference}/arenas");

            var imgBlob = directory.GetBlobReference($"{idName}.png");
            
            if (await imgBlob.ExistsAsync())
            {
                var imgLink = imgBlob.Uri.ToString();

                return Ok(imgLink);
            }

            return NotFound(new ApiError($"Image not found!"));
        }

        [HttpGet("chests/{idName}", Name = nameof(GetChestImages))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetChestImages(string idName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(_azureBlobStorageSettings.ContainerName);
            CloudBlobDirectory directory = cloudBlobContainer.GetDirectoryReference($"{_azureBlobStorageSettings.DirReference}/chests");

            var imgBlob = directory.GetBlobReference($"{idName}.png");

            if (await imgBlob.ExistsAsync())
            {
                var imgLink = imgBlob.Uri.ToString();

                return Ok(imgLink);
            }

            return NotFound(new ApiError($"Image not found!"));
        }

        [HttpGet("leagues/{idName}", Name = nameof(GetLeagueImages))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLeagueImages(string idName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(_azureBlobStorageSettings.ContainerName);
            CloudBlobDirectory directory = cloudBlobContainer.GetDirectoryReference($"{_azureBlobStorageSettings.DirReference}/leagues");

            var imgBlob = directory.GetBlobReference($"{idName}.png");

            if (await imgBlob.ExistsAsync())
            {
                var imgLink = imgBlob.Uri.ToString();

                return Ok(imgLink);
            }

            return NotFound(new ApiError($"Image not found!"));
        }
    }
}