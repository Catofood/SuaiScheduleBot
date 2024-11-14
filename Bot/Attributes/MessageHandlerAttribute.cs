
[AttributeUsage(AttributeTargets.Method)]
public class MessageHandlerAttribute : Attribute
{
	public string Text { get; set; }
	public MessageHandlerAttribute(string text)
	{
		Text = text;
	}
}