using System;
using System.Collections.Generic;
using System.Text;

namespace Cnp.Sdk
{
    public class CnpConnectionLimitExceededException : Exception
    {
        public CnpConnectionLimitExceededException(string message) : base(message)
        {
            
        }

        public CnpConnectionLimitExceededException(string message, Exception e) : base(message, e)
        {

        }
    }
}
