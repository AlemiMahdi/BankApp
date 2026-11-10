using BankApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankAppDataContext _db;
        public AccountRepository( BankAppDataContext db)
        {
            _db = db;
        }
        public Task<Account?> GetAsync(int id)
        {
            return _db.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public Task<List<Transaction>> GetTransactionsAsync(int accountId, int skip, int take)
        {
            return _db.Transactions
                .AsNoTracking()
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.TransactionId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
