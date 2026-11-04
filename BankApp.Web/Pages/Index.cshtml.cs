using BankApp.Services;
using BankApp.Services.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IStatisticsService _statisticsService;
    public IndexModel(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public List<CountryStatisticsViewModel> Countries { get; private set; } = new();
    public async Task OnGetAsync()
    {
        Countries = await _statisticsService.GetCountryStatisticsAsync();
    }
}
