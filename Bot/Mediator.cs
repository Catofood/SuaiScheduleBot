using System.Reflection;
using ClassLibrary;
using Microsoft.VisualBasic;
using Telegram.Bot.Types;

namespace Bot;

public interface IMediator
{
	Task<BotResponse> Send(Update update);
}

public class Mediator : IMediator
{
	public async Task<BotResponse> Send(Update update)
	{
		var instance = GetHandler(update.Message.Text);
		if(instance is null) return new BotResponse(){Message = "I don't understand that command."};
		var method = instance
		.GetType()
		.GetMethods()
		.SingleOrDefault(m => m.GetCustomAttributes<MessageHandlerAttribute>()
		.Where(x => x.Text == update.Message.Text).Any());
		
		var response = method.Invoke(instance, new object[] { update }) as Task<BotResponse>;
		
		return response.Result;
	}
	

	private object GetHandler(string text)
	{
		Type type = Assembly
		.GetExecutingAssembly()
		.GetTypes()
		.Where(t => t.GetCustomAttribute<MessageHandlerClassAttribute>() != null)
		.ToList()
		.Where(t => 
		{
			var a = t.GetMethods(BindingFlags.Instance);
			return t.GetMethods().Where(m => m.GetCustomAttributes<MessageHandlerAttribute>().Where(a => a.Text == text).Any()).Any();
		})
		.SingleOrDefault();
		
		if(type is null) return null;
		
		return Activator.CreateInstance(type); 
	}
}