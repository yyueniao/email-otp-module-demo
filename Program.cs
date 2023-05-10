namespace EmailOTPModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your email address: ");
            string emailAddress = Console.ReadLine() ?? "";

            EmailOTPModule module = new(new EmailService(), new OTPDao());

            string status = module.GenerateOTPEmail(emailAddress);
            if (status == EmailOTPModule.STATUS_EMAIL_INVALID)
            {
                Console.WriteLine("Invalid email address!");
                return;
            }
            else if (status == EmailOTPModule.STATUS_EMAIL_FAIL)
            {
                Console.WriteLine("Failed to send email.");
                return;
            }

            Console.WriteLine("OTP sent to email. Please enter OTP within 1 minute.");
            IOStream input = new IOStreamImpl();
            status = module.CheckOTP(input);
            if (status == EmailOTPModule.STATUS_OTP_TIMEOUT)
            {
                Console.WriteLine("OTP validation timeout!");
                return;
            }
            else if (status == EmailOTPModule.STATUS_OTP_FAIL)
            {
                Console.WriteLine("Invalid OTP. OTP validation failed.");
                return;
            }

            Console.WriteLine("OTP validation successful. Continuing...");

        }
    }
}