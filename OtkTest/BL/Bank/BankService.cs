using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OtkTest.BL.Bank
{
    using DAL.Bank;
    using ViewModels;

    public class BankService : IBankService
    {
        private IBankRepository BankRepository { get; }
        private IMapper Mapper { get; }

        public BankService(IBankRepository bankRepository, IMapper mapper)
        {
            BankRepository = bankRepository;
            Mapper = mapper;
        }

        async Task<IEnumerable<Bank>> IBankService.GetBanksAsync()
        {
            var model = await BankRepository.GetAll().ToListAsync();
            var viewModel = Mapper.Map<IEnumerable<Bank>>(model);
            return viewModel;
        }
    }
}
