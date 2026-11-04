namespace BankApp.Services.ViewModels;

public class CountryStatisticsViewModel
{
    public string Country { get; set; } = null!;
    public int CustomerCount { get; set; }
    public int AccountCount { get; set; }
    public decimal TotalBalance { get; set; }
}