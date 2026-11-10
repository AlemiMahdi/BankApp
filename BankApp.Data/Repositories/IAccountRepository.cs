using BankApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAsync(int id);
        Task<List<Transaction>> GetTransactionsAsync(int accountId, int skip, int take);
    }
}
