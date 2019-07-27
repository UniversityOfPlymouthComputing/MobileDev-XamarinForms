using System;
using System.IO;
using System.Xml.Serialization;

namespace HelloBindingsLib
{
    public class PayLoad
    {
        public string Saying { get; set; }
        public int From { get; set; }
        public PayLoad()
        {
        }
        public string ToXML()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }

        }
        public static PayLoad FromXML(string text)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PayLoad));
            using (StringReader textReader = new StringReader(text))
            {
                return xmlSerializer.Deserialize(textReader) as PayLoad;
            }
        }
    }
}
