namespace Application.DB.Entity;

public class Room 
{
    public long Id { get; set; }
    
    public List<Event> Events { get; set; } = new();
    
    public string Name { get; set; } = string.Empty;
    
    public Building? Building { get; set; }
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