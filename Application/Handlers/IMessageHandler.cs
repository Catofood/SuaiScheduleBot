using Telegram.Bot.Types;

namespace Application.Handlers;

public interface IMessageHandler
{
    public Task HandleMessage(Message message);
}