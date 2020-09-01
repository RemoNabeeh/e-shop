using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace E_Shop.Infrastructure.Models
{
    [Serializable()]
    public class Product
    {
        [XmlElement("Code")]
        public string Code { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Image")]
        public string Image { get; set; }

        [XmlElement("Price")]
        public string Price { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }
    }

    [Serializable()]
    [XmlRoot("Products")]
    public class ProductCollection
    {
        [XmlElement("Product")]
        public List<Product> Products { get; set; }
    }
}
