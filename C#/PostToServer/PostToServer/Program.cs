using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PostToServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var URL = "";
            var CONTENTTYPE = "";
            var jsonStmt = "";

            WebRequest req = WebRequest.Create(URL);
            //req.Headers.Add("Authorization", "Basic " + encodedAuth);

            req.ContentType = CONTENTTYPE;
            req.Method = "POST";

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(jsonStmt);
                streamWriter.Flush();
            }

            WebResponse resp = req.GetResponse();
            string responseJson = "";
            using (var streamReader = new StreamReader(resp.GetResponseStream()))
            {
                responseJson = streamReader.ReadToEnd();
            }
            List<String> IDList = JsonConvert.DeserializeObject<List<String>>(responseJson);
            

        }
    }
}
