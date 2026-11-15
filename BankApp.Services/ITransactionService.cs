namespace BankApp.Services;

public interface ITransactionService
{
    Task<OperationResult> DepositAsync(int accountId, decimal amount);
    Task<OperationResult> WithdrawAsync(int accountId, decimal amount);
    Task<OperationResult> TransferAsync(int fromAccountId, int toAccountId, decimal amount);
}