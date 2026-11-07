using BankApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankAppDataContext _db;
        public CustomerRepository(BankAppDataContext db)
        {
            _db = db;
        }

        private IQueryable<Customer> Filter(string? name, string? city)
        {
            var query = _db.Customers.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var n = name.Trim();
                query = query.Where(c => (c.Givenname + " " + c.Surname).Contains(n));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                var ci = city.Trim();
                query = query.Where(c => c.City.Contains(ci));
            }
            return query;
        }

        public Task<int> CountAsync(string? name, string? city)
        {
            return Filter(name, city).CountAsync();
        }

        public Task<List<Customer>> SearchAsync(string? name, string? city, int skip, int take)
        {
            return Filter(name, city)
                .OrderBy(c => c.Surname)
                .ThenBy(c => c.Givenname)
                .ThenBy(c => c.CustomerId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public Task<Customer?> GetWithAccountsAsync(int id)
        {
            return _db.Customers
                .AsNoTracking()
                .Include(c => c.Dispositions)
                    .ThenInclude(d => d.Account)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }
    }
}
