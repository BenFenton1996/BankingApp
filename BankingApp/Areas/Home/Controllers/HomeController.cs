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

        /// <summary>
        /// Returns the Home Index page, populated with details from bank accounts and recent transactions
        /// Includes a form for doing quick transactions between the current user's accounts
        /// </summary>
        /// <returns>The Home Index page with a ViewModel populated with details from bank accounts and recent transactions</returns>
        [HttpGet]
        public ViewResult Index()
        {
            return View(GetBankAccountDetails());
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns>Privacy View for viewing the privacy policy and changing privacy settings</returns>
        [HttpGet]
        public ViewResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Transfer(TransferViewModel viewModel)
        {
            var transactionStatus = false;
            if (ModelState.IsValid && viewModel.AmountToTransfer <= 10000 && viewModel.SenderID != viewModel.RecipientID)
            {
                transactionStatus = BankAccountsService.TransferMoneyBetweenAccounts(
                    viewModel.AmountToTransfer,
                    viewModel.SenderID,
                    viewModel.RecipientID);

                if (transactionStatus == false)
                {
                    ModelState.AddModelError("Funds", "Insufficient Funds");
                }
            }

            return transactionStatus;
        }

        /// <summary>
        /// Returns a partial view for bank details populated with account details and recent transactions
        /// </summary>
        /// <returns>The BankAccountsPartial View with a ViewModel populated with account details and recent transactions</returns>
        [HttpGet]
        public PartialViewResult BankAccountsPartial()
        {
            return PartialView(GetBankAccountDetails());
        }

        /// <summary>
        /// Gets all bank accounts for the current user and uses them to populate and return a list of BankAccountViewModels
        /// </summary>
        /// <returns>A List of BankAccountViewModels containing the details of all bank accounts for the current user</returns>
        private List<BankAccountViewModel> GetBankAccountDetails()
        {
            var bankAccountEntities = BankAccountsService.GetBankAccountsForUser(BankingAppContext.GetUserID());
            var bankAccountViewModels = new List<BankAccountViewModel>();
            foreach (var bankAccount in bankAccountEntities)
            {
                var transferLogs = BankAccountsService.GetRecentTransfersForAccount(bankAccount.BankAccountID);
                var transferLogViewModels = new List<TransferLogViewModel>();
                foreach (var transferLog in transferLogs)
                {
                    transferLogViewModels.Add(new TransferLogViewModel(transferLog));
                }
                bankAccountViewModels.Add(new BankAccountViewModel
                {
                    BankAccountID = bankAccount.BankAccountID,
                    Balance = bankAccount.Balance,
                    AccountName = bankAccount.AccountName,
                    AccountType = bankAccount.AccountType,
                    RecentTransfers = transferLogViewModels
                });
            }

            return bankAccountViewModels;
        }
    }
}
