using MediatR;
using Telegram.Bot.Types;

namespace Application.Commands;

public record HandleTextMessageCommand(Message Message) : IRequest;