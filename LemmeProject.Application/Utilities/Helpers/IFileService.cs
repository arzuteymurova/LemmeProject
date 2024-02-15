namespace LemmeProject.Application.Utilities.Helpers
{
    public interface IFileService
    {
        string SavePhotoToFtp(byte[] imageBytes, string name);
        byte[] GetPhoto(string fileNameFromDb);

    }
}
