using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_13_События_
{
    class JournalEntry
    {
        public string CollectionName { get; set; }
        public string EventChangeName { get; set; }
        public string Item { get; set; }

        public JournalEntry(string collectionName, string eventChangeName, string item)
        {
            CollectionName = collectionName;
            EventChangeName = eventChangeName;
            Item = item;
        }

        public override string ToString()
        {
            return "В коллекции:" + CollectionName + ", произошло событие:" + EventChangeName + ", с элементом:" + Item;
        }
    }

    class Jornal
    {
        List<JournalEntry> jornal = new List<JournalEntry>();

        public void AddEntry(string collectionName, string eventChangeName, string item)
        {
            jornal.Add(new JournalEntry(collectionName, eventChangeName, item));
        }

        public void Print()
        {
            foreach (var x in jornal)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
}
