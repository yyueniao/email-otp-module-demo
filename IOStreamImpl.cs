using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailOTPModule
{
    public class IOStreamImpl : IOStream
    {
        public string ReadOTP()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
