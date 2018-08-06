using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RodriBus.Utils
{
    public abstract class XmlSerializable<T>
    {
        /// <summary>
        /// Encoding used in <see cref="Serialize"/> method to create the XML.
        /// </summary>
        private Encoding _encoding { get; set; } = Encoding.GetEncoding("UTF-8");

        /// <summary>
        /// Namespace dictionary[prefix,url] used in <see cref="Serialize"/> method to create the XML.
        /// </summary>
        private IDictionary<string, string> _namespaces { get; } = new Dictionary<string, string>() { { "", "" } };

        /// <summary>
        /// Sets the <see cref="_encoding"/> used in <see cref="Serialize"/> method to create the XML.
        /// Should be called inside the constructor.
        /// </summary>
        /// <param name="name">The code page name of the preferred encoding.</param>
        protected void SetEncoding(string name)
        {
            SetEncoding(Encoding.GetEncoding(name));
        }

        /// <summary>
        /// Sets the <see cref="_encoding"/> used in <see cref="Serialize"/> method to create the XML.
        /// Should be called inside the constructor.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        protected void SetEncoding(Encoding encoding)
        {
            _encoding = encoding;
        }

        /// <summary>
        /// Adds a namespace to the dictionary used in <see cref="Serialize"/> method to create the XML.
        /// Should be called inside the constructor.
        /// </summary>
        /// <param name="prefix">The prefix associated with an XML namespace.</param>
        /// <param name="value">An XML namespace.</param>
        protected void AddNamespace(string prefix, string value)
        {
            _namespaces[prefix] = value;
        }

        /// <summary>
        /// Serializes this instance into a XML string using XML attributes defined in the class.
        /// </summary>
        /// <returns>The XML string</returns>
        public string Serialize()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = _encoding,
                    Indent = false,
                    ConformanceLevel = ConformanceLevel.Document
                };

                var namespaces = new XmlSerializerNamespaces();
                foreach (var entry in _namespaces)
                {
                    namespaces.Add(entry.Key, entry.Value);
                }

                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    xmlSerializer.Serialize(writer, this, namespaces);
                }

                string xml = _encoding.GetString(stream.ToArray());

                return xml;
            }
        }

        /// <summary>
        /// Deserializes a XML string into an instance of a <see cref="T"/> class.
        /// </summary>
        /// <param name="xmlString">The XML string</param>
        /// <returns>The <see cref="T"/> class instance</returns>
        public static T Deserialize(string xmlString)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return Deserialize(doc);
        }

        /// <summary>
        /// Deserializes a <see cref="XmlDocument"/> into an instance of a <see cref="T"/> class.
        /// </summary>
        /// <param name="xml">The XML document</param>
        /// <returns>The <see cref="T"/> class instance</returns>
        public static T Deserialize(XmlDocument xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml.OuterXml);
            var item = serializer.Deserialize(reader);
            return (T)item;
        }
    }
}
