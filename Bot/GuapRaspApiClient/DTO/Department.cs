using System.Text.Json.Serialization;

namespace Bot.GuapRaspApiClient.DTO;

public record Department()
{
    [JsonPropertyName("ItemId")] public int ItemId;
    [JsonPropertyName("Name")] public string Name;
}

// Пример:
// [
//     {
//         "ItemId": 38,
//         "Name": "1"
//     },
//     {
//         "ItemId": 37,
//         "Name": "13"
//     },
// ]