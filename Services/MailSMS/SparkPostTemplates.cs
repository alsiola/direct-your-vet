using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.Services
{
    public static class SparkPostTemplates
    {
        public static string RegisterTemplate = "dyvregister";

        public static string SubscriberInvite = "dyvuser-invite";
        internal static string ForgotPassword = "dyvforgotpassword";
        internal static string Twofactorcode = "dyvtwofactorcode";
    }
}
