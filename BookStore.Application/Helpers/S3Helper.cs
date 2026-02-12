using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace BookStore.Application.Helpers
{
    public static class S3Helper
    {
        public static async Task<(int, string)> UploadFileAsync(string image, S3 s3)
        {
            try
            {
                var client = new AmazonS3Client(s3.AccessKey, s3.Secret, RegionEndpoint.USEast1);
                var fileTransferUtility = new TransferUtility(client);
                var hash = EncryptionUtil.ChangeUrlSpecialCharset(EncryptionUtil.Encrypt(DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")));
                var random_string = StringHelper.GenerateRandomString(10);
                var fileName = $"{hash}{random_string}.png";

                using (FileStream fs = new FileStream("transiction.png", FileMode.Create))
                {
                    var data = Convert.FromBase64String(image);
                    fs.Write(data);

                    await fileTransferUtility.UploadAsync(fs, s3.BucketName, "images/" + fileName);
                }

                return (200, fileName);
            }
            catch (AmazonS3Exception e)
            {
                return (500, e.Message);
            }
            catch (Exception e)
            {
                return (500, e.Message);
            }
        }
    }
}