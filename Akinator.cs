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
            Root = new BinaryTree<string>();
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
            
            while (true)
            {
                Console.WriteLine(treeNode.Data);
                var answerFromHumanYN = Console.ReadLine()?.ToLower();
                if (treeNode.IsAnswer())
                    EndProgram(treeNode, answerFromHumanYN);

                switch (answerFromHumanYN)
                {
                    case "yes":
                       treeNode.GoTo(Direction.Left);
                       continue;
                    case "no":
                        treeNode.GoTo(Direction.Right); ;
                        continue;
                    default:
                        throw new ArgumentException($"Такое я не понимать: {answerFromHumanYN}");
                }
            }
        }

        private void EndProgram(BinaryTree<string> treeNode,
            string? answerFromHumanYN)
        {
            switch (answerFromHumanYN)
            {
                case "no":
                    AddNewObject(treeNode);
                    Start();
                    break;
                case "yes":
                    Console.WriteLine("I AM God");
                    Start();
                    break;
            }
        }

        public void AddNewObject(BinaryTree<string> treeNode)
        {
            Console.WriteLine("Что это?");
            var obj = Console.ReadLine();
            Console.WriteLine($"Чем оно отличается от {treeNode.Data}");
            var question = Console.ReadLine();
            if (treeNode.IsRootTree)
            {
               treeNode.ChangeRoot(question,obj);
               return;
            }
            treeNode.CreateNode(question,obj);
        }

        public void SaveDate()
        {
            Console.WriteLine("Дай название базе");
            var nameDate = Console.ReadLine();
        }

        public void Serialization()
        {
            foreach (var treeNode in Root)
            {
            }
        }
    }
}