using MediatR;

namespace Application.Commands;

public record UpdateCacheCommand() : IRequest;