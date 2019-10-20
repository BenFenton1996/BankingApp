using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Utilities;
using BankingApp.Areas.Home.Models;
using System.Collections.Generic;
using BankingApp.Entities.Services.Interfaces;

namespace BankingApp.Areas.Home.Controllers
{
    [Area("Home")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBankingAppContext _bankingAppContext;
        private readonly IBankAccountsService _bankAccountsService;
        public HomeController(IBankingAppContext bankingAppContext, IBankAccountsService bankAccountsService)
        {
            _bankingAppContext = bankingAppContext;
            _bankAccountsService = bankAccountsService;
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
        /// Transfers money from one account to another
        /// </summary>
        /// <param name="viewModel">Contains details of the transaction including sender, recipient and amount sent</param>
        /// <returns>A boolean indicating whether or not the transaction succeeded</returns>
        [HttpPost]
        public bool Transfer(TransferViewModel viewModel)
        {
            if (!ModelState.IsValid || viewModel.AmountToTransfer > 10000 || viewModel.SenderId == viewModel.RecipientId)
            {
                return false;
            }

            var transactionStatus = _bankAccountsService.TransferMoneyBetweenAccounts(
                    viewModel.AmountToTransfer,
                    viewModel.SenderId,
                    viewModel.RecipientId,
                    "Transfer");

            if (transactionStatus == false)
            {
                ModelState.AddModelError("Funds", "Insufficient Funds");
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
        /// Returns a view for creating a new bank account for the current user
        /// </summary>
        /// <returns>View containing form with fields for creating a new bank account</returns>
        [HttpGet]
        public ViewResult CreateBankAccount()
        {
            var bankAccountEntities = _bankAccountsService.GetBankAccountsForUser(_bankingAppContext.GetUserId());
            var bankAccountViewModels = new List<BankAccountViewModel>();
            foreach (var bankAccount in bankAccountEntities)
            {
                bankAccountViewModels.Add(new BankAccountViewModel
                {
                    AccountName = bankAccount.AccountName,
                    BankAccountId = bankAccount.BankAccountId
                });
            }

            return View(new CreateBankAccountViewModel
            {
                BankAccounts = bankAccountViewModels
            });
        }

        /// <summary>
        /// Creates a new bank account using data from the view model if it is valid
        /// If creation succeeds then redirect to the Index page of the Home controller, otherwise return the CreateBankAccount View with errors
        /// </summary>
        /// <param name="viewModel">The view model containing the data for the new bank account</param>
        /// <returns>The CreateBankAccount view with errors if creation failed or return a redirect result to the Index page of the Home controller if it succeeded</returns>
        [HttpPost]
        public ActionResult CreateBankAccount(CreateBankAccountViewModel viewModel)
        {
            if (ModelState.IsValid 
                && _bankAccountsService.CreateBankAccount(_bankingAppContext.GetUserId(), viewModel.AccountToDepositFromId, viewModel.AccountName, viewModel.AccountType, viewModel.InitialDeposit))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("CreationFailed", "Bank account creation failed");
            return View(viewModel);
        }

        /// <summary>
        /// Gets all bank accounts for the current user and uses them to populate and return a list of BankAccountViewModels
        /// </summary>
        /// <returns>A List of BankAccountViewModels containing the details of all bank accounts for the current user</returns>
        private List<BankAccountViewModel> GetBankAccountDetails()
        {
            var bankAccountEntities = _bankAccountsService.GetBankAccountsForUser(_bankingAppContext.GetUserId());
            var bankAccountViewModels = new List<BankAccountViewModel>();
            foreach (var bankAccount in bankAccountEntities)
            {
                var transferLogs = _bankAccountsService.GetRecentTransfersForAccount(bankAccount.BankAccountId);
                var transferLogViewModels = new List<TransferLogViewModel>();
                foreach (var transferLog in transferLogs)
                {
                    //Check whether or not the current account in the loop was the sender or recipient of the money
                    if (bankAccount.BankAccountId == transferLog.RecipientId)
                    {
                        string senderName = _bankAccountsService.GetBankAccountName(transferLog.SenderId);
                        transferLogViewModels.Add(new TransferLogViewModel(transferLog, $"FROM {senderName}"));
                    }
                    else
                    {
                        string recipientName = _bankAccountsService.GetBankAccountName(transferLog.RecipientId);
                        transferLogViewModels.Add(new TransferLogViewModel(transferLog, $"TO {recipientName}"));
                    }
                }

                bankAccountViewModels.Add(new BankAccountViewModel
                {
                    BankAccountId = bankAccount.BankAccountId,
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
