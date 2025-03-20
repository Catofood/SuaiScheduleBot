using Application.Client;
using Application.Models;

namespace Application.Services;


public class TextGenerationService
{
    private readonly SuaiClient _suaiClient;

    public TextGenerationService(SuaiClient suaiClient)
    {
        _suaiClient = suaiClient;
    }
    
    

    public async Task Generate(List<Pair> pairs)
    {
        var sorted = pairs.OrderBy(x => x.PairStartTime).GroupBy(x => x.PairStartTime.Value.Date);
    }
}
