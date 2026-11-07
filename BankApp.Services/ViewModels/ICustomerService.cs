using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Services.ViewModels
{
    public interface ICustomerService
    {
        Task<CustomerSearchViewModel> SearchAsync(string? name, string? city, int page);
        Task<CustomerDetailsViewModel> GetDetailsAsync(int id);
    }
}
