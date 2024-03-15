

namespace LemmeProject.Domain.Entities
{ 
    public class ProductImage : BaseEntity
    {

        public int Id { get; set; }
        public string ImagePath { get; set; } 
        //Relations
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}