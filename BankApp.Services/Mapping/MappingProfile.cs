using AutoMapper;
using BankApp.Data.Repositories;
using BankApp.Services.ViewModels;

namespace BankApp.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CountryStatisticsRow, CountryStatisticsViewModel>();
    }
}