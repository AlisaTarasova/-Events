using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Лаб_13_События_
{    
    class CollectionHandlerEventArgs: EventArgs
    {
        public string CollectionName { get; set; }
        public string EventChangeName { get; set; }
        public Person Item { get; set; }
        public Item<Person> Item2 { get; set; }


        public CollectionHandlerEventArgs(string collectionName, string eventChangeName, Person item)
        {
            CollectionName = collectionName;
            EventChangeName = eventChangeName;
            Item = item;
        }

        public CollectionHandlerEventArgs(string collectionName, string eventChangeName, Item<Person> item2)
        {
            CollectionName = collectionName;
            EventChangeName = eventChangeName;
            Item2 = item2;
        }
    }

    delegate void CollectionHandler(MyNewCollection source, CollectionHandlerEventArgs args); // тип позволяет хранить ссылку на функцию
    
    class MyNewCollection: MyHashTable<Person>
    {
        //события как экземпляры делегата
        public event CollectionHandler CollectionAddChange;
        public event CollectionHandler CollectionRemoveChange;
        public event CollectionHandler CollectionReferenceChanged; //объекту коллекции присваивается новое значение
        //public event CollectionHandler CollectionCountChanged;

        public string Name { get; set; }

        public void OnCollectionAddChange(MyNewCollection source, CollectionHandlerEventArgs args)
        {
            if (CollectionAddChange != null)
                CollectionAddChange(source, args);
        }

        public void OnCollectionRemoveChange(MyNewCollection source, CollectionHandlerEventArgs args)
        {
            if (CollectionRemoveChange != null)
                CollectionRemoveChange(source, args);
        }

        public void OnCollectionReferenceChanged(MyNewCollection source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }

        public new void Add(Person item)
        {
            base.Add(item);
            OnCollectionAddChange(this, new CollectionHandlerEventArgs(this.Name, "Add", item));
        }

        public new void Remove(Person item)
        {
            if (base.Remove(item))
            {
                OnCollectionRemoveChange(this, new CollectionHandlerEventArgs(this.Name, "Remove", item));
            }
            else
            {
                Console.WriteLine("Удаление не произошло!");
            }
        }        

        public void Remove(int index)
        {
            if (index < 1 || index > items.Length) 
                throw new IndexOutOfRangeException();

            if (items[index] != null)
            {
                while (items[index] != null)
                    Remove(items[index].Elements[0]);
            }                    
        }

        static public Random rnd = new Random();
        
        public void AddDefaults(int length)
        {
            string[] MemNames = { "Александр",  "Михаил", "Максим", "Артем",
                "Даниил", "Лев", "Марк", "Иван", "Дмитрий", "Матвей", "Роман",
                "Тимофей", "Мирон", "Кирилл", "Илья", "Федор", "Андрей",
                "Никита", "Егор", "Алексей", "Сергей", "Иван", "Владимир",
                "Алексей", "Дмитрий","Данил", "Павел", "Антон"};

            string[] WomanNames = {"Елена", "Александра", "Дарья", "Елизавета",
                "Анна", "Татьяна", "Мария", "Анастасия", "Алина", "Алиса",
                "Валерия", "Вероника", "Виктория", "Евгения", "Софья", "Мирослава",
                "Полина", "Юлия", "Ирина", "Екатерина", "Дарина", "Светлана", "Наталья"};

            string[] MemSurnames = {"Иванов", "Смирнов", "Кузнецов", "Попов",
                "Васильев", "Петров", "Соколов", "Михайлов", "Новиков",
                "Федоров", "Морозов", "Волков", "Алексеев", "Лебедев",
                "Семенов", "Егоров", "Павлов", "Козлов", "Степанов",
                "Николаев", "Тарасов"};

            string[] Womanurnames = {"Иванова", "Смирнова", "Кузнецова", "Попова",
                "Васильева", "Петрова", "Соколова", "Михайлова", "Новикова",
                "Федорова", "Морозова", "Волкова", "Алексеева", "Лебедева",
                "Семенова", "Егорова", "Павлова", "Козлова", "Степанова",
                "Николаева", "Тарасова"};

            for (int i = 1; i <= length; i++)
            {
                char pol;
                string name, surname;
                if (rnd.Next(1, 3) == 1)
                    pol = 'F';
                else
                    pol = 'M';

                if (pol == 'F')
                {
                    name = WomanNames[rnd.Next(WomanNames.Length)];
                    surname = Womanurnames[rnd.Next(Womanurnames.Length)];
                }
                else
                {
                    name = MemNames[rnd.Next(MemNames.Length)];
                    surname = MemSurnames[rnd.Next(MemSurnames.Length)];
                }

                int age = rnd.Next(18, 70);
                Person person = new Person(name, surname, age, pol);
                this.Add(person);
            }
        }

        public override Item<Person> this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "ReferenceChanged", base[index]));
            }
        }
    }
}
