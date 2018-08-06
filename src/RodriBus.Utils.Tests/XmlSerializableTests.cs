using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Rodribus.Utils;
using RodriBus.Utils.Tests.Helpers;
using Xunit;

namespace RodriBus.Utils.Tests
{
    public static class XmlSerializableTests
    {
        [Fact]
        public static void ShouldSerialize()
        {
            var instace = _getInstance();
            var expected = _getXmlString();

            var xmlString = instace.Serialize();

            xmlString.Should().StartWithEquivalent(expected);
        }

        [Fact]
        public static void ShouldDeserialize()
        {
            var expected = _getInstance();
            var xmlString = _getXmlString();

            var instance = SerializableClass.Deserialize(xmlString);

            instance.Should().BeEquivalentTo(expected);
        }

        private static SerializableClass _getInstance()
        {
            var instance = new SerializableClass
            {
                Attribute = 1,
                IntProperty = 1,
                DoubleProperty = 0.5,
                StringProperty = "str",
                ClassPropery = new SerializablePropertyClass { StringProperty = "class-str" },
                IntListPropery = new List<int> { 1, 2 },
                DoubleListPropery = new List<double> { 0.5, 1.5 },
                StringListPropery = new List<string> { "class", "str" },
                ClassListPropery = new List<SerializableListPropertyClass> {
                    new SerializableListPropertyClass { StringProperty = "list" },
                    new SerializableListPropertyClass { StringProperty = "class" }
                }
            };

            return instance;
        }

        private static string _getXmlString()
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<SerializableClass Attribute=\"1\" xmlns=\"MyNamespace\">" +
                "<IntProperty>1</IntProperty>" +
                "<DoubleProperty>0.5</DoubleProperty>" +
                "<StringProperty>str</StringProperty>" +
                "<ClassPropery>" +
                "<StringProperty>class-str</StringProperty>" +
                "</ClassPropery>" +
                "<IntListPropery>1</IntListPropery>" +
                "<IntListPropery>2</IntListPropery>" +
                "<DoubleListPropery>0.5</DoubleListPropery>" +
                "<DoubleListPropery>1.5</DoubleListPropery>" +
                "<StringListPropery>class</StringListPropery>" +
                "<StringListPropery>str</StringListPropery>" +
                "<ClassListPropery>" +
                "<StringProperty>list</StringProperty>" +
                "</ClassListPropery>" +
                "<ClassListPropery>" +
                "<StringProperty>class</StringProperty>" +
                "</ClassListPropery>" +
                "</SerializableClass>";
        }
    }
}
