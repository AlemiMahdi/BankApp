namespace BankApp.Data.Repositories;

public record PostingRequest(int AccountId, decimal Amount, string Type, string Operation, string? CounterAccount);

public enum PostingResult
{
    Success,
    AccountNotFound,
    InsufficientFunds
}