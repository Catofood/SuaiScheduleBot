using System.Text.Json.Serialization;

namespace Bot.DTO;

public class Building
{
    [JsonPropertyName("Name")] private string Name { get; init; }

    [JsonPropertyName("Title")] private string Title { get; init; }

    [JsonPropertyName("ItemId")] private int ItemId { get; init; }
}

// /get-sem-rooms - получить список аудиторий
// Формат:
// [
//   ...
//   {'Name':number, 'BuildingId':building_id, 'ItemId': gr_id}
//   ...
// ]
//

// Пример:
// [
//     {
//         "Name": "Б.М.",
//         "Title": "Б.М.",
//         "ItemId": 11
//     },
//     {
//         "Name": "Б.Морская 67",
//         "Title": "Б.Морская 67",
//         "ItemId": 1
//     },
// ]