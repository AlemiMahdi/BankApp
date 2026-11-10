using AutoMapper;
using BankApp.Data.Repositories;
using BankApp.Services.ViewModels;

namespace BankApp.Services;

public class AccountService : IAccountService
{
    private const int PageSize = 20;

    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AccountDetailsViewModel?> GetDetailsAsync(int id)
    {
        var account = await _repository.GetAsync(id);
        if (account == null) return null;

        var firstPage = await GetTransactionsAsync(id, 0);

        var viewModel = _mapper.Map<AccountDetailsViewModel>(account);
        viewModel.Transactions = firstPage.Transactions;
        viewModel.HasMore = firstPage.HasMore;
        return viewModel;
    }

    public async Task<TransactionPageViewModel> GetTransactionsAsync(int accountId, int skip)
    {
        skip = Math.Max(0, skip);

        // Hämtar en extra rad för att veta om det finns fler
        var transactions = await _repository.GetTransactionsAsync(accountId, skip, PageSize + 1);

        return new TransactionPageViewModel
        {
            Transactions = _mapper.Map<List<TransactionViewModel>>(transactions.Take(PageSize)),
            HasMore = transactions.Count > PageSize
        };
    }
}