namespace CleanArchitecture.Blazor.Application.Common.ExceptionHandler;
public class ConflictException : ServerException
{
    public ConflictException(string message)
        : base(message, null, System.Net.HttpStatusCode.Conflict)
    {
    }
}
