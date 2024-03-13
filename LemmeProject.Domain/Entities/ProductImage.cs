

namespace LemmeProject.Domain.Entities
{ 
    public class ProductImage : BaseEntity
    {

        public string FileName { get; set; }
        public string FilePath { get; set; } 
        //Relations
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}