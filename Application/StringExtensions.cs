namespace Application;

public static class StringExtensions
{
    public static string ToLowerRussian(this string input)
    {
        return input.ToLower()
            .Replace('c', 'с')
            .Replace('s', 'с')
            .Replace('v', 'в')
            .Replace('r', 'р')
            .Replace('m', 'м')
            .Replace('k', 'к')
            .Replace('a', 'а')
            .Replace('e', 'е')
            .Replace('o', 'о')
            .Replace('p', 'р')
            .Replace('x', 'ч')
            .Replace('b', 'в')
            .Replace('u', 'у')
            .Replace('y', 'у')
            .Replace('n', 'н')
            .Replace('i', 'и')
            .Replace('d', 'д')
            .Replace('l', 'л')
            .Replace('t', 'т')
            .Replace('j', 'ж')
            .Replace('z', 'з')
            .Replace('w', 'в')
            .Replace('h', 'х')
            .Replace('g', 'г')
            .Replace('f', 'ф')
            .Replace('q', 'к')
            .Replace('z', 'з');
    }

}