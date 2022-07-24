using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BinaryTrees;

namespace SimpleAkinator
{
    public class HelperTransformation
    {
        private readonly BinaryTree _binaryTree;
        public string? SerializeData { get; private set; }

        public HelperTransformation(BinaryTree binaryTree)
        {
            _binaryTree = new BinaryTree(binaryTree.GetRoot());
        }

        public void MakeSerialize()
        {
            var treeNode = _binaryTree.GetRoot();
            var stringBuilder = new StringBuilder();
            Serialize(treeNode, stringBuilder);
            SerializeData = stringBuilder.ToString();
        }

        private void Serialize(BinaryTree.TreeNode? treeNode, StringBuilder builder)
        {
            if (treeNode == null) return;

            builder.Append(treeNode.Value + "/");

            Serialize(treeNode.Left, builder);
            Serialize(treeNode.Right, builder);
        }

        public void MakeDeserialize(string[] data)
        {
            _index = 0;
            Deserialize(data, null);
        }

        public string[] CreateArrayTree(string[] data) // просто красивый метод
        {
            var treeArray = new string[data.Length + 10];
            var goLeft = true;
            var rightElement = 0;
            var leftElement = 1;
            treeArray[1] = data[0];
            for (var j = 1; j < data.Length - 1; j++)
            {
                if (goLeft)
                {
                    treeArray[leftElement += 2] = data[j];
                    if (data[j].Last() == '!') goLeft = false;
                }
                else
                {
                    treeArray[rightElement += 2] = data[j];
                    goLeft = data[j].Last() != '!';
                }
            }

            return treeArray;
        }

        private int _index;

        private BinaryTree.TreeNode? Deserialize(IReadOnlyList<string> data, BinaryTree.TreeNode? parent)
        {
            if (_index >= data.Count) return null;

            var newTreeNode = _index == 0 ? _binaryTree.GetRoot() : new BinaryTree.TreeNode();

            newTreeNode!.Parent = parent;
            newTreeNode.Value = data[_index];

            if (data[_index].Last() == '?')
            {
                _index++;
                newTreeNode.Left = Deserialize(data, newTreeNode);
                _index++;
                newTreeNode.Right = Deserialize(data, newTreeNode);
            }

            return newTreeNode;
        }

        public void CreateFileTxt(string? data)
        {
            Console.WriteLine("Дай название базе");
            var nameFile = Console.ReadLine() + ".txt";
            var path = @"C:\Users\user\RiderProjects\SimpleAkinator\SimpleAkinator\Date\" + nameFile;
            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                using var dir = File.CreateText(path);
                dir.WriteLine(data);
            }
            else
            {
                using StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(data);
            }
        }

        public void FindFile()
        {
            Console.WriteLine("Как называется база?");
            var nameData = Console.ReadLine() + ".txt";
            var path = @"C:\Users\user\RiderProjects\SimpleAkinator\SimpleAkinator\Date\" + nameData;
            var fileInfo = new FileInfo(path);

            SerializeData = fileInfo.Exists ? fileInfo.OpenText().ReadToEnd() : null;
        }
    }
}