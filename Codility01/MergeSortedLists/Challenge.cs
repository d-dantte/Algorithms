using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.MergeSortedLists
{
	class Challenge
    {
        public static ListNode[] ToLists(int[][] lists)
        {
            return lists
                .Select(list =>
                {
                    ListNode firstNode = null;
                    list.Aggregate((ListNode)null, (acc, next) =>
                    {
                        var node = new ListNode(next);
                        if (acc != null)
                            acc.next = node;

                        else firstNode = node;

                        return node;
                    });

                    return firstNode;
                })
                .ToArray();
        }

        public static ListNode MergeKLists(ListNode[] lists)
        {
            BNode root = null;
            foreach (var list in lists)
            {
                if (list == null)
                    continue;

                var current = list;
                ListNode next = null;
                do
                {
                    next = current.next;
                    current.next = null;

                    if (root == null)
                        root = new BNode(current);

                    else root.Add(current);

                    current = next;
                }
                while (current != null);
            }

            if (root == null)
                return null;

            else
            {
                return root.Leftmost().StartVal;
            }
        }

        public class BNode
        {
            public ListNode StartVal { get; set; }
            public ListNode EndVal { get; set; }

            public int MaxLeft { get; set; }
            public int MinRight { get; set; }

            public BNode Left { get; set; }
            public BNode Right { get; set; }

            public BNode(ListNode val)
            {
                StartVal = EndVal = val;
            }

            public void Add(ListNode node)
            {
                if(node.val == StartVal.val)
                {
                    var next = EndVal.next;
                    EndVal.next = node;
                    EndVal = node;
                    EndVal.next = next;
                }

                //left
                else if (node.val < StartVal.val)
                {
                    if (Left == null)
                    {
                        Left = new BNode(node);
                        MaxLeft = node.val;
                        node.next = StartVal;
                    }
                    else
                    {
                        Left.Add(node);
                        if(node.val > MaxLeft)
                        {
                            MaxLeft = node.val;
                            node.next = StartVal;
                        }
                    }
                }

                //right
                else
                {
                    if (Right == null)
                    {
                        Right = new BNode(node);
                        MinRight = node.val;
                        EndVal.next = node;
                    }
                    else
                    {
                        Right.Add(node);
                        if (node.val < MinRight)
                        {
                            MinRight = node.val;
                            EndVal.next = node;
                        }
                    }
                }
            }

            public BNode Leftmost()
            {
                if (Left != null)
                    return Left.Leftmost();

                else return this;
            }

            public BNode Rightmost()
            {
                if (Right != null)
                    return Right.Rightmost();

                else return this;
            }
        }

        public class ListNode
        {
            public ListNode next;

            public int val;

            public ListNode(int val)
            {
                this.val = val;
            }

            public override string ToString() => $"[{val}]";


            public string Output() => $"[{val}{next?.ToNonHeadString()}]";

            private string ToNonHeadString() => $", {val}{next?.ToNonHeadString()}";
        }
    }
}
