using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public record DeleteStreamerCommand(int Id) : IRequest;