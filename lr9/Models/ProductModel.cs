namespace lr9.Models
{
    public class ProductModel
    {
        private static int nextId = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductModel()
        {
            Id = nextId++;
            Name = "Ringo";
            Price = 100;
        }
        public ProductModel(string name, int price)
        {
            Id = nextId++;
            Name = name;
            Price = price;
        }

        ~ProductModel() { nextId--; }
    }
}
