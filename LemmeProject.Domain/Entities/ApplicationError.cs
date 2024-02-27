namespace LemmeProject.Domain.Entities
{
    public class ApplicationError : BaseEntity
    {
        public string ErrorMessage { get; set; }
        public string ErrorSource { get; set; }
    }
}
