using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.Infrastructure.Helpers
{
    public class XmlManager
    {
        #region [Properties]

        private readonly string FilePath;

        #endregion

        #region [Ctor]

        public XmlManager(string filePath)
        {
            string epath = Assembly.GetExecutingAssembly().Location;
            FilePath = epath.Substring(0, epath.LastIndexOf('\\') + 1) + filePath;
        }

        #endregion

        public IEnumerable<T> Read<T>()
        {
            XElement xelement = XElement.Load(FilePath);
            IEnumerable<XElement> elements = xelement.Elements();
            List<T> result = new List<T>();
            foreach (var element in elements)
            {
                result.Add(DeSerializer<T>(element));
            }
            return result;
        }

        public void Write<T>(IEnumerable<T> items, string itemsParentNode = null)
        {
            XElement root = XElement.Load(FilePath);

            XElement parentItem = null;
            if (!string.IsNullOrEmpty(itemsParentNode))
                parentItem = new XElement(itemsParentNode);

            foreach (var item in items)
            {
                var element = Serializer(item);
                if (parentItem != null)
                    parentItem.Add(element);
                else
                    root.Add(element);
            }

            if (parentItem != null)
                root.Add(parentItem);

            root.Save(FilePath);
        }

        #region [Helpers]

        private T DeSerializer<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(element.CreateReader());
        }

        private XElement Serializer<T>(T item)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, item);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }

        #endregion
    }
}
