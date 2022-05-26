using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_13_События_
{
    class Item<T>
    {       
        public List<T> Elements = new List<T>();


        public Item(List<T> el)
        {
            Elements = new List<T>(el);
        }

        public override string ToString()
        {
            string str = "";
            //str += "Элементы:\n";
            foreach (T i in Elements)
                str += $"{i}\n";
            return str;
        }
    }

    //Enumerator для перечесление цепочки
    class MyHashEnumerator<T> : IEnumerator<Item<T>>
    {
        int position = -1;
        Item<T>[] items;

        public MyHashEnumerator(Item<T>[] c)
        {
            this.items = c;
        }

        public Item<T> Current
        {
            get
            {
                if (position < 0 || position >= items.Length)
                    throw new ArgumentException();
                return items[position];
            }
        }

        object IEnumerator.Current { get { return Current; } }

        public bool MoveNext()
        {
            if (position < items.Length - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose() { }
    }

    class MyHashTable<T> : IEnumerable<Item<T>>, ICloneable where T : IComparable, ICloneable
    {
        protected Item<T>[] items = null;        
        public int Count { get; set; } = 0;

        //Конструкторы
        public MyHashTable()
        {
            items = new Item<T>[5];
        }

        public MyHashTable(int size)
        {
            items = new Item<T>[size];
        }

        public MyHashTable(MyHashTable<T> c)
        {

            Count = c.Count;
            items = new Item<T>[c.items.Length];

            for (var i = 0; i < c.items.Length; i++)
            {
                if (c.items[i] != null)
                {
                    List<T> tmp = new List<T>();
                    for (var j = 0; j < c.items[i].Elements.Count; j++)
                    {
                        tmp.Add((T)c.items[i].Elements[j].Clone());
                    }
                    this.items[i] = new Item<T>(tmp);
                }
            }
        }

        //Добавление 1 элемента
        public void Add(T item)
        {
            var key = GetHash(item);
            if (items[key] == null)
            {
                var tmp = new List<T>();
                tmp.Add(item);
                items[key] = new Item<T>(tmp);
                Count++;
            }
            else
            {
                items[key].Elements.Add(item);
                Count++;
            }
        }

        //Добавление нескольких элементов
        public void Add(params T[] item)
        {
            foreach (var i in item)
                Add(i);
        }

        //Поиск элемента
        public bool Search(T item)
        {
            if (items[GetHash(item)] == null)
            {
                return false;
            }
            else
            {
                return items[GetHash(item)].Elements.Contains(item);
            }
        }

        //Удаление 1 элемента
        public bool Remove(T item)
        {
            if (Count == 0)
            {
                return false;
            }
            else
            {
                var key = GetHash(item);
                if (items[key] != null)
                {
                    items[key].Elements.RemoveAt(items[key].Elements.FindIndex(x => object.Equals(x, item)));
                    if (items[key].Elements.Count == 0)
                    {
                        items[key] = null;
                    }
                    Count--;
                    return true;
                }
                return false;
            }
        }

        //Удаление нескольких элементов
        public void Remove(params T[] item)
        {
            foreach (var i in item)
                Remove(i);
        }

        //Клонирование коллекции
        public object Clone()
        {
            return new MyHashTable<T>(this);
        }

        //Поверхностное копирование коллекции      
        public object Copy()
        {
            return this.MemberwiseClone();
        }

        //Удаление коллекции из памяти
        public void Clear()
        {
            Count = 0;
            items = new Item<T>[100];
        }

        public IEnumerator<Item<T>> GetEnumerator()
        {
            return new MyHashEnumerator<T>(items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual int GetHash(T item)
        {
            return item.GetHashCode() % items.Length;
        }

        //Печать 
        public void Show()
        {
            if (Count != 0)
            {
                int k = 0;
                foreach (var i in items)
                {
                    Console.WriteLine($"Хэш-функция ={k++}: {i}");
                }
            }
            else
                Console.WriteLine("HashTable пустая");
        }

        virtual public Item<T> this[int i]
        {
            get
            {
                if (i >= 0 && i < items.Length)
                    return items[i];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (i >= 0 && i < items.Length)
                {
                    items[i] = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }
    }    
}