using LemmeProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Application.Utilities.Helpers
{
    public class FileService : IFileService
    {
        public string SavePhotoToFtp(byte[] imageBytes, string name)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string folderPath = Path.Combine(currentDirectory, "AppImages");
                string guid = Guid.NewGuid().ToString();
                string fileName = $"{name}{guid}.jpeg";

                string filePath = $"{folderPath}/{fileName}";
                File.WriteAllBytes(filePath, imageBytes);
                return fileName;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        public byte[] GetPhoto(string fileNameFromDb)
        {
            byte[] photo = null;
            try
            {
                if (!string.IsNullOrEmpty(fileNameFromDb))
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string folderPath = Path.Combine(currentDirectory, "AppImages");
                    string fullFilePath = Path.Combine(folderPath, fileNameFromDb.ToUpper());
                    photo = File.ReadAllBytes(fullFilePath);
                }
                else
                {
                    // physicalPersonPhoto = NoImage;
                }
                return photo;
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                //physicalPersonPhoto = NoImage;
                return photo;
            }
        }
    }
}
