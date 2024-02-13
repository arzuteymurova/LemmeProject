namespace LemmeProject.Application.Helpers
{
    public interface IFileService
    {
        string SavePhotoToFtp(byte[] imageBytes, string name);
        byte[] GetPhoto(string fileNameFromDb);

    }
}
