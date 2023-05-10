using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public class EmailOTPModule
    {
        public const string STATUS_EMAIL_OK = "EMAIL_OK";
        public const string STATUS_EMAIL_FAIL = "EMAIL_FAIL";
        public const string STATUS_EMAIL_INVALID = "EMAIL_INVALID";
        public const string STATUS_OTP_OK = "OTP_OK";
        public const string STATUS_OTP_FAIL = "OTP_FAIL";
        public const string STATUS_OTP_TIMEOUT = "OTP_TIMEOUT";

        private readonly EmailService emailService;
        private readonly OTPDao otpDao;
        private static readonly Random random = new();
        private string userEmail = "";

        public EmailOTPModule(EmailService emailService, OTPDao otpDao)
        {
            this.emailService = emailService;
            this.otpDao = otpDao;
        }

        private string GenerateOTP()
        {
            int otp = random.Next(999999);
            return otp.ToString("D6");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Host == "dso.org.sg";
            }
            catch
            {
                return false;
            }
        }

        public string GenerateOTPEmail(string emailAddress)
        {
            if (!IsValidEmail(emailAddress))
            {
                return STATUS_EMAIL_INVALID;
            }

            string otp = GenerateOTP();
            string emailBody = "Your OTP code is " + otp + ". The code is valid for 1 minute.";
            try
            {
                otpDao.CreateOTP(emailAddress, otp);
                emailService.SendEmail(emailAddress, emailBody);
                userEmail = emailAddress;
                return STATUS_EMAIL_OK;
            }
            catch
            {
                return STATUS_EMAIL_FAIL;
            }
        }

        public string CheckOTP(IOStream input)
        {
            DateTime startTime = DateTime.Now;
            string expectedOTP = otpDao.GetOTP(userEmail);
            int tries = 10;
            string inputOtp = "";
            while (tries > 0)
            {
                TimeSpan timeElapsed = DateTime.Now - startTime;
                if (timeElapsed.TotalMinutes > 1)
                {
                    return STATUS_OTP_TIMEOUT;
                }
                Console.WriteLine("Enter OTP: ");
                Task<string> readOtpTask = Task.Run(() => input.ReadOTP());
                if (readOtpTask.Wait(TimeSpan.FromMinutes(1)))
                {
                    inputOtp = readOtpTask.Result;
                }
                else
                {
                    return STATUS_OTP_TIMEOUT;
                }

                if (inputOtp == expectedOTP)
                {
                    return STATUS_OTP_OK;
                }
                else
                {
                    tries--;
                    Console.WriteLine("Invalid OTP. " + tries + " tries remaining.");
                }
            }
            return STATUS_OTP_FAIL;
        }
    }
}
