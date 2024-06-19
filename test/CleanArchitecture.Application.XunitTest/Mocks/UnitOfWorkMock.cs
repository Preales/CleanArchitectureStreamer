using CleanArchitecture.Infraestructure.Persistence;
using CleanArchitecture.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.XunitTest.Mocks
{
    public static class UnitOfWorkMock
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var optionsDb = new DbContextOptionsBuilder<StreamerDbContext>()
            .UseInMemoryDatabase(databaseName: $"{nameof(StreamerDbContext)}-{dbContextId}")
            .Options;

            var streamerDbContextFake = new StreamerDbContext(optionsDb);
            streamerDbContextFake.Database.EnsureDeleted();
            var unitOfWorkMock = new Mock<UnitOfWork>(streamerDbContextFake);


            ///var mockVideoRepository = VideoRepositoryMock.GetVideoRepository();
            ///unitOfWorkMock.Setup(x => x.VideoRepository).Returns(mockVideoRepository.Object);
            return unitOfWorkMock;
        }
    }
}
