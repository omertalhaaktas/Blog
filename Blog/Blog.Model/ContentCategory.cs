using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class ContentCategory : Core.ModelBase 
    {
        public ContentCategory()
        {

        }
        public ContentCategory(int contentId, int categoryId)
        {
            CategoryId = categoryId;
            ContentId = contentId;
        }
        public int ContentId { get; set; }
        public int CategoryId { get; set; }
    }
}
