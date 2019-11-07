using System;

namespace Cnp.Sdk
{
    public class CnpObjectionableContentException : Exception
    {
        public CnpObjectionableContentException(string message) : base(message)
        {
            
        }

        public CnpObjectionableContentException(string message, Exception e) : base(message, e)
        {

        }
    }
}
