using BankApp.Data.Repositories;

namespace BankApp.Services;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accounts;
    private readonly ITransactionRepository _transactions;

    public TransactionService(IAccountRepository accounts, ITransactionRepository transactions)
    {
        _accounts = accounts;
        _transactions = transactions;
    }

    public async Task<OperationResult> DepositAsync(int accountId, decimal amount)
    {
        var error = ValidateAmount(amount);
        if (error != null) return OperationResult.Fail(error);

        var account = await _accounts.GetAsync(accountId);
        if (account == null) return OperationResult.Fail($"Konto {accountId} finns inte.");

        var result = await _transactions.PostAsync(new[]
        {
            new PostingRequest(accountId, amount, TransactionKinds.Credit, TransactionKinds.DepositOperation, null)
        });

        return result == PostingResult.Success
            ? OperationResult.Ok()
            : OperationResult.Fail("Insättningen kunde inte genomföras.");
    }

    public async Task<OperationResult> WithdrawAsync(int accountId, decimal amount)
    {
        var error = ValidateAmount(amount);
        if (error != null) return OperationResult.Fail(error);

        var account = await _accounts.GetAsync(accountId);
        if (account == null) return OperationResult.Fail($"Konto {accountId} finns inte.");

        if (amount > account.Balance)
            return OperationResult.Fail(InsufficientMessage(account.Balance, amount, "ta ut"));

        var result = await _transactions.PostAsync(new[]
        {
            new PostingRequest(accountId, -amount, TransactionKinds.Debit, TransactionKinds.WithdrawalOperation, null)
        });

        return ToResult(result, account.Balance, amount, "ta ut");
    }

    public async Task<OperationResult> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
    {
        var error = ValidateAmount(amount);
        if (error != null) return OperationResult.Fail(error);

        if (fromAccountId == toAccountId)
            return OperationResult.Fail("Du kan inte överföra pengar till samma konto.");

        var from = await _accounts.GetAsync(fromAccountId);
        if (from == null) return OperationResult.Fail($"Konto {fromAccountId} finns inte.");

        var to = await _accounts.GetAsync(toAccountId);
        if (to == null) return OperationResult.Fail($"Mottagarkonto {toAccountId} finns inte.");

        if (amount > from.Balance)
            return OperationResult.Fail(InsufficientMessage(from.Balance, amount, "överföra"));

        var result = await _transactions.PostAsync(new[]
        {
            new PostingRequest(fromAccountId, -amount, TransactionKinds.Debit,
                TransactionKinds.TransferOutOperation, toAccountId.ToString()),
            new PostingRequest(toAccountId, amount, TransactionKinds.Credit,
                TransactionKinds.TransferInOperation, fromAccountId.ToString())
        });

        return ToResult(result, from.Balance, amount, "överföra");
    }

    private static string? ValidateAmount(decimal amount)
    {
        if (amount <= 0) return "Beloppet måste vara större än 0.";
        if (decimal.Round(amount, 2) != amount) return "Beloppet får högst ha två decimaler.";
        return null;
    }

    private static string InsufficientMessage(decimal balance, decimal amount, string verb)
    {
        return $"Otillräckligt saldo. Saldot är {balance:N2} men du försökte {verb} {amount:N2}.";
    }

    private static OperationResult ToResult(PostingResult result, decimal balance, decimal amount, string verb)
    {
        return result switch
        {
            PostingResult.Success => OperationResult.Ok(),
            PostingResult.InsufficientFunds => OperationResult.Fail(InsufficientMessage(balance, amount, verb)),
            _ => OperationResult.Fail("Kontot hittades inte.")
        };
    }
}