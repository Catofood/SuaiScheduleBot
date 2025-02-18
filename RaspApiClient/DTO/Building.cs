using System.Text.Json.Serialization;

namespace RaspApiClient.DTO;

public class Building
{
    [JsonPropertyName("Name")]
    string Name { get; init; }
    
    [JsonPropertyName("Title")]
    string Title { get; init; }
    
    [JsonPropertyName("ItemId")]
    int ItemId { get; init; }
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