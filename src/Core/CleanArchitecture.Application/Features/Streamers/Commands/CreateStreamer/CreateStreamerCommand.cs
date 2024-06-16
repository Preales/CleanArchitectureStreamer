using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public record CreateStreamerCommand(string Nombre, string Url) : IRequest<int>;