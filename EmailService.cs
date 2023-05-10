using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public class EmailService: IEmailService
    {
        public void SendEmail(string emailAddress, string emailBody)
        {
            // assume this has been implemented
            Console.WriteLine("Email sent to " + emailAddress + ": " + emailBody);
        }
    }
}
