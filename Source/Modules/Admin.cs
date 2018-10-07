using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GSAbot.Core.User_Accounts;

namespace GSAbot.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("pay")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task pay(IGuildUser user, uint amount)
        {
            var account = UserAccounts.GetAccount((SocketUser)user);
            account.Auros += amount;
            UserAccounts.saveAccounts();
            string auro = Utilities.GetAlert("auro");
            await Context.Channel.SendMessageAsync($"{auro}{amount} was added to {account.CountryName} and now has {auro}{account.Auros}");
        }
        [Command("take")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task take(IGuildUser user, uint amount)
        {
            string auro = Utilities.GetAlert("auro");
            var account = UserAccounts.GetAccount((SocketUser)user);
            if (Convert.ToInt32(account.Auros) - amount > 0)
            {
                account.Auros -= amount;
                UserAccounts.saveAccounts();
                await Context.Channel.SendMessageAsync($"{auro}{amount} was taken to {account.CountryName} and now has {auro}{account.Auros}");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{account.CountryName} cannot pay {auro}{amount}, they only have {auro}{account.Auros}");
            }
        }
        [Command("getcolor")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task getcolor(IGuildUser user)
        {
            var account = UserAccounts.GetAccount((SocketUser)user);
            await Context.Channel.SendMessageAsync($"R: {account.colorR}      G: {account.colorG}      B: {account.colorB}");
        }
    }
}