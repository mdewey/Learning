using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jExplorations
{
    // TODO: Work on optionality 
    class Program
    {

        public class LearningObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class OptionalityData
        {
            public int ID { get; set; }
            public int NumberRequired { get; set; }

            public OptionalityData()
            {

            }
            public OptionalityData(int num)
            {
                this.ID = num;
            }

        }

        public class ChildRelationship : Relationship, IRelationshipAllowingSourceNode<LearningObject>, IRelationshipAllowingTargetNode<LearningObject>
        {
            public static readonly string TypeKey = "HAS";
            public ChildRelationship(NodeReference targetNode)
                : base(targetNode)
            { }
            public override string RelationshipTypeKey
            {
                get { return TypeKey; }
            }
        }

        public class OptionalGroupRelationship : Relationship, IRelationshipAllowingSourceNode<LearningObject>, IRelationshipAllowingTargetNode<LearningObject>
        {
            public static readonly string TypeKey = "HAS";
            public OptionalGroupRelationship(NodeReference targetNode, OptionalityData data)
                : base(targetNode, data)
            { }
            public override string RelationshipTypeKey
            {
                get { return TypeKey; }
            }
        }

      
        //public class HatesRelationship : Relationship<HatesData>, IRelationshipAllowingSourceNode<Person>, IRelationshipAllowingTargetNode<Person>
        //{
        //    public static readonly string TypeKey = "HATES";

        //    public HatesRelationship(NodeReference targetNode, HatesData data)
        //        : base(targetNode, data)
        //    { }

        //    public override string RelationshipTypeKey
        //    {
        //        get { return TypeKey; }
        //    }
        //}

        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            // Create entities
            var refA = client.Create(new LearningObject() { Name = "Person A" });
            var refB = client.Create(new LearningObject() { Name = "Person B" });
            var refC = client.Create(new LearningObject() { Name = "Person C" });
            var refD = client.Create(new LearningObject() { Name = "Person D" });
            var refE = client.Create(new LearningObject() { Name = "Person E" });
            var refF = client.Create(new LearningObject() { Name = "Person F" });


            // Create relationships
            client.CreateRelationship(refA, new ChildRelationship(refB));
            client.CreateRelationship(refB, new ChildRelationship(refC));
            client.CreateRelationship(refF, new ChildRelationship(refC));
            client.CreateRelationship(refC, new ChildRelationship(refD));
            client.CreateRelationship(refC, new ChildRelationship(refE));

            var myNode = client.Get<LearningObject>(refA);
            var bNode = client.Get<LearningObject>((NodeReference)refB.Id);

            var query = client
                .Cypher
                .Start(new { root = refA })
                .Match("root-[:HAS]->badge-[:HAS]->activity-[:HAS]->step")
                .Return<Node<LearningObject>>("step");

            var results = query.Results;

            client.Delete(refA, DeleteMode.NodeAndRelationships);
            client.Delete(refB, DeleteMode.NodeAndRelationships);
            client.Delete(refC, DeleteMode.NodeAndRelationships);
            client.Delete(refD, DeleteMode.NodeAndRelationships);
        }
    }
}
