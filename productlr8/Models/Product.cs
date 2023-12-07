namespace productlr8.Models
{
    public class Product
    {
        private static int nextId = 1;
        public long Id { get; set; } = 0; 
        public string Name { get; set; }
        public uint Price { get; set; }
        public DateTime CreatedDate;
        public Product(string name, uint price)
        {
            Id = nextId++;
            Name = name;
            Price = price;
            CreatedDate = DateTime.Now;
        }
        ~Product() { nextId--; }
    }
}
