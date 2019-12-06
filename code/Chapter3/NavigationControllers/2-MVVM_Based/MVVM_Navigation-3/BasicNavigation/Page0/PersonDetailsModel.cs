using System.Xml;

namespace BasicNavigation
{

    public class PersonDetailsModel : BindableModelBase
    {
        
        private string name;
        private int birthYear;

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

    }
}
