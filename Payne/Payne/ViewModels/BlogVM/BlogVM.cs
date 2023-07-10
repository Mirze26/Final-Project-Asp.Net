using Payne.Helpers;
using Payne.Models;

namespace Payne.ViewModels.BlogVM
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BlogImage> BlogImages { get; set; }
        public Paginate<Blog> PaginatedDatas { get; set; }
       
    }
}
