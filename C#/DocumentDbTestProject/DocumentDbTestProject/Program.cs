using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


namespace DocumentDbTestProject
{
    class Program
    {
        private static string EndpointUrl = "<your endpoint URI>";
        private static string AuthorizationKey = "<your key>";

        


        static void CreateDocument(DocumentClient client)
        {
            client.CreateDocumentAsync()
        }
     
        static void Main(string[] args)
        {
            // Create a new instance of the DocumentClient.
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);  

        }
    }
}
