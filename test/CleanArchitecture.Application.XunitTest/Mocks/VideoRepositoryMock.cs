using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;

namespace CleanArchitecture.Application.XunitTest.Mocks;

public static class VideoRepositoryMock
{
    public static void AddDataVideoRepository(StreamerDbContext streamerDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var videos = fixture.CreateMany<Video>().ToList();
        videos.Add(fixture.Build<Video>()
                        .With(tr => tr.CreatedBy, "Admin")
                        .Create()
                    );


        streamerDbContextFake.Videos!.AddRange(videos);
        streamerDbContextFake.SaveChanges();

        ///var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);
        ///mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(videos);
        ///return mockRepository;
    }
}