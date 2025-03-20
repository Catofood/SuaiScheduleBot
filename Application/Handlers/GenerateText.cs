// using Application.Client;
// using MediatR;
//
// namespace Application.Handlers;
//
// public record GenerateTextCommand(long GroupId, DateTimeOffset StartDate, DateTimeOffset EndDate) : IRequest;
//
// public class GenerateText : IRequestHandler<GenerateTextCommand>
// {
//     private readonly GuapClient _guapClient;
//
//     public GenerateText(GuapClient guapClient)
//     {
//         _guapClient = guapClient;
//     }
//
//     public async Task Handle(GenerateTextCommand request, CancellationToken cancellationToken)
//     {
//         // TODO: Создать класс с описанием расписания по подобию SUAI бота
//         // 
//         var dict = await _guapClient.GetGroupStudyEvents(request.GroupId, request.StartDate, request.EndDate);
//     }
// }
