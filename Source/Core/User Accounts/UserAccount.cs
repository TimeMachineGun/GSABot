using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAbot.Core.User_Accounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }
        public string CountryName { get; set; }
        public uint Auros { get; set; }
        public float happiness { get; set; }
        public UInt16 colorR { get; set; }
        public UInt16 colorG { get; set; }
        public UInt16 colorB { get; set; }
        public string flag { get; set; }
    }
}
