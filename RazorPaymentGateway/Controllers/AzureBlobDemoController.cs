using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RazorPaymentGateway.ViewModels;

namespace RazorPaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureBlobDemoController : ControllerBase
    {

        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=demostorage23031999;AccountKey=8/i9eYbNBywB3V6qLorm1GY6fwlGl73OrZglasQR6NeEAOb4Jqo569E4by5tFAdd0KjX91NIa8fK+ASt2b2K/g==;EndpointSuffix=core.windows.net";

        private string containerName = "angularblob";

        [HttpPost]
        [Route("UploadFile")]
        public IActionResult UploadFile()
        {
            IFormFile file = Request.Form.Files[0];
            string fileName = file.FileName;
            Console.WriteLine(file.FileName);
            try
            {

                BlobClient blobClient = new BlobClient(connectionString: this.connectionString, blobContainerName: containerName, blobName: fileName);
                
                blobClient.Upload(file.OpenReadStream());
            }
            catch (Exception e)
            {
                return BadRequest(new { error = "Something went wrong", errorMessage = e.Message });
            }

            return Ok(new { success = "Image uploaded successfully", file = fileName });
        }

        [HttpGet]
        [Route("ViewBlobs")]
        public IActionResult ViewBlobs()
        {
            List<string> blobsList = new List<string>();
            try
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(this.connectionString, this.containerName);
                blobContainerClient.CreateIfNotExists();
                Console.WriteLine("Listing blobs...");
                // List all blobs in the container
                var blobs = blobContainerClient.GetBlobs();
                foreach (BlobItem blobItem in blobs)
                {
                    blobsList.Add(blobItem.Name);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { error = "Something went wrong while fetching blobs", errorMessage = e.Message });
            }

            return Ok(new { blobs = blobsList, success = "Blobs fetched successfully" });
        }

        [HttpPost]
        [Route("DeleteBlob")]
        public IActionResult DeleteBlob(Blob blob)
        {
            try
            {
                BlobClient blobClient = new BlobClient(connectionString: this.connectionString, blobContainerName: containerName, blobName: blob.BlobName);
                blobClient.Delete();
            }
            catch (Exception e)
            {

                return BadRequest(new { error = "Something went wrong while fetching blobs", errorMessage = e.Message });
            }

            return Ok(new { success = "Blobs deleted successfully" });
        }

    }
}