using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace GYMManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allawedExtension = { ".jpg", ".jpeg", ".png" };

        private readonly long maxLenght= 5*1024*1024;
        private readonly IWebHostEnvironment _WebHost;

        public AttachmentService(IWebHostEnvironment webHost)
        {
            _WebHost = webHost;
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(folderName)|| file is null || file.Length==0) return null;

                if(file.Length > maxLenght) return null;

                var extension = Path.GetExtension(file.FileName).ToLower();

                if(!allawedExtension.Contains(extension)) return null;

                var folderPath = Path.Combine(_WebHost.WebRootPath, "css/images", folderName);
                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;

            }
            catch (Exception ex) {

                Console.WriteLine($"failed to upload file {ex}");
                return null;
            }
        }

        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folderName) || string.IsNullOrWhiteSpace(fileName)) return false;

                var filePath = Path.Combine(_WebHost.WebRootPath, "css/images", folderName, fileName);

                if (!File.Exists(filePath)) return false;

                File.Delete(filePath);

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to delete file {ex}");
                return false;
            }

            }
    }
}
