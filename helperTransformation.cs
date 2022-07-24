using System.Linq;
using System.Reflection;
using System.Text;
using BinaryTrees;

namespace SimpleAkinator
{
    public class helperTransformation
    {
        private readonly BinaryTree _binaryTree;
        public string SerializeData { get; private set; }

        public helperTransformation(BinaryTree binaryTree)
        {
            _binaryTree = new BinaryTree(binaryTree.GetRoot());
        }

        public void MakeSerialize()
        {
            var treeNode = _binaryTree.GetRoot();
            var stringBulder = new StringBuilder();
            Serialize(treeNode, stringBulder);
            SerializeData = stringBulder.ToString();
        }

        private void Serialize(BinaryTree.TreeNode treeNode, StringBuilder builder)
        {
            if (treeNode == null) return;

            if (treeNode.Left == null && treeNode.Right == null)
                builder.Append(treeNode.Value + "/");

            else
                builder.Append(treeNode.Value + "/");

            Serialize(treeNode.Left, builder);
            Serialize(treeNode.Right, builder);
        }

        public void MakeDeserialize(string[] data)
        {
            _index = 0;
            Deserialize(data,null);
        }

        private string[] CreateArrayTree(string[] data)
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

        private int _index = 0; 
        private BinaryTree.TreeNode Deserialize(string[] data, BinaryTree.TreeNode parent)
        {
            BinaryTree.TreeNode newTreeNode;
            
            if (_index >= data.Length) return null;
            
            newTreeNode = _index == 0 ? _binaryTree.GetRoot() : new BinaryTree.TreeNode();
            
            newTreeNode.Parent = parent;
            newTreeNode.Value = data[_index];
            
            if (data[_index].Last() == '?')
            {
                _index++;
                newTreeNode.Left = Deserialize(data,newTreeNode);
                _index++;
                newTreeNode.Right = Deserialize(data,newTreeNode);
            }
            return newTreeNode;
        }
    }
}



/*var parent = treeNode;
            treeNode.Value = data[0];
            treeNode.Left = new BinaryTree.TreeNode();
            treeNode.Right = new BinaryTree.TreeNode();
            for (var j = 1; j < data.Length - 1; j++)
            {
                if (data[j].Last() == '?')
                {
                    treeNode.Value = data[j];
                    treeNode.Left = new BinaryTree.TreeNode();
                    treeNode.Right = new BinaryTree.TreeNode();
                    parent = treeNode;
                    treeNode = treeNode.Left;
                    treeNode.Parent = parent;
                }
                else
                {
                    treeNode.Value = data[j];
                }
            }

            treeNode.Parent = parent;*/

/*if (data[curNodeSide] == null) return;
            if (data[curNodeSide].Last() == '?')
            {
                treeNode.Value = data[curNodeSide];
                treeNode.Left = new BinaryTree.TreeNode();
                treeNode.Right = new BinaryTree.TreeNode();
                Deserialize(treeNode.Left, data, curNodeSide * 2 + 1, treeNode);
                Deserialize(treeNode.Right, data, curNodeSide * 2, treeNode);
            }
            else
            {
                treeNode.Value = data[curNodeSide];
            }
                

            treeNode.Parent = parent;*/


/*var nameFile = "Firsttest.txt";
var path = @"C:\Users\user\RiderProjects\SimpleAkinator\SimpleAkinator\Date\" + nameFile;
FileInfo fileInfo = new FileInfo(path);*/

/*if (!fileInfo.Exists)
            {
                using var dir = File.CreateText(path);
                dir.WriteLine(data);
            }*/
/*else
{
    using StreamWriter sw = new StreamWriter(path);
    sw.WriteLine(data);
}*/