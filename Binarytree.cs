using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using SimpleAkinator;

namespace BinaryTrees
{
    public class BinaryTree : IEnumerable
    {
        public class TreeNode
        {
            public string Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode PrevTreeNode { get; set; }
            public int Weight { get; set; }
        }

        private TreeNode _root = new TreeNode() {Value = "Неизвестно что"};
        
        

        public string Data => _root.Value;
        public bool IsRoot => _root.PrevTreeNode == null;
        public bool IsAnswer() => _root.Left == null;
        
        public void CreateNode(string question, string obj)
        {
            if (IsRoot)
            {
                var treeNode = _root;
                _root = new TreeNode() { Value = question};
                _root.Left = new TreeNode() {Value = obj};
                _root.Right = treeNode;
                return;
            }
            
            if (_root.PrevTreeNode.Left.Value.CompareTo(_root.Value) == 0)
            {
                _root.PrevTreeNode.Left = new TreeNode() {Value = question};
                var newTreeNode = _root.PrevTreeNode.Left;
                newTreeNode.Left = new TreeNode {Value = obj};
                newTreeNode.Right = _root;
            }
            else
            {
                _root.PrevTreeNode.Right = new TreeNode() {Value = question};
                var newTreeNode = _root.PrevTreeNode.Right;
                newTreeNode.Left = new TreeNode() {Value = obj};
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
        
        
        

        public bool Contains(string value)
        {
            var currentNode = _root;
            while (true)
            {
                if (currentNode == null) return false;
                if (Equals(currentNode.Value, value)) return true;
                currentNode = value.CompareTo(currentNode.Value) < 0 ? currentNode.Left : currentNode.Right;
            }
        }
        
        public IEnumerator GetEnumerator()
        {
            return TakeElementOrderBy(_root).GetEnumerator();
        }

        private IEnumerable TakeElementOrderBy(TreeNode treeNode)
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

        public void GoToStart()
        {
            while (_root.PrevTreeNode != null)
                _root = _root.PrevTreeNode;
        }
    }
}