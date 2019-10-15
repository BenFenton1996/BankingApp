using BankingApp.Entities.Models;
using System.Collections.Generic;

namespace BankingApp.Entities.Services.Interfaces
{
    public interface IUsersService
    {
        /// <summary>
        /// Gets all rows in the Users table and returns them as a List
        /// </summary>
        /// <returns>A List containing all rows in the Users table</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Checks the Users table and returns the row with a matching Email and Password
        /// </summary>
        /// <param name="Email">The Email to check against the Email column in the Users table</param>
        /// <param name="Password">The Password to check against the Password column in the Users table</param>
        /// <returns>The row in Users with matching details</returns>
        User CheckUserDetails(string Email, string Password);

        /// <summary>
        /// Adds a new row to the Users table if it doesn't already exist
        /// </summary>
        /// <param name="Username">The Username of the new account</param>
        /// <param name="Email">The Email of the new account</param>
        /// <param name="Password">The plaintext of the new account</param>
        /// <returns>True if the account was created, false if the account already exists</returns>
        bool CreateNewAccount(string Username, string Email, string Password);
    }
}
