﻿using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
{
    private readonly IStreamerRepository _streamerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateStreamerCommandHandler> _logger;

    public DeleteStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
    {
        _streamerRepository = streamerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await _streamerRepository.GetByIdAsync(request.Id);
        if (streamerToDelete == null)
        {
            _logger.LogError($"{request.Id} streamer no existe en el sistema");
            throw new NotFoundException(nameof(Streamer),request.Id);
        }

        await _streamerRepository.DeleteAsync(streamerToDelete);
        _logger.LogInformation($"El {request.Id} streamer fue eliminado");
    }
}