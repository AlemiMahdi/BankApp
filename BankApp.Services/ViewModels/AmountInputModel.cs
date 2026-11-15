using System.ComponentModel.DataAnnotations;

namespace BankApp.Services.ViewModels;

public class AmountInputModel
{
    [Required(ErrorMessage = "Ange ett belopp.")]
    [Range(0.01, 1000000000, ErrorMessage = "Beloppet måste vara större än 0 och högst 1 000 000 000.")]
    [Display(Name = "Belopp")]
    public decimal? Amount { get; set; }
}