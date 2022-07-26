using System;
using System.Collections;
using System.Collections.Generic;
using SimpleAkinator;

namespace BinaryTrees
{
    public class BinaryTree : IEnumerable<BinaryTree.TreeNode>
    {
        public class TreeNode
        {
            public string? Value { get; set; }
            public TreeNode? Left { get; set; }
            public TreeNode? Right { get; set; }
            public TreeNode? Parent { get; set; }
        }

        private TreeNode? _treeNode;
        private TreeNode? _root;
        public void Reset() => _treeNode = _root;
        public TreeNode? GetRoot() => _root;
        public TreeNode? CurData => _treeNode;
        public bool IsAnswer() => _treeNode?.Left == null && _treeNode?.Right == null;
        private bool IsRoot => _treeNode?.Parent == null;

        public BinaryTree(string defaultTxt)
        {
            _treeNode = new TreeNode() {Value = defaultTxt};
            _root = _treeNode;
        }

        public BinaryTree(TreeNode? root)
        {
            _treeNode = root;
            _root = _treeNode;
        }

        public void CreateNode(string? question, string? obj)
        {
            TreeNode newTreeNode;
            
            if (IsRoot)
                newTreeNode = _root = new TreeNode() {Value = question};

            else if (_treeNode.Parent.Left.Value == _treeNode?.Value)
                newTreeNode = _treeNode.Parent.Left = new TreeNode() {Value = question};

            else
                newTreeNode = _treeNode.Parent.Right = new TreeNode() {Value = question};

            newTreeNode.Left = new TreeNode {Value = obj};
            newTreeNode.Right = _treeNode;
        }


        public void GoTo(Direction dir)
        {
            var treeNode = _treeNode;
            switch (dir)
            {
                case Direction.Left:
                    _treeNode = _treeNode.Left;
                    break;
                case Direction.Right:
                    _treeNode = _treeNode.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            _treeNode.Parent = treeNode;
        }
        
        public IEnumerator<TreeNode> GetEnumerator()
        {
            return TakeElementOrderBy(_root).GetEnumerator();
        }

        private IEnumerable<TreeNode> TakeElementOrderBy(TreeNode treeNode)
        {
            if (treeNode == null) yield break;
            foreach (var comparable in TakeElementOrderBy(treeNode.Left))
                yield return comparable;

            yield return treeNode;

            foreach (var comparable in TakeElementOrderBy(treeNode.Right))
                yield return comparable;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}