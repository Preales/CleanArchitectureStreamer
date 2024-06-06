using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public sealed class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
{
    private readonly IStreamerRepository _streamerRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateStreamerCommandHandler> _logger;

    public CreateStreamerCommandHandler(
        IStreamerRepository streamerRepository,
        IMapper mapper,
        IEmailService emailService,
        ILogger<CreateStreamerCommandHandler> logger)
    {
        _streamerRepository = streamerRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerEntity = _mapper.Map<Streamer>(request);
        var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

        _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exisitosamente");
        await SendEmail(newStreamer);
        return newStreamer.Id;
    }

    private async Task SendEmail(Streamer streamer)
    {
        var email = new Email
        {
            To = "realespriscy@gamil.com",
            Body = $"La compañia {streamer.Nombre} se ha creado exitosamente",
            Subject = "Informacion"
        };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception)
        {
            _logger.LogError($"Error al enviar el email de {streamer.Id}");
        }
    }
}