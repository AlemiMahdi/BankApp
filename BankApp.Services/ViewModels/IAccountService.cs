using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public interface IAccountService
    {
        Task<AccountDetailsViewModel> GetDetailsAsync(int id);
        Task<TransactionPageViewModel> GetTransactionsAsync(int accountId, int skip);
    }
}
