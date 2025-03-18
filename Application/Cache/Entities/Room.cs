namespace Application.Cache.Entities;

public class Room 
{
    public string Name { get; set; } = string.Empty;
    public long BuildingId { get; set; }
}


// /get-sem-rooms - получить список аудиторий
// Формат данных:
// [
//   ...
//     {'Name': number,         // Номер аудитории
//      'BuildingId': building_id,  // Идентификатор здания
//      'ItemId': gr_id         // Идентификатор аудитории
//   ...
// ]


// Пример 
// [
//     {
//         "ItemId": 1,
//         "Name": "23-12",
//         "BuildingId": 2
//     },
//     {
//         "ItemId": 2,
//         "Name": "23-09",
//         "BuildingId": 2
//     },
//  ]