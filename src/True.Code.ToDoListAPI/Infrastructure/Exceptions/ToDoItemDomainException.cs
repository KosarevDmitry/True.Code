namespace True.Code.ToDoListAPI.Infrastructure.Exceptions;

public class ToDoItemDomainException : Exception
{
    public ToDoItemDomainException()
    {
    }

    public ToDoItemDomainException(Exception ex)
        : base(ex.Message + " " + ex.InnerException?.Message)
    {
    }

    public ToDoItemDomainException(string message)
        : base(message)
    {
    }

    public ToDoItemDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}