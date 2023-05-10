using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public interface IEmailService
    {
        public void SendEmail(string emailAddress, string emailBody);
    }
}
