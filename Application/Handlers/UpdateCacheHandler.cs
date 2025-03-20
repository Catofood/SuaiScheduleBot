using Application.Cache;
using Application.Cache.Entities;
using Application.Client;
using Application.Commands;
using Application.Mapping;
using AutoMapper;
using MediatR;
using Version = System.Version;

namespace Application.Handlers;

public class UpdateCacheHandler : IRequestHandler<UpdateCacheCommand>
{
    private readonly CacheRepository _cacheRepository;
    private readonly GuapClient _guapClient;
    private readonly IMapper _mapper;

    public UpdateCacheHandler(CacheRepository cacheRepository, GuapClient guapClient, IMapper mapper)
    {
        _cacheRepository = cacheRepository;
        _guapClient = guapClient;
        _mapper = mapper;
    }

    // Этот метод ПОЛНОСТЬЮ очищает redis, затем выполняет запросы на апи гуап и сохраняет данные в redis
    private async Task UpdateCache()
    {
        // Ждём пока база Redis будет полностью очищена
        await _cacheRepository.FlushDb();
        await Task.WhenAll(DoBuildings(), DoRooms(), DoGroups(), DoTeachers(), DoDepartments(), DoVersion());
        // Делаем все нужные запросы на API гуап и сохраняем данные в кэш
        async Task DoBuildings()
        {
            var buildingDtos = await _guapClient.GetBuildings();
            var buildingEntities = _mapper.Map<Dictionary<long, string>>(buildingDtos);
            await _cacheRepository.SetBuildingNames(buildingEntities);
        }

        async Task DoRooms()
        {
            var roomsDtos = await _guapClient.GetRooms();
            var roomEntities = _mapper.Map<Dictionary<long, Room>>(roomsDtos);
            await _cacheRepository.SetRooms(roomEntities);
        }

        async Task DoGroups()
        {
            var groupDtos = await _guapClient.GetGroups();
            var groupEntities = _mapper.Map<Dictionary<string, long>>(groupDtos);
            await _cacheRepository.SetGroupIds(groupEntities);
        }

        async Task DoTeachers()
        {
            var teacherDtos = await _guapClient.GetTeachers();
            var teacherEntities = _mapper.Map<Dictionary<long, Teacher>>(teacherDtos);
            await _cacheRepository.SetTeachers(teacherEntities);
        }


        async Task DoDepartments()
        {
            var departmentDtos = await _guapClient.GetDepartments();
            var departmentEntities = _mapper.Map<Dictionary<long, string>>(departmentDtos);
            await _cacheRepository.SetDepartmentNames(departmentEntities);
        }

        async Task DoVersion()
        {
            var versionDto = await _guapClient.GetVersion();
            var versionEntity = _mapper.Map<Cache.Entities.Version>(versionDto);
            await _cacheRepository.SetVersion(versionEntity);
        }
    }

    public async Task Handle(UpdateCacheCommand request, CancellationToken cancellationToken)
    {
        await UpdateCache();
    }
}