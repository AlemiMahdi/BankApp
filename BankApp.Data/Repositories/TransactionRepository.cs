using BankApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankAppDataContext _db;
        public TransactionRepository(BankAppDataContext db)
        {
            _db = db;
        }

        public async Task<PostingResult> PostAsync(IReadOnlyList<PostingRequest> postings)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync();
            foreach (var posting in postings)
            {
                var amount = posting.Amount;
                var updated = await _db.Accounts
                    .Where(a => a.AccountId == posting.AccountId && a.Balance + amount >= 0)
                    .ExecuteUpdateAsync(s => s.SetProperty(a => a.Balance, a => a.Balance + amount));

                if (updated == 0)
                {
                    var exists = await _db.Accounts.AnyAsync(a => a.AccountId == posting.AccountId);
                    return exists ? PostingResult.InsufficientFunds : PostingResult.AccountNotFound;
                }

                var newBalance = await _db.Accounts
                    .Where(a => a.AccountId == posting.AccountId)
                    .Select(a => a.Balance)
                    .SingleAsync();

                _db.Transactions.Add(new Transaction
                {
                    AccountId = posting.AccountId,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Type = posting.Type,
                    Operation = posting.Operation,
                    Amount = amount,
                    Balance = newBalance,
                    Account = posting.CounterAccount
                });
                await _db.SaveChangesAsync();
            }

            await dbTransaction.CommitAsync();
            return PostingResult.Success;
        
        }
    }
}
