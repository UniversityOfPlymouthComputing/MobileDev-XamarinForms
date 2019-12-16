using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Xml.Serialization;

namespace BasicNavigation
{
    public class BindableModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected string filename;
        public string Filename { get => filename; set => filename = value; }

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Serialise this instance to an XML file
        public void Save()
        {
            if (Filename != null)
            {
                Save(Filename);
            }
            else
            {
                throw new Exception("No filename set - either load or save with filename first");
            }
        }

        public void Save(string fn)
        {
            this.Filename = fn;
            using (var writer = new System.IO.StreamWriter(fn))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        //Deserialise an XML file to a new instance of this type
        public static ModelType Load<ModelType>(string fn) where ModelType : BindableModelBase
        {
            try
            {
                using (FileStream stream = File.OpenRead(fn))
                {
                    var serializer = new XmlSerializer(typeof(ModelType));
                    ModelType m = serializer.Deserialize(stream) as ModelType;
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
