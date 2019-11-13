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
        private string filename;
        private string name;
        private int birthYear;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get => filename; set => filename = value; }

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
        public PersonDetailsModel()
        {
            Name = "NickO";
            BirthYear = 1970;
        }
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

        //Serialise this instance to an XML file
        public void Save()
        {
            if (Filename != null)
            {
                Save(Filename);
            } else
            {
                throw new Exception("No filename set");
            }
        }

        public void Save(string FileName)
        {
            using (var writer = new System.IO.StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        //Deserialise an XML file to a new instance of this type
        public static PersonDetailsModel Load(string fn)
        {
            try
            {
                using (FileStream stream = File.OpenRead(fn))
                {
                    var serializer = new XmlSerializer(typeof(PersonDetailsModel));
                    PersonDetailsModel m = serializer.Deserialize(stream) as PersonDetailsModel;
                    m.Filename = fn;
                    return m;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
