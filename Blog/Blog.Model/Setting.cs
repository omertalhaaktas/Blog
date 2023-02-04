using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class Setting : Core.ModelBase
    {
        public string LogoPath { get; set; }
        public string HomeMetaTitle { get; set; }
        public string HomeMetaDescription { get; set; }
        public string FtpUsername { get; set; }
        public bool FtpPassword { get; set; }
        public bool FtpSiteUrl { get; set; }
        public bool MediaBasePath { get; set; }
    }
}
