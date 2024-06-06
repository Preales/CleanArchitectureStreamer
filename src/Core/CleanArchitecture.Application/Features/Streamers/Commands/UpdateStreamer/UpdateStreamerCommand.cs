using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public record UpdateStreamerCommand(int Id, string Nombre, string Url) : IRequest;
