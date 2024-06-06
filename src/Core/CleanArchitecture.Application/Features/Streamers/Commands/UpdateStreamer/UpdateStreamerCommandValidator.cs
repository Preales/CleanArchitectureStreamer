using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        RuleFor(r => r.Nombre)
           .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
           .NotNull()
           .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

        RuleFor(r => r.Url)
            .NotEmpty().WithMessage("La {Url} no puede estar en blanco");
    }
}