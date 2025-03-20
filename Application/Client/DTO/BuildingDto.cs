using Newtonsoft.Json;

namespace Application.Client.DTO;

public class BuildingDto 
{
    [JsonProperty("Name")] 
    public string Name { get; set; }
    [JsonProperty("Title")] 
    public string Title{ get; set; }
    [JsonProperty("ItemId")] 
    public long ItemId{ get; set; }
}

// /get-sem-builds?ids=<id1_>,<id2_>,<id3_>... - получить список корпусов
// Аргумент ids - список ИД через запятую
// Формат:

//     [
//     ...
//     {'Name':adress, 'Title':adress, 'ItemId': id}
//     ...
//     ]

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