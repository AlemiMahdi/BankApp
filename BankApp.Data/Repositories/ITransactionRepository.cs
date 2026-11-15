namespace BankApp.Data.Repositories;

public interface ITransactionRepository
{
    Task<PostingResult> PostAsync(IReadOnlyList<PostingRequest> postings);
}