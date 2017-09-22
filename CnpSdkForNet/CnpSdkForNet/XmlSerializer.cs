using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Cnp.Sdk
{
    public class cnpXmlSerializer
    {
        virtual public String SerializeObject(cnpOnlineRequest req)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(cnpOnlineRequest));
            MemoryStream ms = new MemoryStream();
            try
            {
                serializer.Serialize(ms, req);
            }
            catch (XmlException e)
            {
                throw new CnpOnlineException("Error in sending request to Cnp!", e);
            }
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        virtual public cnpResponse DeserializeObjectFromFile(string filePath)
        {
            cnpResponse i;
            try
            {
                i = new cnpResponse(filePath);
            }
            catch (XmlException e)
            {
                throw new CnpOnlineException("Error in recieving response from Cnp!", e);
            }
            return i;
        }// deserialize the object
    }
}
