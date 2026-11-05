using BankApp.Services;
using BankApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Web.Pages.Customers;

[Authorize(Roles = "Admin,Cashier")]
public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;

    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Name { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? City { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public CustomerSearchViewModel Result { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Result = await _customerService.SearchAsync(Name, City, PageNumber);
    }
}