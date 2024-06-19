﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand, Unit>
{
    ///private readonly IStreamerRepository _streamerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteStreamerCommandHandler> _logger;

    public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteStreamerCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
        if (streamerToDelete == null)
        {
            _logger.LogError($"{request.Id} streamer no existe en el sistema");
            throw new NotFoundException(nameof(Streamer), request.Id);
        }

        await _unitOfWork.StreamerRepository.DeleteAsync(streamerToDelete);
        _logger.LogInformation($"El {request.Id} streamer fue eliminado");

        return Unit.Value;
    }
}