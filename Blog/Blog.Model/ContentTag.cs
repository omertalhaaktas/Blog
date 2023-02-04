using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class ContentTag : Core.ModelBase
    {
        public ContentTag()
        {

        }
        public ContentTag(int contentId, int tagId)
        {
            TagId = tagId;
            ContentId = contentId;
        }
        public int ContentId { get; set; }
        public int TagId { get; set; }
    }
}
