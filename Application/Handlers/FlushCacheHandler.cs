using Application.Cache;
using Application.Commands;
using MediatR;

namespace Application.Handlers;

public class FlushCacheHandler : IRequestHandler<FlushCacheCommand>
{
    private readonly CacheRepository _cacheRepository;

    public FlushCacheHandler(CacheRepository cacheRepository)
    {
        _cacheRepository = cacheRepository;
    }

    public async Task Handle(FlushCacheCommand request, CancellationToken cancellationToken)
    {
        await _cacheRepository.FlushDb();
    }
}