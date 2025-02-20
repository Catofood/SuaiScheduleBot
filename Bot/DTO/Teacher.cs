using System.Text.Json.Serialization;

namespace Bot.DTO;

public record Teacher
{
    [JsonPropertyName("ItemId")] public int ItemId;
    [JsonPropertyName("Name")] public string Name;
    [JsonPropertyName("Post")] public string Post;
    [JsonPropertyName("Degree")] public string Degree;
    [JsonPropertyName("AcademicTitle")] public string AcademicTitle;
}


// /get-sem-teachers - получить список преподавателей
// Формат:
// [
//     ...
//     'Name': full_name,             // Полное имя преподавателя
//     'Post': post,                  // Должность преподавателя
//     'Degree': degree,              // Степень (например, кандидат наук, доктор наук и т.д.)
//     'AcademicTitle': academic_title, // Учёное звание (например, профессор, доцент и т.д.)
//     'ItemId': id                   // Уникальный идентификатор преподавателя
//     ...
// ]
//
// Пример
// [
//     {
//         "ItemId": 1,
//         "Name": "Туманов А.Ю.",
//         "Post": "доцент",
//         "Degree": "канд. техн. наук",
//         "AcademicTitle": "доцент"
//     },
//     {
//         "ItemId": 2,
//         "Name": "Калашникова М.В.",
//         "Post": "ассистент",
//         "Degree": null,
//         "AcademicTitle": null
//     },
//  ]
//