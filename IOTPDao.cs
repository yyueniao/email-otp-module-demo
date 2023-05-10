using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public interface IOTPDao
    {
        public void CreateOTP(string emailAddress, string otp);

        public string GetOTP(string emailAddress);
    }
}
