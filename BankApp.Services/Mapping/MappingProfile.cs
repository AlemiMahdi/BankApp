using AutoMapper;
using BankApp.Data.Repositories;
using BankApp.Services.ViewModels;
using BankApp.Data.Models;

namespace BankApp.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CountryStatisticsRow, CountryStatisticsViewModel>();
        CreateMap<Customer, CustomerListItemViewModel>();
        CreateMap<Customer, CustomerDetailsViewModel>()
            .ForMember(d => d.Accounts, o => o.MapFrom(s => s.Dispositions));

        CreateMap<Disposition, CustomerAccountViewModel>()
            .ForMember(d => d.Frequency, o => o.MapFrom(s => s.Account.Frequency))
            .ForMember(d => d.Created, o => o.MapFrom(s => s.Account.Created))
            .ForMember(d => d.Balance, o => o.MapFrom(s => s.Account.Balance));
    }
}