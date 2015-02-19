using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


namespace DocumentDbTestProject
{
    class Program
    {
        private static string EndpointUrl = "https://test-database.documents.azure.com:443/";
        private static string AuthorizationKey = "PMncAniLRoHWE8le9yIe6Bk/sExeQegYwPZUeQjVQPxwT30uvYtk+B5K0cibaNRiV4nYnmEePVWm6y1hmu0yDg==";

        private static string DbName = "MyFamiles";

        private class FamilyMemeber
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
        }

        private class Family
        {
            public string id { get; set; }

            public IEnumerable<FamilyMemeber> Parents { get; set; }
            public IEnumerable<FamilyMemeber> Childrend { get; set; }
            public Dictionary<string,string> Address { get; set; }
            public bool IsHome { get; set; }
            public string Name { get; set; }
        }
        public static async Task<HttpStatusCode> CreateDatabase(DocumentClient client)
        {
            var database = await client.CreateDatabaseAsync(new Database() { Id =  DbName});
            var test = database.StatusCode;
            return test;
        }

        public static async Task<HttpStatusCode> CreateCollection(DocumentClient client, Database database, string collectionName)
        {
            var result = await client.CreateDocumentCollectionAsync(database.CollectionsLink, new DocumentCollection
            {
                Id = collectionName
            });
            return result.StatusCode;
        }

        public static Document GetDocumentById(DocumentClient client, DocumentCollection collection, string id)
        {
            return client.CreateDocumentQuery(collection.DocumentsLink).Where(w => w.Id == id).AsEnumerable().FirstOrDefault();
        }

        public async static Task GoOnVacation(DocumentClient client, DocumentCollection collection, string jeffersons, bool left)
        {
            var family = GetFamily(client, collection, jeffersons);
            dynamic doc = GetDocumentById(client, collection,family.id);
            Family f = doc;
            f.IsHome = false;
            var task = await client.ReplaceDocumentAsync(doc.SelfLink, f);
            
        }


        public static async Task AddFamily(DocumentClient client,DocumentCollection collection, string name)
        {
            var family = new Family
            {
                Address = new Dictionary<string, string> {{"state", "fl"}},
                Childrend = new List<FamilyMemeber>
                {
                    new FamilyMemeber {Age = 10, Gender = "female", Name = "susy"},
                    new FamilyMemeber {Age = 8, Gender = "male", Name = "jimmy"}
                },
                IsHome = true,
                Parents = new List<FamilyMemeber>
                {
                    new FamilyMemeber {Age = 45, Gender = "female", Name = "Linda"},
                    new FamilyMemeber {Age = 40, Gender = "male", Name = "Bob"}
                },
                Name = name
            };
            await client.CreateDocumentAsync(collection.DocumentsLink, family);
        }

        public static Database GetDatabase(DocumentClient client, string databaseName)
        {
            return client.CreateDatabaseQuery().Where(db => db.Id == databaseName).AsEnumerable().FirstOrDefault();
        }

        private static Family GetFamily(DocumentClient client, DocumentCollection collection, string needle)
        {
            return client.CreateDocumentQuery<Family>(collection.DocumentsLink).Where(w => w.Name == needle).AsEnumerable().FirstOrDefault();
        }

        public static DocumentCollection GetCollection(DocumentClient client, Database db, string collectionName)
        {
            return
                client.CreateDocumentCollectionQuery(db.CollectionsLink)
                    .Where(w => w.Id == collectionName)
                    .AsEnumerable()
                    .FirstOrDefault();
        }

        public static Task DeleteFamily(DocumentClient client, DocumentCollection collection, string needle)
        {
             var family = GetFamily(client, collection, needle);
            dynamic doc = GetDocumentById(client, collection,family.id);
            var task = client.DeleteDocumentAsync(doc.SelfLink);
            return task;
        }

        static void Main(string[] args)
        {
            // Create a new instance of the DocumentClient.
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            var collectionName = "Family";
            var database = GetDatabase(client, DbName);
            var collection = GetCollection(client, database, collectionName);
            var fam = GetFamily(client, collection, "Jeffersons");
            var task = GoOnVacation(client, collection, "Jeffersons",true);
            task.Wait();
            var fam2 = GetFamily(client, collection, "Jeffersons");
            var passed = fam.IsHome || fam2.IsHome;
            var x = passed;
            var bye = DeleteFamily(client, collection, "Clintons");
            bye.Wait();
            //var task = AddFamily(client, collection, "Jeffersons");
            //task.Wait();
            //task = AddFamily(client, collection, "Doles");
            //task.Wait();
            //task = AddFamily(client, collection, "Clintons");
            //task.Wait();
            //task = AddFamily(client, collection, "Smiths");
            //task.Wait();
            //task = AddFamily(client, collection, "Jones");
            //task.Wait();
        }
    }
}
