using BankApp.Data.Models;
using BankApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Web.Pages.Accounts
{
    [Authorize(Roles = "Admin, Cashier")]
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;
        public DetailsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public AccountDetailsViewModel? Account { get; private set; }
        public async Task OnGetAsync()
        {
            Account = await _accountService.GetDetailsAsync(Id);
        }

        public async Task<IActionResult> OnGetTransactionsAsync(int skip)
        {
            var page = await _accountService.GetTransactionsAsync(Id, skip);
            return new JsonResult(new
            {
                hasMore = page.HasMore,
                transactions = page.Transactions.Select(t => new
                {
                    date = t.Date.ToString("yyyy-MM-dd"),
                    type = t.Type,
                    operation = t.Operation,
                    amount = t.Amount.ToString("N2"),
                    balance = t.Balance.ToString("N2")
                })
            });
        }

    }
}
