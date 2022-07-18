using System;
using BinaryTrees;

namespace SimpleAkinator
{
    public class Akinator
    {
        private BinaryTree BinaryTree;

        public Akinator()
        {
            BinaryTree = new BinaryTree();
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
            BinaryTree.GoToStart();
            while (true)
            {
                Console.WriteLine(BinaryTree.Data);
                var answerFromHumanYN = Console.ReadLine()?.ToLower();
                if (BinaryTree.IsAnswer())
                    CompletedFind(answerFromHumanYN);

                switch (answerFromHumanYN)
                {
                    case "yes":
                       BinaryTree.GoTo(Direction.Left);
                       continue;
                    case "no":
                        BinaryTree.GoTo(Direction.Right); ;
                        continue;
                    default:
                        throw new ArgumentException($"Такое я не понимать: {answerFromHumanYN}");
                }
            }
        }

        private void CompletedFind(string? answerFromHumanYN)
        {
            switch (answerFromHumanYN)
            {
                case "no":
                    AddNewObject();
                    Start();
                    break;
                case "yes":
                    Console.WriteLine("I AM God");
                    Start();
                    break;
            }
        }

        public void AddNewObject()
        {
            Console.WriteLine("Что это?");
            var obj = Console.ReadLine();
            Console.WriteLine($"Чем оно отличается от {BinaryTree.Data}");
            var question = Console.ReadLine();
            BinaryTree.CreateNode(question,obj);
        }

        public void SaveDate()
        {
            Console.WriteLine("Дай название базе");
            var nameDate = Console.ReadLine();
        }

        public void Serialization()
        {
            foreach (var treeNode in BinaryTree)
            {
            }
        }
    }
}