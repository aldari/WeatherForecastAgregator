using System;
using System.IO;
using System.Net;

namespace Services
{
    public class QueryLoader: IQueryLoader
    {
        public String LoadData(String uri)
        {
            var req = WebRequest.Create(uri);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            if (stream == null) 
                return null;

            var streamReader = new StreamReader(stream);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            return result;
        }
    }
}
