using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.Helpers.DataStructures
{

	public class BTreeNode<TValue>
	where TValue : IComparable<TValue>
	{
		private readonly List<TValue> _duplicates;
		private readonly bool _allowsDuplicates;

		public TValue Value { get;  }

		public BTreeNode<TValue> Left { get; set; }

		public BTreeNode<TValue> Right { get; set; }

		public bool IsBalanced
		{
			get
			{

				var bf = BalanceFactor();
				return bf >= -1 && bf <= 1;
			}
		}

		public bool HasDuplicates => _duplicates.Count > 0;


		public BTreeNode(TValue value, bool allowsDuplicates = false)
		{
			Value = value;
			_allowsDuplicates = allowsDuplicates;

			if (_allowsDuplicates)
				_duplicates = new List<TValue>();
		}


		public BTreeNode<TValue> RightmostChild(out BTreeNode<TValue> parent)
		{
			parent = null;
			if (Right == null)
				return this;

			else
			{
				var rightmost = Right.RightmostChild(out parent);
				if (rightmost == Right)
					parent = this;

				return rightmost;
			}
		}

		public BTreeNode<TValue> LeftmostChild(out BTreeNode<TValue> parent)
		{
			parent = null;
			if (Left == null)
				return this;

			else
			{
				var leftmost = Left.LeftmostChild(out parent);
				if (leftmost == Left)
					parent = this;

				return leftmost;
			}
		}

		public BTreeNode<TValue> FindChild(TValue value, out BTreeNode<TValue> parent)
		{
			parent = this;
			var comparison = Value.CompareTo(value);

			switch(comparison)
			{
				case -1:
					return 
						Left == null ? null
						: Left.ValueEquals(value) ? Left
						: Left.FindChild(value, out parent);

				case 1:
					return
						Right == null ? null
						: Right.ValueEquals(value) ? Right
						: Right.FindChild(value, out parent);

				default:
					return null;
			}
		}

		public BTreeNode<TValue> Add(TValue value)
		{
			var comparison = Value.CompareTo(value);
			if (comparison < 0)
			{
				if (Left == null)
					return Left = new BTreeNode<TValue>(value);

				else
					return Left.Add(value);
			}
			else if (comparison == 0)
			{
				if (!_allowsDuplicates)
					throw new Exception("Duplicate encountered");

				_duplicates.Add(value);
				return this;
			}
			else //if (comparison > 0)
			{
				if (Right == null)
					return Right = new BTreeNode<TValue>(value);

				else
					return Right.Add(value);
			}
		}

		public BTreeNode<TValue> Remove(TValue value)
		{
			var child = FindChild(value, out var parent);
			if (child == null)
				return null;

			else if(parent.Left == child)
			{
				//get childs rightmost
				var rightmost = child.RightmostChild(out var _rightmostParent);
				if (rightmost != null)
				{
					_rightmostParent.Right = null;
					parent.Left = rightmost;
				}
				else parent.Left = child.Left;
			}
			else //if(parent.Right == child)
			{
				var leftmost = child.LeftmostChild(out var _leftmostParent);
				if (leftmost != null)
				{
					_leftmostParent.Left = null;
					parent.Right = leftmost;
				}
				else parent.Right = child.Right;
			}

			return child;
		}

		/// <summary>
		/// left rotation
		/// </summary>
		/// <param name="child"></param>
		public void RotateRightChild(BTreeNode<TValue> parent)
		{
			if(Right != null)
			{
				var pivot = Right;
				if (parent.Left == this)
					parent.Left = pivot;

				else
					parent.Right = pivot;

				Right = pivot.Left;
				pivot.Left = this;
			}
		}

		/// <summary>
		/// right rotation
		/// </summary>
		/// <param name="child"></param>
		public void RotateLeftChild(BTreeNode<TValue> parent)
		{
			if (Left != null)
			{
				var pivot = Left;
				if (parent.Left == this)
					parent.Left = pivot;

				else
					parent.Right = pivot;

				Left = pivot.Right;
				pivot.Right = this;
			}
		}

		public void Balance()
		{
			var balanceFactor = BalanceFactor();

			if (balanceFactor < -2)
				Left.Balance();

			else if (balanceFactor > 2)
				Right.Balance();
		}


		public bool ValueEquals(TValue value) => Value.CompareTo(value) == 0;

		public int Height() => Math.Max(
			(Left?.Height() + 1) ?? 0,
			(Right?.Height() + 1) ?? 0);

		public int BalanceFactor() => (Right?.Height() ?? 0) - (Left?.Height() ?? 0);
	}
}
