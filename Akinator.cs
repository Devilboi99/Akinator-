using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BinaryTrees;

namespace SimpleAkinator
{
    public class Akinator
    {
        private BinaryTree<string> Root;

        public Akinator()
        {
            Root.Add("Неизвестно что?");
        }

        public void Start()
        {
            Console.WriteLine("Что ты хочешь? [1] - игры, [2] - сохранить базу");
            var chose = Console.ReadLine();
            switch (chose)
            {
                case "1":
                    ActivateAkinator();
                    break;
                case "2":
                    SaveDate();
                    break;
            }
        }

        public void ActivateAkinator()
        {
            Console.WriteLine("Добро пожаловать");
            FindAnswer();
        }

        private void FindAnswer()
        {
            var treeNode = Root;
            var prevTreeNode = treeNode;
            while (true)
            {
                Console.WriteLine(treeNode.Date);
                var answerFromHumanYN = Console.ReadLine()?.ToLower();
                if (treeNode.IsAnswer)
                    EndProgram(treeNode, prevTreeNode, answerFromHumanYN);

                switch (answerFromHumanYN)
                {
                    case "yes":
                        prevTreeNode = treeNode;
                        treeNode = treeNode.Left;
                        continue;
                    case "no":
                        prevTreeNode = treeNode;
                        treeNode = treeNode.Right;
                        continue;
                    default:
                        throw new ArgumentException($"Такое я не понимать: {answerFromHumanYN}");
                }
            }
        }

        private void EndProgram(BinaryTree.TreeNode treeNode, BinaryTree.TreeNode prevTreeNode,
            string? answerFromHumanYN)
        {
            switch (answerFromHumanYN)
            {
                case "no":
                    CreateNewNode(treeNode, prevTreeNode);
                    Start();
                    break;
                case "yes":
                    Console.WriteLine("I AM God");
                    Start();
                    break;
            }
        }

        public void CreateNewNode(BinaryTree.TreeNode treeNode, BinaryTree.TreeNode prevTreeNode)
        {
            Console.WriteLine("Что это?");
            var obj = Console.ReadLine();
            Console.WriteLine($"Чем оно отличается от {treeNode.Date}");
            var question = Console.ReadLine();
            BinaryTree.TreeNode newTreeNode;
            if (prevTreeNode.Date == treeNode.Date)
            {
                Root = new BinaryTree.TreeNode() {Date = question};
                Root.Left = new BinaryTree.TreeNode() {Date = obj};
                Root.Right = treeNode;
                return;
            }

            if (prevTreeNode.Left.Date == treeNode.Date)
            {
                prevTreeNode.Left = new BinaryTree.TreeNode() {Date = question};
                newTreeNode = prevTreeNode.Left;
            }

            else
            {
                prevTreeNode.Right = new BinaryTree.TreeNode() {Date = question};
                newTreeNode = prevTreeNode.Right;
            }

            newTreeNode.Left = new BinaryTree.TreeNode() {Date = obj};
            newTreeNode.Right = treeNode;
        }

        public void SaveDate()
        {
            Console.WriteLine("Дай название базе");
            var nameDate = Console.ReadLine();
        }

        public void Serialization()
        {
            foreach (var treeNode in)
            {
            }
        }
    }
}