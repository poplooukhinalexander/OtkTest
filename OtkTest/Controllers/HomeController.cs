using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace OtkTest.Controllers
{
    using BL.Account;
    using BL.Bank;
    using Models;
    using ViewModels;

    public class HomeController : Controller
    {
        private IAccountService AccountService { get; }
        private IBankService BankService { get; }

        public HomeController(IAccountService accountService, IBankService bankService)
        {
            Contract.Requires(accountService != null);
            Contract.Requires(bankService != null);

            AccountService = accountService;
            BankService = bankService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await BankService.GetBanksAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(int bankId, string accountNumber, int skip, int take)
        {
            var result = await AccountService.GetAccountsAsync(bankId, accountNumber, skip, take);
            var response = new JsonResult(result)
            {
                StatusCode = 200
            };

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> TransferMoney(TransferMoneyRequest request)
        {
            await AccountService.TransferMoneyAsync(request.SenderAccountId, request.RecepientAccountId, request.TransferAmount);
            var response = new JsonResult(null)
            {
                StatusCode = 200
            };
            return response;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
