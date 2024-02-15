namespace LemmeProject.Application.DTOs.Images
{
    public class ProductImageTableResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileBase64 { get; set; }

        //Relations
        public string ProductName { get; set; }
    }
}
