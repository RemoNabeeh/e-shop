using System.Collections.Generic;

namespace E_Shop.Core.Entities
{
    public class Product
    {
        public Product()
        {
            Images = new List<string>();
        }   

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public List<string> Images { get; set; }
    }
}
