using BankApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly BankAppDataContext _db;

    public StatisticsRepository(BankAppDataContext db)
    {
        _db = db;
    }

    public async Task<List<CountryStatisticsRow>> GetCountryStatisticsAsync()
    {
        var customers = await _db.Customers
            .AsNoTracking()
            .GroupBy(c => c.Country)
            .Select(g => new { Country = g.Key, Customers = g.Count() })
            .ToListAsync();

        // Distinct så att ett konto med flera dispositions inte räknas dubbelt
        var accounts = await _db.Dispositions
            .AsNoTracking()
            .Select(d => new { d.Customer.Country, d.AccountId, d.Account.Balance })
            .Distinct()
            .GroupBy(x => x.Country)
            .Select(g => new { Country = g.Key, Accounts = g.Count(), Balance = g.Sum(x => x.Balance) })
            .ToListAsync();

        return customers
            .GroupJoin(accounts, c => c.Country, a => a.Country,
                (c, a) => new { Customer = c, Account = a.FirstOrDefault() })
            .Select(x => new CountryStatisticsRow
            {
                Country = x.Customer.Country,
                CustomerCount = x.Customer.Customers,
                AccountCount = x.Account?.Accounts ?? 0,
                TotalBalance = x.Account?.Balance ?? 0m
            })
            .OrderBy(r => r.Country)
            .ToList();
    }
}