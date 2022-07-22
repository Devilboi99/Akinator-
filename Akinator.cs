using System;
using BinaryTrees;

namespace SimpleAkinator
{
    public class Akinator
    {
        private BinaryTree _binaryTree;
        public BinaryTree GetData => _binaryTree; 
        public Akinator()
        {
            _binaryTree = new BinaryTree("Неизвесто что?");
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

        private void ActivateAkinator()
        {
            Console.WriteLine("Добро пожаловать");
            FindAnswer();
        }

        private void FindAnswer()
        {
            _binaryTree.Start();
            while (true)
            {
                Console.WriteLine(_binaryTree.CurData);
                var answerFromHumanYn = Console.ReadLine()?.ToLower();
                if (_binaryTree.IsAnswer())
                    CompletedFind(answerFromHumanYn);

                switch (answerFromHumanYn)
                {
                    case "yes":
                        _binaryTree.GoTo(Direction.Left);
                        continue;
                    case "no":
                        _binaryTree.GoTo(Direction.Right);
                        continue;
                    default:
                        Console.WriteLine("Либо да или нет?");
                        continue;
                }
            }
        }

        private void CompletedFind(string? answerFromHumanYn)
        {
            switch (answerFromHumanYn)
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

            Console.WriteLine($"Чем оно отличается от {_binaryTree.CurData}");
            var question = Console.ReadLine();


            _binaryTree.CreateNode(question, obj);
        }

        public void SaveDate()
        {
            Console.WriteLine("Дай название базе");
        }
    }
}