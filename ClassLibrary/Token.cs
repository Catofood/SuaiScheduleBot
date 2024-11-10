namespace ClassLibrary;

public static class Token
{
    public static string GetToken() => File.ReadAllText("C:\\Users\\Catofood\\Desktop\\token.txt");
}