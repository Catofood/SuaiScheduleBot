using Newtonsoft.Json;

namespace Application.Client.DTO;

public class Group
{
    [JsonProperty("Name")] 
    public string Name;
    [JsonProperty("ItemId")] 
    public long ItemId;
}


// Пример:
//
// [
//     {
//         "Name": "8114Кв",
//         "ItemId": 1
//     },
//     {
//         "Name": "В1441",
//         "ItemId": 2
//     },
// ]