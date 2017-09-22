using System;
using System.Collections.Generic;
using System.Text;

namespace Cnp.Sdk
{
    public class CnpInvalidCredentialException : Exception
    {
        public CnpInvalidCredentialException(string message) : base(message)
        {
            
        }

        public CnpInvalidCredentialException(string message, Exception e) : base(message, e)
        {

        }
    }
}
