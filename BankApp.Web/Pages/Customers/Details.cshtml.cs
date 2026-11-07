using BankApp.Services;
using BankApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Web.Pages.Customers;

[Authorize(Roles = "Admin,Cashier")]
public class DetailsModel : PageModel
{
    private readonly ICustomerService _customerService;

    public DetailsModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public CustomerDetailsViewModel? Customer { get; private set; }

    public async Task OnGetAsync()
    {
        Customer = await _customerService.GetDetailsAsync(Id);
    }
}