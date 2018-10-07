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
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        public async Task roll([Remainder]string message)
        {
            int i = 0;
            Random r = new Random();
            if (!Int32.TryParse(message, out i))
            {
                await Context.Channel.SendMessageAsync("please use a number greater than 1");
            }
            else if (i < 2)
            {
                await Context.Channel.SendMessageAsync("please use a number greater than 1");
            }
            else
            {
                await Context.Channel.SendMessageAsync("" + r.Next(1, i));
            }

        }
        [Command("rename")]
        public async Task rename([Remainder]string message)
        {
            var account = UserAccounts.GetAccount(Context.User);
            string oldName = account.CountryName;
            account.CountryName = message;
            UserAccounts.saveAccounts();
            await Context.Channel.SendMessageAsync($"Your country was renamed from {oldName} to {account.CountryName}");
        }
        [Command("setcolor")]
        public async Task setcolor([Remainder]string message)
        {
            string[] colorstring = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            UInt16[] rgb = new UInt16[3];
            for (int i = 0; i < 3; i++)
            {
                UInt16 j = 0;
                if (!UInt16.TryParse(colorstring[i], out j))
                {
                    await Context.Channel.SendMessageAsync("format as g!setcolor r g b");
                    return;
                }
                else if (j < 0 || j > 255)
                {
                    await Context.Channel.SendMessageAsync("make sure all numbers are within 0 and 255");
                    return;
                }
                else
                {
                    rgb[i] = j;
                }
            }
            var account = UserAccounts.GetAccount(Context.User);
            account.colorR = rgb[0];
            account.colorG = rgb[1];
            account.colorB = rgb[2];
            UserAccounts.saveAccounts();
            var embed = new EmbedBuilder();
            embed.WithTitle($"changed {account.CountryName}'s color");
            embed.WithColor(new Color(rgb[0], rgb[1], rgb[2]));
            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("setflagurl")]
        public async Task setflag(string url)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.flag = url;
            UserAccounts.saveAccounts();
            await Context.Channel.SendMessageAsync("flag change was successful");
        }
        [Command("info")]
        public async Task info()
        {
            var account = UserAccounts.GetAccount(Context.User);
            var embed = new EmbedBuilder();
            embed.WithTitle(account.CountryName);
            embed.WithThumbnailUrl(account.flag);
            embed.WithColor(new Color(account.colorR, account.colorG, account.colorB));
            embed.WithDescription($"Auros: {account.Auros} \n Happiness: {account.happiness}");
            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("info")]
        public async Task info(IGuildUser user)
        {
            var account = UserAccounts.GetAccount((SocketUser)user);
            var embed = new EmbedBuilder();
            embed.WithTitle(account.CountryName);
            embed.WithThumbnailUrl(account.flag);
            embed.WithColor(new Color(account.colorR, account.colorG, account.colorB));
            embed.WithDescription($"Auros: {account.Auros} \n Happiness: {account.happiness}");
            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
