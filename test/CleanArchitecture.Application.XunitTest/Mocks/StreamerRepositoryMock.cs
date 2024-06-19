using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;

namespace CleanArchitecture.Application.XunitTest.Mocks;

public static class StreamerRepositoryMock
{
    public static void AddDataStreamerRepository(StreamerDbContext streamerDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var entitiesFake = fixture.CreateMany<Streamer>().ToList();
        entitiesFake.Add(fixture.Build<Streamer>()
                        .With(tr => tr.Id, 8001)
                        .With(tr => tr.CreatedBy, "Admin")
                        .Without(w => w.Videos)
                        .Create()
                    );


        streamerDbContextFake.Streaners!.AddRange(entitiesFake);
        streamerDbContextFake.SaveChanges();

        ///var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);
        ///mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(videos);
        ///return mockRepository;
    }
}
