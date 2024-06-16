using MediatR;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;

public record CreateDirectorCommand(string Nombre, string Apellido, int VideoId) : IRequest<int>;
