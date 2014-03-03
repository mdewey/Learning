using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTree
{
    class Program
    {
        public static class TreeToEnumerableEx
        {
            public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
            {
                yield return head;
                foreach (var node in childrenFunc(head))
                {
                    foreach (var child in AsDepthFirstEnumerable(node, childrenFunc))
                    {
                        yield return child;
                    }
                }
            }

            public static IEnumerable<T> AsBreadthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
            {
                yield return head;
                var last = head;
                foreach (var node in AsBreadthFirstEnumerable(head, childrenFunc))
                {
                    foreach (var child in childrenFunc(node))
                    {
                        yield return child;
                        last = child;
                    }
                    if (last.Equals(node)) yield break;
                }
            }
        }
        public class Node
        {
            private readonly List<Node> children = new List<Node>();

            public Node(int id)
            {
                Id = id;
            }

            public IEnumerable<Node> Children { get { return children; } }

            public Node AddChild(int id)
            {
                var child = new Node(id);
                children.Add(child);
                return child;
            }

            public int Id { get; private set; }
        }


        static void Main(string[] args)
        {
            // build the tree in breadth-first order
            var id = 1;
            var depthFirst  = new Node(id);
            var bf2 = depthFirst.AddChild(++id);
            var bf3 = depthFirst.AddChild(++id);
            var bf4 = bf2.AddChild(++id);
            var bf5 = bf2.AddChild(++id);
            var bf6 = bf3.AddChild(++id);
            var bf7 = bf3.AddChild(++id);

           // find all nodes in depth-first order and select just the Id of each node  
            var IDs = from node in depthFirst.AsDepthFirstEnumerable(x => x.Children)
                      select node.Id;

        }
    }
}
