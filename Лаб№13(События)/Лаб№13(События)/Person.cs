using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_13_События_
{
    public interface IExecutable
    {
        string Name { get; set; }
        string Surname { get; set; }
        int Age { get; set; }
        char Gender { get; set; }
        void Print();
    }

    public class Person : IExecutable, IComparable, ICloneable
    {
        protected string name;
        protected string surname;
        protected int age;
        protected char gender;

        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value <= 0)
                    age = 0;
                else
                    age = value;
            }
        }

        public char Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value == 'M' || value == 'F')
                    gender = value;
                else
                    gender = 'F';
            }
        }

        public Person()
        {
            Name = "НетИмени";
            Surname = "НетФамилии";
            Age = 0;
            Gender = 'F';
        }

        public Person(string n, string s, int a, char g)
        {
            Name = n;
            Surname = s;
            Age = a;
            Gender = g;
        }

        public virtual void Print()
        {
            Console.WriteLine($"Человек: {Name} {Surname}, возраст: {Age}, пол: {Gender}");
        }

        public int CompareTo(Object obj)
        {
            Person p = (Person)obj;
            Console.WriteLine(String.Compare(this.Name, p.Name));
            if (String.Compare(this.Name, p.Name) > 0) return 1;
            if (String.Compare(this.Name, p.Name) < 0) return -1;
            return 0;
        }

        // Глубокое клонирование
        public virtual object Clone()
        {
            return new Person($"Клон: {this.Name}", this.Surname, this.Age, this.Gender);
        }

        //Поверхностное копирование
        public virtual object Copy()
        {
            return (Person)this.MemberwiseClone();
        }

        /////////////////     
        public override string ToString()
        {
            return ($"{Name} {Surname}, возраст: {Age}, пол: {Gender}");
        }

        public override int GetHashCode()
        {
            var k1 = this.Name.GetHashCode();
            var k2 = this.Surname.GetHashCode();
            return Math.Abs(this.Name.GetHashCode() % 100) + Math.Abs(this.Surname.GetHashCode()) % 100 + this.Age + this.Gender.GetHashCode();
        }        
    }
}

