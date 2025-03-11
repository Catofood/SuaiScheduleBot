namespace Application;

public class Time
{
    void lel()
    {
        var unixTimeMoscow = DateTimeOffset.UtcNow
            .ToOffset(TimeSpan.FromHours(3))
            .ToUnixTimeSeconds();
        Console.WriteLine(unixTimeMoscow);  
    }
}