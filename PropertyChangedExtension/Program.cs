using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace PropertyChangedExtension
{
    class Program
    {
        private static long n;

        static void Main(string[] args)
        {
            var foo = new Foo();
            foo.PropertyChanged += foo_PropertyChanged1;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                foo.Raise("Name");
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                StressTest.RaiseEvent(foo, "Name");
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }

        static void foo_PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            n++;
        }
    }

    class Foo : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public void Raise(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
