namespace BankApp.Data.Repositories;

public interface IStatisticsRepository
{
    Task<List<CountryStatisticsRow>> GetCountryStatisticsAsync();
}