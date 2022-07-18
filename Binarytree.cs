using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private class TreeNode<T> where T : IComparable
        {
            public T Value { get; set; }
            public TreeNode<T> Left { get; set; }
            public TreeNode<T> Right { get; set; }
            public int Weight { get; set; }
        }

        private TreeNode<T> root;

        public T TakeData()
            => root.Value;
        public void Add(T value)
        {
            if (root == null)
            {
                root = new TreeNode<T>() {Value = value};
                root.Weight++;
                return;
            }

            if (value.GetType() != root.Value.GetType()) throw new ArgumentException();
            FindFreeSpace(value, root);
        }

        private static void FindFreeSpace(T value, TreeNode<T> currentTreeNode)
        {
            while (true)
            {
                currentTreeNode.Weight++;
                if (value.CompareTo(currentTreeNode.Value) < 0)
                {
                    if (currentTreeNode.Left == null)
                    {
                        currentTreeNode.Left = new TreeNode<T>() {Value = value, Weight = 1};
                        return;
                    }
                    currentTreeNode = currentTreeNode.Left;
                }
                else
                {
                    if (currentTreeNode.Right == null)
                    {
                        currentTreeNode.Right = new TreeNode<T>() {Value = value, Weight = 1};
                        return;
                    }
                    currentTreeNode = currentTreeNode.Right;
                }
            }
        }

        public bool Contains(T value)
        {
            var currentNode = root;
            while (true)
            {
                if (currentNode == null) return false;
                if (Equals(currentNode.Value, value)) return true;
                currentNode = value.CompareTo(currentNode.Value) < 0 ? currentNode.Left : currentNode.Right;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 && index >= root.Weight)
                    throw new IndexOutOfRangeException();
                return TakeByIndex(root, index);
            }
        }

        private static T TakeByIndex(TreeNode<T> treeNode, int index)
        {
            if (index == (treeNode.Left != null ? treeNode.Left.Weight : 0)) return treeNode.Value;
            if (index < (treeNode.Left != null ? treeNode.Left.Weight : 0)) return TakeByIndex(treeNode.Left, index);
            return TakeByIndex(treeNode.Right, index - (treeNode.Left != null ? treeNode.Left.Weight + 1 : 1));
        }


        public IEnumerator<T> GetEnumerator()
        {
            return TakeElementOrderBy(root).GetEnumerator();
        }

        private IEnumerable<T> TakeElementOrderBy(TreeNode<T> treeNode)
        {
            if (treeNode == null) yield break;
            foreach (var comparable in TakeElementOrderBy(treeNode.Left))
                yield return comparable;

            yield return treeNode.Value;

            foreach (var comparable in TakeElementOrderBy(treeNode.Right))
                yield return comparable;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}