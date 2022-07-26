using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BinaryTrees;

namespace SimpleAkinator
{
    public class Akinator
    {
        private readonly BinaryTree _binaryTree;

        public Akinator(BinaryTree binaryTree)
        {
            _binaryTree = binaryTree;
        }

        public void Start()
        {
            Console.WriteLine("Что ты хочешь? [1] - игры, [2] - настройка базы, [3] - Определить");
            var chose = Console.ReadLine();
            _binaryTree.Reset();
            switch (chose)
            {
                case "1":
                    ActivateGame();
                    break;
                case "2":
                    OpenSettings();
                    break;
                case "3":
                    WritePath();
                    break;
            }
        }

        private void WritePath()
        {
            var stack =  DetectElement();
            if (stack == null)
            {
                Console.WriteLine("Такого нету");
                Start();
            }
            
            var prevElement = stack?.Pop();
            BinaryTree.TreeNode curElement;
            var stringBuilder = new StringBuilder();
            while (stack.Count != 0)
            {
                curElement = stack.Pop();
                
                if (prevElement.Left == curElement)
                    stringBuilder.Append(prevElement.Value + ", ");
                else
                    stringBuilder.Append("не " + prevElement.Value + ", ");

                prevElement = curElement;
            }
            
            stringBuilder.Append("это - " + prevElement.Value);
            
            Console.WriteLine(stringBuilder.ToString()); 
            Start();
        }

        private Stack<BinaryTree.TreeNode>? DetectElement()
        {
            Console.WriteLine("скажи напиши объекта");
            var nameObj = Console.ReadLine() + "!";
            var stack = new Stack<BinaryTree.TreeNode>();
            foreach (var element in _binaryTree)
            {
                if (element.Right == null && element.Left == null && element.Value == nameObj)
                {
                    var treeNode = element;
                    while (treeNode != null)
                    {
                        stack.Push(treeNode);
                        treeNode = treeNode.Parent;
                    }

                    return stack;
                }
            }

            return null;
        }

        private void OpenSettings()
        {
            Console.WriteLine("Хочешь [1] соханить базу или [2] загрузить?");
            var chose = Console.ReadLine();
            switch (chose)
            {
                case "1":
                    SaveData();
                    break;
                case "2":
                    LoadData();
                    break;
                default:
                    Console.WriteLine("я не понимать");
                    break;
            }
        }

        private void ActivateGame()
        {
            Console.WriteLine("Добро пожаловать");
            while (true)
            {
                Console.WriteLine(_binaryTree.CurData?.Value);
                var answerFromHumanYn = Console.ReadLine()?.ToLower();

                if (_binaryTree.IsAnswer())
                    CheckAnswer(answerFromHumanYn);

                switch (answerFromHumanYn)
                {
                    case "да":
                        _binaryTree.GoTo(Direction.Left);
                        continue;
                    case "нет":
                        _binaryTree.GoTo(Direction.Right);
                        continue;
                    default:
                        Console.WriteLine("Либо да или нет?");
                        continue;
                }
            }
        }

        private void CheckAnswer(string? answerFromHumanYn)
        {
            switch (answerFromHumanYn)
            {
                case "нет":
                    AddNewObject();
                    Start();
                    break;
                case "да":
                    Console.WriteLine("I AM God");
                    Start();
                    break;
            }
        }

        public void AddNewObject()
        {
            Console.WriteLine("Что это?");
            var obj = Console.ReadLine() + "!";

            Console.WriteLine($"Чем оно отличается от {_binaryTree.CurData?.Value}");
            var question = Console.ReadLine() + "?";


            _binaryTree.CreateNode(question, obj);
        }

        public void SaveData()
        {
            var helper = new HelperTransformation(_binaryTree);
            helper.MakeSerialize();
            helper.CreateFileTxt(helper.SerializeData);

            Start();
        }

        private void LoadData()
        {
            var helper = new HelperTransformation(_binaryTree);
            helper.FindFile();

            if (helper.SerializeData != null)
                helper.MakeDeserialize(helper.SerializeData.Split('/'));
            else
                Console.WriteLine("Такой нету");

            Start();
        }
    }
}