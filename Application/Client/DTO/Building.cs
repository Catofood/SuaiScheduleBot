using Newtonsoft.Json;

namespace Application.Client.DTO;

public class Building 
{
    [JsonProperty("Name")] 
    public string Name;
    [JsonProperty("Title")] 
    public string Title;
    [JsonProperty("ItemId")] 
    public long ItemId;
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