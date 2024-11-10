namespace ClassLibrary;
using System;
public static class FileDownloader
{

    public static async Task DownloadFileAsync(string url, string filePath)
    {
        Console.WriteLine($"Начинаю загрузку файла с {url} {filePath}");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());

        using HttpClient client = new HttpClient();
        try
        {
            byte[] fileBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(filePath, fileBytes);
            Console.WriteLine($"Файл успешно загружен по пути: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }
    
}
