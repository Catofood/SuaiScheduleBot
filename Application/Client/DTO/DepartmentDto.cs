using Newtonsoft.Json;

namespace Application.Client.DTO;

public class DepartmentDto
{
    [JsonProperty("ItemId")] 
    public long ItemId;

    [JsonProperty("Name")] 
    public string Name;
}

// /get-sem-depts - получить список кафедр
// Формат данных:
// [
//   ...
//     {'Name': dept_num,      // Номер кафедры
//      'ItemId': dept_id      // Идентификатор кафедры
//   ...
// ]


// Пример ответа от API на запрос /get-sem-rooms
// Формат данных:
// [
//   {
//     "ItemId": 38,    // Идентификатор аудитории
//     "Name": "1"      // Номер аудитории
//   },
//   {
//     "ItemId": 37,    // Идентификатор аудитории
//     "Name": "13"     // Номер аудитории
//   }
// ]