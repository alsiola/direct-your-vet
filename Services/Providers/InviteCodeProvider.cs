using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DYV.Services.Providers
{
    public class InviteCodeProvider : IInviteCodeProvider
    {
        public string GetNewInviteCode()
        {
            return GetNewInviteCode(16);
        }

        public string GetNewInviteCode(int length)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            byte[] data = new byte[1];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[length];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder sb = new StringBuilder(length);
            foreach(byte b in data)
            {
                sb.Append(chars[b % chars.Length]);
            }

            return sb.ToString();
        }
    }
}
