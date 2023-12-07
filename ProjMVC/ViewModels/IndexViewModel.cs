using MVCProject_.Models;

namespace MVCProject_.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
