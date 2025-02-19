namespace RaspApiClient.DTO;

using System.Text.Json.Serialization;

public class Teacher
{
    [JsonPropertyName("ItemId")] public int ItemId { get; init; }

    [JsonPropertyName("Name")] public string Name { get; init; }

    [JsonPropertyName("Post")] public string Post { get; init; }

    [JsonPropertyName("Degree")] public string Degree { get; init; }

    [JsonPropertyName("AcademicTitle")] public string AcademicTitle { get; init; }
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