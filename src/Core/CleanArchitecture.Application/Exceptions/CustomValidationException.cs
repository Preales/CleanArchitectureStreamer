using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions;

public class CustomValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException() : base("Se presentaron uno o mas errores de validacion")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(g => g.PropertyName, g => g.ErrorMessage)
            .ToDictionary(f => f.Key, f => f.ToArray());
    }
}