using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Utilities;
using BankingApp.Entities.Services;
using BankingApp.Areas.Home.Models;
using System.Collections.Generic;

namespace BankingApp.Controllers
{
    [Area("Home")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBankingAppContext BankingAppContext;
        private readonly IBankAccountsService BankAccountsService;
        public HomeController(IBankingAppContext BankingAppContext, IBankAccountsService BankAccountsService)
        {
            this.BankingAppContext = BankingAppContext;
            this.BankAccountsService = BankAccountsService;
        }

        public ViewResult Index()
        {
            var bankAccountEntities = BankAccountsService.GetBankAccountsForUser(BankingAppContext.GetUserID());
            var bankAccountViewModels = new List<BankAccountViewModel>();
            foreach (var bankAccount in bankAccountEntities)
            {
                bankAccountViewModels.Add(new BankAccountViewModel
                {
                    BankAccountID = bankAccount.BankAccountID,
                    Balance = bankAccount.Balance,
                    AccountName = bankAccount.AccountName,
                    AccountType = bankAccount.AccountType
                });
            }

            return View(bankAccountViewModels);
        }

        public ViewResult Privacy()
        {
            return View();
        }
    }
}
