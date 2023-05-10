using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    internal class OTPDao
    {
        // Should be store in database. To make it easier, I implement by `Dictionary`
        private Dictionary<string, string> emailToOtp = new();

        public void CreateOTP(string emailAddress, string otp)
        {
            emailToOtp.Add(emailAddress, otp);
        }

        public string GetOTP(string emailAddress)
        {
            return emailToOtp[emailAddress];
        }
    }
}
