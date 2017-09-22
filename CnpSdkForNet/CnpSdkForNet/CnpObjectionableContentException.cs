using System;
using System.Collections.Generic;
using System.Text;

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
