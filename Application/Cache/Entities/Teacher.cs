namespace Application.Cache.Entities;

public class Teacher 
{
    public string Name { get; set; } = string.Empty;
    public string Post { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string AcademicTitle { get; set; } = string.Empty;
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