using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Лаб_13_События_
{
    class Program
    {
        static Jornal journal1 = new Jornal();
        static Jornal journal2 = new Jornal();        

        /*static public void WriteReferenceChange(MyNewCollection source, CollectionHandlerEventArgs args)
        {
            journal2.AddEntry(args.CollectionName, args.EventChangeName, args.Item2.ToString());
            journal1.AddEntry(args.CollectionName, args.EventChangeName, args.Item2.ToString());
            Console.WriteLine($"В коллекции {args.CollectionName} изменили ссылку ");
            args.Item2.ToString();
        }*/

        static void Main(string[] args)
        {
            MyNewCollection collection1 = new MyNewCollection();
            collection1.Name = "Collection 1";
            MyNewCollection collection2 = new MyNewCollection();
            collection2.Name = "Collection 2";

            Jornal journal1 = new Jornal();
            Jornal journal2 = new Jornal();

            collection1.CollectionAddChange += delegate (MyNewCollection sourse, CollectionHandlerEventArgs e)
            {                
                journal1.AddEntry(e.CollectionName, e.EventChangeName, e.Item.ToString());
                Console.WriteLine($"В коллекции {e.CollectionName} добавили ");
                e.Item.Print();
            };
            collection1.CollectionRemoveChange += delegate (MyNewCollection sourse, CollectionHandlerEventArgs e)
            {
                journal1.AddEntry(e.CollectionName, e.EventChangeName, e.Item.ToString());
                Console.WriteLine($"В коллекции {e.CollectionName} удалили ");
                e.Item.Print();
            };
            collection1.CollectionReferenceChanged += delegate (MyNewCollection sourse, CollectionHandlerEventArgs e)
            {
                journal2.AddEntry(e.CollectionName, e.EventChangeName, e.Item2.ToString());
                journal1.AddEntry(e.CollectionName, e.EventChangeName, e.Item2.ToString());
                Console.WriteLine($"В коллекции {e.CollectionName} добавили ");
                e.Item2.ToString();
            };

            collection2.CollectionReferenceChanged += delegate (MyNewCollection sourse, CollectionHandlerEventArgs e)
            {
                journal2.AddEntry(e.CollectionName, e.EventChangeName, e.Item2.ToString());                
                Console.WriteLine($"В коллекции {e.CollectionName} добавили ");
                e.Item2.ToString();
            };            

            collection1.AddDefaults(5);
            collection2.AddDefaults(3);
            collection2.Remove(1);
            collection1.Remove(1);

            Person p = new Person("Анна", "Иванова", 20, 'Ж');
            List<Person> l = new List<Person>();
            l.Add(p);
            Item<Person> it = new Item<Person>(l);

            Console.WriteLine(collection1[0]);
            collection1[0] = it;
            collection2[0] = it;

            Console.WriteLine($"Журнал коллекции 1:");
            journal1.Print();
            Console.WriteLine($"Журнал коллекции 2:");
            journal2.Print();
        }
    }
}
