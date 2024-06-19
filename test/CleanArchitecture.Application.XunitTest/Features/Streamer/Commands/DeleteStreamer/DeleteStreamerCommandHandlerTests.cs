using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.XunitTest.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace CleanArchitecture.Application.XunitTest.Features.Streamer.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandlerTests
{
    private readonly Mock<UnitOfWork> _unitOfWork;
    private readonly Mock<ILogger<DeleteStreamerCommandHandler>> _logger;

    public DeleteStreamerCommandHandlerTests()
    {
        _unitOfWork = UnitOfWorkMock.GetUnitOfWork();

        _logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

        StreamerRepositoryMock.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task UpdateStreamerCommand_InputStreamer_ReturnsUnit()
    {
        var streamerInput = new DeleteStreamerCommand(8001);

        var handler = new DeleteStreamerCommandHandler(_unitOfWork.Object, _logger.Object);

        var result = await handler.Handle(streamerInput, CancellationToken.None);

        result.ShouldBeOfType<Unit>();
    }
}