namespace Application.Cache.Entities;

public class Version
{
    public long Id { get; set; }
    public int VersionId { get; set; }
    public int VersionMain { get; set; }
    public int VersionSession { get; set; }
    public int VersionZaoch { get; set; }
    public int VersionSpo { get; set; }
    public long DateTime { get; set; }
    public int Term { get; set; }
}


// [GET]
// /get-version - получить данные о текущей версии расписания
// Формат:
// {
//     'versionID': id,
//     'version_main': updated_och_flag,
//     'version_session': updated_session_flag,
//     'version_zaoch': updated_zaoch_flag,
//     'version_spo': updated_spo_flag,
//     'datetime' : datetime_of_generation_timestamp,
//     'term' : num_term (1|2)
// }

// Пример
// {
//     "versionID": 22,
//     "version_main": 5,
//     "version_session": 4,
//     "version_zaoch": 6,
//     "version_spo": 7,
//     "datetime": 1739354574,
//     "term": 2
// }