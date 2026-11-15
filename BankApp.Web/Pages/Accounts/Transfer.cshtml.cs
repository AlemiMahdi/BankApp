using BankApp.Services;
using BankApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Web.Pages.Accounts;

[Authorize(Roles = "Admin,Cashier")]
public class TransferModel : PageModel
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;

    public TransferModel(IAccountService accountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public TransferInputModel Input { get; set; } = new();

    public AccountDetailsViewModel? Account { get; private set; }
    public string? ErrorMessage { get; private set; }

    public async Task OnGetAsync()
    {
        Account = await _accountService.GetDetailsAsync(Id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Account = await _accountService.GetDetailsAsync(Id);
        if (Account == null || !ModelState.IsValid) return Page();

        var result = await _transactionService.TransferAsync(Id, Input.ToAccountId!.Value, Input.Amount!.Value);
        if (!result.Succeeded)
        {
            ErrorMessage = result.Error;
            return Page();
        }

        TempData["Success"] = $"Överföring på {Input.Amount.Value:N2} till konto {Input.ToAccountId} genomförd.";
        return RedirectToPage("./Details", new { id = Id });
    }
}