using System.ComponentModel.DataAnnotations;

namespace BankApp.Services.ViewModels;

public class TransferInputModel : AmountInputModel
{
    [Required(ErrorMessage = "Ange mottagarkonto.")]
    [Range(1, int.MaxValue, ErrorMessage = "Ange ett giltigt kontonummer.")]
    [Display(Name = "Till konto")]
    public int? ToAccountId { get; set; }
}