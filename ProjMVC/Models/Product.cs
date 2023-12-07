namespace MVCProject_.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Product()
        {
            Name = "";
            Quantity = 0;
        }
    }
}
