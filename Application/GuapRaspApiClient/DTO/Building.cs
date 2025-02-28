using System.Text.Json.Serialization;

namespace Application.GuapRaspApiClient.DTO;

public class Building
{
    [JsonPropertyName("Name")] private string Name;
    [JsonPropertyName("Title")] private string Title;
    [JsonPropertyName("ItemId")] private int ItemId;
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