using System.Linq;
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
                builder.Append(treeNode.Value + "!/");
            
            else
                builder.Append(treeNode.Value + "?/");

            Serialize(treeNode.Left, builder);
            Serialize(treeNode.Right, builder);
        }

        public void MakeDeserialize(string[] data)
        {
            var treeNode = _binaryTree.GetRoot();
            Deserialize(treeNode, data, 1, null);
        }

        private void Deserialize(BinaryTree.TreeNode treeNode, string[] data, int element, BinaryTree.TreeNode parent)
        {
            if (data[element].Last() == '?')
            {
                treeNode.Value = data[element];
                treeNode.Left = new BinaryTree.TreeNode();
                treeNode.Right = new BinaryTree.TreeNode();
                Deserialize(treeNode.Left, data, element * 2 + 1, treeNode);
                Deserialize(treeNode.Right, data, element * 2, treeNode);
            }
            else
                treeNode.Value = data[element];
            
            treeNode.Parent = parent;
        }
    }
}


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