using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BasicNavigation
{
    public class PersonDetailsModel : INotifyPropertyChanged
    {
        private string name;
        private int birthYear;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int BirthYear
        {
            get => birthYear;
            set {
                if (birthYear != value)
                {
                    birthYear = value;
                    OnPropertyChanged();
                }
            }
        }

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Default constructor
        public PersonDetailsModel(string n = "NickO", int y = 1970)
        {
            Name = n;
            BirthYear = y;
        }

        //Copy Constructor
        public PersonDetailsModel(PersonDetailsModel m)
        {
            BirthYear = m.BirthYear;
            Name = m.Name;
        }

        //Copy instance-method
        public PersonDetailsModel Copy() => new PersonDetailsModel(Name, BirthYear);

        //Copy - static method
        public static PersonDetailsModel Copy(PersonDetailsModel m) => new PersonDetailsModel(m.Name, m.BirthYear);

    }
}
