namespace LemmeProject.Application.Utilities.Results.Abstract
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; set; }

    }
}
