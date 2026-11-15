namespace BankApp.Services;

public class OperationResult
{
    public bool Succeeded { get; private init; }
    public string? Error { get; private init; }

    public static OperationResult Ok() => new() { Succeeded = true };
    public static OperationResult Fail(string error) => new() { Error = error };
}