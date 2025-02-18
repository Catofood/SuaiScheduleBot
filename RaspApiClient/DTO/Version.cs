namespace RaspApiClient.DTO;

using System.Text.Json.Serialization;

public class Version
{
    [JsonPropertyName("versionID")]
    public int VersionId { get; init; }

    [JsonPropertyName("version_main")]
    public int VersionMain { get; init; }

    [JsonPropertyName("version_session")]
    public int VersionSession { get; init; }

    [JsonPropertyName("version_zaoch")]
    public int VersionZaoch { get; init; }

    [JsonPropertyName("version_spo")]
    public int VersionSpo { get; init; }

    [JsonPropertyName("datetime")]
    public long DateTime { get; init; }

    [JsonPropertyName("term")]
    public int Term { get; init; }
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