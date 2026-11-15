namespace BankApp.Services;

internal static class TransactionKinds
{
    public const string Credit = "Credit";
    public const string Debit = "Debit";

    public const string DepositOperation = "Credit in Cash";
    public const string WithdrawalOperation = "Withdrawal in Cash";
    public const string TransferOutOperation = "Remittance to Another Bank";
    public const string TransferInOperation = "Collection from Another Bank";
}