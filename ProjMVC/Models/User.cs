using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_.Models
{
    public class User
    {
        private static int nextId = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int Age { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public User()
        {
            Id = nextId++;
        }
        public User(string name, int age)
        {
            Id = nextId++;
            Name = name;
            Age = age;
        }
        ~User()
        {
            nextId--;
        }

    }
}
