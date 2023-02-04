using Blog.Data.Infrastructure.Entities;
using Blog.Data.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class TagData : EntityBaseData<Model.Tag>
    {
        public TagData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
