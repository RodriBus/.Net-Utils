using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RodriBus.Utils.Tests.Helpers
{
    [XmlRoot("SerializableClass", Namespace = NAMESPACE)]
    public class SerializableClass : XmlSerializable<SerializableClass>
    {
        public const string NAMESPACE = "MyNamespace";

        public SerializableClass()
        {
            SetEncoding("UTF-8");
            AddNamespace(string.Empty, NAMESPACE);
        }

        [XmlAttribute("Attribute")]
        public int Attribute { get; set; }

        [XmlElement("IntProperty")]
        public int IntProperty { get; set; }

        [XmlElement("DoubleProperty")]
        public double DoubleProperty { get; set; }

        [XmlElement("StringProperty")]
        public string StringProperty { get; set; }

        [XmlElement("ClassPropery")]
        public SerializablePropertyClass ClassPropery { get; set; }


        [XmlElement("IntListPropery")]
        public List<int> IntListPropery { get; set; }

        [XmlElement("DoubleListPropery")]
        public List<double> DoubleListPropery { get; set; }

        [XmlElement("StringListPropery")]
        public List<string> StringListPropery { get; set; }

        [XmlElement("ClassListPropery")]
        public List<SerializableListPropertyClass> ClassListPropery { get; set; }
    }

    public class SerializablePropertyClass
    {
        [XmlElement("StringProperty")]
        public string StringProperty { get; set; }
    }

    public class SerializableListPropertyClass
    {
        [XmlElement("StringProperty")]
        public string StringProperty { get; set; }
    }
}
