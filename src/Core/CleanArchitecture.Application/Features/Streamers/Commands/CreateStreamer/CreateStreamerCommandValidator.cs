using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
{
    public CreateStreamerCommandValidator()
    {
        RuleFor(r => r.Nombre)
            .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
            .NotNull()
            .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

        RuleFor(r => r.Url)
            .NotEmpty().WithMessage("La {Url} no puede estar en blanco");
    }
}
