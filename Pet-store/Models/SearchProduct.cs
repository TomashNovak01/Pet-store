using System.Collections.Generic;
using System.Linq;

namespace Pet_store.Models
{
    public class SearchProduct
    {
        public List<string> Products { get; set; } = DataBaseContext.Instance.Products.Select(c => c.Name).ToList();
        public List<string> Categories { get; set; } = DataBaseContext.Instance.Categories.Select(c => c.Name).ToList();
    }
}
