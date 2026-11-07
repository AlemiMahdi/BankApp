using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public class CustomerAccountViewModel
    {
        public int AccountId { get; set; }
        public string Type { get; set; } = null;
        public string Frequency { get; set; } = null;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
    }
}
