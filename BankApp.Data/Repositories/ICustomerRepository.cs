using BankApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> CountAsync(string? name, string? city);
        Task<List<Customer>> SearchAsync(string? name, string? city, int skip, int take);
        Task<Customer?> GetWithAccountsAsync(int id);
    }
}
