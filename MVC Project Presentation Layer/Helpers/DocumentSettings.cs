using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName) //IFormFile is the form that have the input that the user will upload the file in it
        {
            ///1. get located folder path
            ///string folderPath = $"F:\\Back-End\\Assignments\\MVC\\Demo MVC Project\\MVC Project Presentation Layer\\wwwroot\\Files\\{folderName}";
            ///string folderPath = $"{Directory.GetCurrentDirectory()}wwwroot\\Files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); //check if the directory or subdirectory in the path doesn't exist , it will go and create them 
            ///2. get file name and make it unique
            // string fileName = file.FileName;//gets the file name 
            // fileName = file.Name;//gets the extension name 
            ///make the name unique as we may have another file with same name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            ///3. get file path
            string filePath = Path.Combine(folderPath, fileName);
            ///4. save file as streams [data per time]
            var fileStream = new FileStream(filePath, FileMode.Create);
          await  file.CopyToAsync(fileStream);
            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
