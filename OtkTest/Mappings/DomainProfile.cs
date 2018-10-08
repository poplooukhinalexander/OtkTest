using System.Collections.Generic;

using AutoMapper;

namespace OtkTest.Mappings
{
    public class DomainProfile : Profile
    {
        private static readonly IDictionary<int, string> AccountTypeMapper = new Dictionary<int, string>
        {
            { Models.AccountType.IndividualAccountType, "Физ.лицо" },
            { Models.AccountType.CompanyAccountType, "Юр.лицо" },
            { Models.AccountType.NonResidentAccountType, "Не резидент" }
        };

        public DomainProfile()
        {
            CreateMap<Models.Bank, ViewModels.Bank>();
            CreateMap<Models.Account, ViewModels.Account>()
                .ForMember(d => d.Currency, opts => opts.MapFrom(s => s.CurrencyId))
                .ForMember(d => d.AccountType, opts => opts.MapFrom(s => s.AccountTypeId))
                .ForMember(d => d.AccountTypeName, opts => opts.MapFrom(s => AccountTypeMapper[s.AccountTypeId]));
        }
    }
}
