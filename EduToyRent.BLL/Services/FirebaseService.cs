using EduToyRent.BLL.Interfaces;
using EduToyRent.DAL.Interfaces;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Services
{
    public class FirebaseService : IFirebaseService
    {

        private readonly string _bucketName = "edutoyrent.appspot.com";
        private readonly StorageClient _storageClient;

        public FirebaseService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }
        public Task<string> GetImage(string filePath)
        {
            // Return the full public URL of the file in Firebase Storage
            return Task.FromResult($"https://storage.googleapis.com/{_bucketName}/{filePath}");
        }

        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty.");

            // Create a unique filename
            var fileName = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

            using (var stream = new MemoryStream())
            {
                // Copy file content to the memory stream
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset the stream position to the beginning

                // Upload the file to Firebase Storage
                var storageObject = await _storageClient.UploadObjectAsync(
                    _bucketName,
                    fileName,
                    file.ContentType,
                    stream,
                    new UploadObjectOptions
                    {
                        PredefinedAcl = PredefinedObjectAcl.PublicRead // Make the image publicly accessible
                    });

                // Return the public URL to access the uploaded image
                return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
            }
        }
    }
}
