namespace CleanArchitecture.API.Errors;

public class CodeErrorException : CodeErrorResponse
{
    public string? Details { get; set; }
    public CodeErrorException(int statusCode, string? message = null, string? deatils = null) : base(statusCode, message)
    {
        Details = deatils;
    }
}
