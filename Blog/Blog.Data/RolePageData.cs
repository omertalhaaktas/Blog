using Blog.Data.Infrastructure.Entities;
using Blog.Data.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Model;

namespace Blog.Data
{
    public class RolePageData : EntityBaseData<Model.RolePage>
    {
        public RolePageData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}

