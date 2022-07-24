using System;
using BinaryTrees;

namespace SimpleAkinator
{
    public class Akinator
    {
        private BinaryTree _binaryTree;

        public Akinator()
        {
            _binaryTree = new BinaryTree("Неизвестно что!");
        }

        public void Start()
        {
            Console.WriteLine("Что ты хочешь? [1] - игры, [2] - сохранить базу, [3] - загрузка базы");
            var chose = Console.ReadLine();
            _binaryTree.Start();
            switch (chose)
            {
                case "1":
                    ActivateGame();
                    break;
                case "2":
                    SaveData();
                    break;
                case "3":
                    LoadData();
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

        private void CheckAnswer(string? answerFromHumanYn)
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