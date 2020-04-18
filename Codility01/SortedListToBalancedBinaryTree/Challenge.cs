using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.SortedListToBalancedBinaryTree
{

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    public class Solution
    {
        public TreeNode SortedListToBST(ListNode head)
        {
            var values = ExtractValues(head);

            return Balance(values, 0, values.Count);
        }

        public TreeNode Balance(List<int> values, int start, int count)
        {
            if (count == 0)
                return null;

            else if (count == 1)
                return new TreeNode(values[start]);

            else
            {
                var mid = Math.DivRem(count, 2, out var rem);
                var node = new TreeNode(values[mid + start])
                { 
                    //right side
                    right = Balance(values, mid + start + 1, mid - (rem > 0 ? 0 : 1)),

                    //left side
                    left = Balance(values, start, mid)
                };

                return node;
            }
        }

        public List<int> ExtractValues(ListNode head)
        {
            var list = new List<int>();
            var node = head;
            while (node != null)
            {
                list.Add(node.val);
                node = node.next;
            }

            return list;
        }
    }
}
