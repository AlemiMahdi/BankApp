using AutoMapper;
using BankApp.Data.Repositories;
using BankApp.Services.ViewModels;

namespace BankApp.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _repository;
    private readonly IMapper _mapper;

    public StatisticsService(IStatisticsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CountryStatisticsViewModel>> GetCountryStatisticsAsync()
    {
        var rows = await _repository.GetCountryStatisticsAsync();
        return _mapper.Map<List<CountryStatisticsViewModel>>(rows);
    }
}