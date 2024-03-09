namespace LemmeProject.Domain.Entities
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string Adress { get; set; }

        public List<Product> Products { get; set;}
    }
}
