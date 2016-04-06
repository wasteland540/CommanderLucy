using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CommanderLucy.Model
{
    public class Command
    {
        public string Name { get; set; }
        public string CommandText { get; set; }
        public string Action { get; set; } //filepath, url, or pluginname...
        public string[] Parameters { get; set; }

        public static void Serialize(string filename, Command[] commands)
        {
            var xmlSerializer = new XmlSerializer(typeof(Command[]));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, commands);
                    string xml = sww.ToString();

                    File.WriteAllText(filename, xml);
                }
            }
        }

        public static Command[] Deserialize(string filename)
        {
            Command[] commands;
            var xmlSerializer = new XmlSerializer(typeof(Command[]));

            string xml = File.ReadAllText(filename);

            using (var sr = new StringReader(xml))
            {
                using (XmlReader reader = XmlReader.Create(sr))
                {
                    commands = (Command[])xmlSerializer.Deserialize(reader);
                }
            }

            return commands;
        }
    }
}