using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using SimpleAkinator;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        public class TreeNode<T> where T : IComparable
        {
            public T Value { get; set; }
            public TreeNode<T> Left { get; set; }
            public TreeNode<T> Right { get; set; }
            public TreeNode<T> PrevTreeNode { get; set; }
            public int Weight { get; set; }
        }

        private TreeNode<T> _root;

        
        public T Data => _root.Value;
        public bool IsRootTree => _root.PrevTreeNode == null;
        public bool IsAnswer() => _root.Left == null;
        
        public void CreateNode(T question, T obj)
        {
            if (_root.PrevTreeNode.Left.Value.CompareTo(_root.Value) == 0)
            {
                _root.PrevTreeNode.Left = new TreeNode<T>() {Value = question};
                var newTreeNode = _root.PrevTreeNode.Left;
                newTreeNode.Left = new TreeNode<T> {Value = obj};
                newTreeNode.Right = _root;
            }
            else
            {
                _root.PrevTreeNode.Right = new TreeNode<T>() {Value = question};
                var newTreeNode = _root.PrevTreeNode.Right;
                newTreeNode.Left = new TreeNode<T>() {Value = obj};
                newTreeNode.Right = _root;
            }


        }
        
        public void GoTo(Direction dir)
        {
            var treeNode = _root;
            switch (dir)
            {
                case Direction.Left:
                    _root = _root.Left;
                    break;
                case Direction.Right:
                    _root = _root.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            _root.PrevTreeNode = treeNode;
        }

        public void ChangeRoot(T question, T obj)
        {
            var treeNode = _root;
            _root = new TreeNode<T>() { Value = question};
            _root.Left = new TreeNode<T>() {Value = obj};
            _root.Right = treeNode;
        }

        public void Add(T value)
        {
            if (_root == null)
            {
                _root = new TreeNode<T>() {Value = value};
                return;
            }

            if (value.GetType() != _root.Value.GetType()) throw new ArgumentException();
            FindFreeSpace(value, _root);
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
            var currentNode = _root;
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
                if (index < 0 && index >= _root.Weight)
                    throw new IndexOutOfRangeException();
                return TakeByIndex(_root, index);
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
            return TakeElementOrderBy(_root).GetEnumerator();
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