using Telegram.Bot.Types;

namespace Bot.Handlers;

public interface IMessageHandler
{
    public Task Handle(Message message);
}