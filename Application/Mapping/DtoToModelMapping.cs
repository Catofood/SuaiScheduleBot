// using Application.Cache.Entities;
// using Application.Client.DTO;
// using Application.Models;
// using Version = Application.Cache.Entities.Version;
//
// namespace Application.Mapping;
//
// using AutoMapper;
//
// public class DtoToModelMapping : Profile
// {
//     // TODO: Добавить маппинг с переводом данных в формат Pair с обогащением данными из кэша.
//     public DtoToModelMapping()
//     {
//         CreateMap<EventDto, Pair>()
//             .ForMember(dest => dest.Building, opt => opt.MapFrom<BuildingResolver>());
//     }
// }
//
// public class BuildingResolver : IValueResolver<BuildingDto, string, string>
// {
//     public string Resolve(BuildingDto source, string destination, string destMember, ResolutionContext context)
//     {
//         throw new NotImplementedException();
//     }
// }