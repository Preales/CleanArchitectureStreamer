using AutoMapper;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.XunitTest.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using Moq;
using Shouldly;

namespace CleanArchitecture.Application.XunitTest.Features.Videos.Queries.GetVideoList;

public class GetVideoListQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;

    public GetVideoListQueryHandlerTest()
    {
        _unitOfWork = UnitOfWorkMock.GetUnitOfWork();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        VideoRepositoryMock.AddDataVideoRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task GetVideoListTest()
    {
        var handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);
        var request = new GetVideosListQuery("Admin");
        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        result.ShouldBeOfType<List<VideoVm>>();
        result.Count.ShouldBe(1);
    }
}