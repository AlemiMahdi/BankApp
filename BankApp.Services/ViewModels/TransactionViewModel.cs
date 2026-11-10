using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

    }
}
