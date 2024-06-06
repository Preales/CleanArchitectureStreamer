using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories;

public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
{
    public StreamerRepository(StreamerDbContext context) : base(context)
    {
    }
}

public class VideoRepository : RepositoryBase<Video>, IVideoRepository
{
    public VideoRepository(StreamerDbContext context) : base(context)
    {
    }

    public async Task<Video> GetVideoByNombre(string nombre)
        => await _context.Videos!.Where(w => w.Nombre == nombre).SingleOrDefaultAsync();

    public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
        => await _context.Videos!.Where(w => w.CreatedBy == username).ToListAsync();
}