namespace Application.DB.Entity;

public class Group 
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<User> Users { get; set; } = new();
    public List<Event> Events { get; set; } = new();
}


// /get-sem-groups?ids=<id1_>,<id2_>,<id3_>... - получить список групп
// Аргумент ids - список ИД через запятую
// Формат:
// [
// ...
//      {'Name':gr_num, 'ItemId': gr_id}
// ...
// ]

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