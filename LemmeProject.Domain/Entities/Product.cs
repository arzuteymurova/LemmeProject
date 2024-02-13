namespace LemmeProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }

        //Relations
        public List<ProductImage> Images { get; set; }
        public List<ProductSearchHistory> ProductSearchHistory { get; set; }
    }
}
