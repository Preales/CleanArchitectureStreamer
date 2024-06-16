using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infraestructure.Persistence;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
    {
        if (!context.Streaners!.Any())
        {
            context.Streaners!.AddRange(GetPreconfigurationStreamer());
            await context.SaveChangesAsync();
            logger.LogInformation("Seed {context}", typeof(StreamerDbContext).Name);
        }
    }

    private static IEnumerable<Streamer> GetPreconfigurationStreamer()
        => new List<Streamer>()
            {
                new Streamer{Nombre ="Maxi HOB", Url="http://www.hob.com", CreatedBy="Migration" },
                new Streamer{Nombre ="Disny Minus", Url="http://www.disny.com", CreatedBy="Migration" }
            };
}