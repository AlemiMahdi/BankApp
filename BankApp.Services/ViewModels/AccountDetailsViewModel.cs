using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public class AccountDetailsViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new();
        public bool HasMore { get; set; }
    }
}
