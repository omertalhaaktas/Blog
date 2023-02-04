using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class Comment : Core.ModelBase
    {
        public int ContentId { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Text { get; set; }
        public bool IsApprovied { get; set; }
    }
}
