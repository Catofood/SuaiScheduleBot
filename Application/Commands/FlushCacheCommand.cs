using MediatR;

namespace Application.Commands;

public record FlushCacheCommand() : IRequest;