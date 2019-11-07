using System;

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
