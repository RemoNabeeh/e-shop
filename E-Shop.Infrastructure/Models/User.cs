using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace E_Shop.Infrastructure.Models
{
    [Serializable()]
    public class User
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
    [XmlRoot("Users")]
    public class UserCollection
    {
        [XmlElement("User")]
        public List<User> Users { get; set; }
    }
}
