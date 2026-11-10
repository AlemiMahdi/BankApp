using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public class TransactionPageViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; } = new();
        public bool HasMore { get; set; }
    }
}
