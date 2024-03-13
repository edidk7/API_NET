using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace ApiAportesTerminales.Code
{
    public class FileUploadService
    {
        CloudStorageAccount cloudStorageAccount;
        CloudBlobClient cloudBlobClient;
        CloudBlobContainer cloudBlobContainer;



        static string account = ConfigurationManager.AppSettings["StorageAccountName"];
        static string key = ConfigurationManager.AppSettings["StorageAccountKey"];
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            return CloudStorageAccount.Parse(connectionString);
        }



        public FileUploadService(string containerName = "aportesterminales")
        {
            cloudStorageAccount = GetConnectionString();
            cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
        }

        //SE DEBE MEJORAR LA REUTILIZACIÓN DEL OBJETO
        public async Task<string> UploadFileAsyncExcel(Stream fileToUpload, string containerFolder, string filename, string contentType)
        {
            string fileFullPath = null;
            if (fileToUpload == null || fileToUpload.Length == 0)
            {
                return null;
            }
            try
            {
                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                    );
                }
                string fileName = (!string.IsNullOrEmpty(containerFolder) ? containerFolder + "/" : "") + filename;
                //string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(imageToUpload.FileName);



                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                cloudBlockBlob.Properties.ContentType = contentType;
                await cloudBlockBlob.UploadFromStreamAsync(fileToUpload);



                fileFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {



            }
            return fileFullPath;
        }

        public async Task<string> UploadFileAsync(HttpPostedFile fileToUpload, string containerFolder, string filename)

        {

            string fileFullPath = null;

            if (fileToUpload == null || fileToUpload.ContentLength == 0)

            {

                return null;

            }

            try

            {

                //CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();

                //CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                //CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);







                if (await cloudBlobContainer.CreateIfNotExistsAsync())

                {

                    await cloudBlobContainer.SetPermissionsAsync(

                    new BlobContainerPermissions

                    {

                        PublicAccess = BlobContainerPublicAccessType.Blob

                    }

                    );

                }

                string fileName = (!string.IsNullOrEmpty(containerFolder) ? containerFolder + "/" : "") + filename;

                //string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(imageToUpload.FileName);







                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

                cloudBlockBlob.Properties.ContentType = fileToUpload.ContentType;

                await cloudBlockBlob.UploadFromStreamAsync(fileToUpload.InputStream);







                fileFullPath = cloudBlockBlob.Uri.ToString();

            }

            catch (Exception ex)

            {







            }

            return fileFullPath;

        }
    }
}