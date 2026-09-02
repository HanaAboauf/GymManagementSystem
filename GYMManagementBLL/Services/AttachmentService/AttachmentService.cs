using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace GYMManagementBLL.Services.AttachmentService
{
    /*IWebHostEnvironment: interface is used to get the web root path of the application, which is the root folder where
    the application is hosted. This is useful for saving uploaded files to a specific location within the application. 
    Also help me know which environment the application is running in.*/
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
                //1.  check extension 
                var extension= Path.GetExtension(file.FileName).ToLower();
                if(extension is null || folderName is null || extension.Length ==0) return null;
                if(!!allawedExtension.Contains(extension)) return null;

                //2. check file size
                if(file.Length > maxLenght) return null;

                //3. Get located Path
                var folderPath = Path.Combine(_WebHost.WebRootPath, "css/images", folderName);

                if(!File.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //4. Make attachment unique name
                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                //5. Get full path
                var filePath = Path.Combine(folderPath, uniqueFileName);
                //6. create file stream and copy file to it
                using var fileStream= new FileStream(filePath, FileMode.Create);
                //7. copy file to file stream
                file.CopyTo(fileStream);
                return uniqueFileName;


            }
            catch ( Exception ex)
            {
                Console.WriteLine($"failed to upload file {ex.Message}");
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
