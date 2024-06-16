using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;

public class CreateDirectorValidator : AbstractValidator<CreateDirectorCommand>
{
    public CreateDirectorValidator()
    {
        RuleFor(r => r.Nombre)
        .NotEmpty().WithMessage("{Nombre} no puede ser nulo");

        RuleFor(r => r.Apellido)
        .NotEmpty().WithMessage("{Apellido} no puede ser nulo");
    }
}