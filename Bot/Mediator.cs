using ClassLibrary;

namespace Bot;

public interface IMediator
{
    List<StudyScheduleItem> GetStudyScheduleItems();

    void UpdateStudyScheduleItems();
}

public class Mediator : IMediator
{
    
}