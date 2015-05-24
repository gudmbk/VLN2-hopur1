using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace klukk_social.Controllers
{
    public class HelperController : Controller
    {
        public Image resizeImage(Image imgToResize, Size size)
        {

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

			if (sourceWidth < size.Width || sourceHeight < size.Height)
			{
				return imgToResize;
			}

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
			else
                nPercent = nPercentW;
			
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
        public string UploadPicture(HttpPostedFileBase file, string name, string location, Size size)
        {
            if (file != null)
            {
                Image originalImage = System.Drawing.Image.FromStream(file.InputStream, true, true);
                Image resizedImage = resizeImage(originalImage, size);

                string accountName = "klukk";
                string accountKey = "XXdyMdSkYqhdavfImVBJkxafDYq3xzUwrcsFFxovwCGxp30qtd/8S0Rx/a4+7Lx736QA3qD1pwYTe4vQ8CHUWA==";

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer sampleContainer = client.GetContainerReference(location);

                sampleContainer.CreateIfNotExists();
                sampleContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                string type = FileType(file.FileName.Substring(file.FileName.Length - 4));
                string filename = name + type;

                MemoryStream streams = new MemoryStream();
                type = type.ToLower();
                if (type == ".jpg" || type == "jpeg")
                {
                    resizedImage.Save(streams, ImageFormat.Jpeg);
                }
                else if (type == ".png")
                {
                    resizedImage.Save(streams, ImageFormat.Png);
                }
                else
                {
                    resizedImage.Save(streams, ImageFormat.Gif);
                }

                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(filename);
                streams.Position = 0;
                using (Stream file1 = streams)
                {
                    blob.UploadFromStream(file1);
                }

                return "https://klukk.blob.core.windows.net/" + location + "/" + filename;
            }
            return "";
        }
        private string FileType(string ending)
        {
            ending.ToLower();
            if (ending == ".jpg" || ending == "jpeg")
            {
                return ".jpg";
            }
            else if (ending == ".png")
            {
                return ".png";
            }
            else
            {
                return ".gif";
            }
        }
    }
}