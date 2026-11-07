using AutoMapper;
using BankApp.Data.Repositories;
using BankApp.Services.ViewModels;

namespace BankApp.Services;

public class CustomerService : ICustomerService
{
    private const int PageSize = 50;

    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerSearchViewModel> SearchAsync(string? name, string? city, int page)
    {
        var totalCount = await _repository.CountAsync(name, city);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var customers = await _repository.SearchAsync(name, city, (page - 1) * PageSize, PageSize);

        return new CustomerSearchViewModel
        {
            Customers = _mapper.Map<List<CustomerListItemViewModel>>(customers),
            Page = page,
            TotalPages = totalPages,
            TotalCount = totalCount
        };
    }

    public async Task<CustomerDetailsViewModel?> GetDetailsAsync(int id)
    {
        var customer = await _repository.GetWithAccountsAsync(id);
        if (customer == null) return null;

        var viewModel = _mapper.Map<CustomerDetailsViewModel>(customer);
        viewModel.Accounts = viewModel.Accounts.OrderBy(a => a.AccountId).ToList();
        return viewModel;
    }
}