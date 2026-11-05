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
    }
}