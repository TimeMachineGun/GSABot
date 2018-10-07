using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAbot.Core.User_Accounts
{
    public static class UserAccounts
    {
        public static List<UserAccount> accounts = null;
        private static string accountsFile = "Resources/accounts.json";

        static UserAccounts()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                saveAccounts();
            }
        }

        public static void saveAccounts()
        {
            DataStorage.SaveUserAccounts(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }
        private static UserAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;
            var account = result.FirstOrDefault();
            if (account == null)
            {
                account = CreateUserAccount(id);
            }
            return account;
        }

        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                CountryName = "unnamed",
                Auros = 10,
                happiness = 80,
                colorR = 20,
                colorG = 20,
                colorB = 20,
                flag = null
        };

            accounts.Add(newAccount);
            saveAccounts();
            return newAccount;
        }
    }
}
