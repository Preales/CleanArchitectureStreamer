using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQuery : IRequest<List<VideoVm>>
{
    public string Username { get; set; } = default!;

    public GetVideosListQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}

public class VideoVm
{
    public string? Nombre { get; set; }
    public int StreamerId { get; set; }
}

public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVideosListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<VideoVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
    {
        var videoList = await _unitOfWork.VideoRepository.GetVideoByUsername(request.Username);
        return _mapper.Map<List<VideoVm>>(videoList);
    }
}