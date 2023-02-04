using Blog.Data.Infrastructure;
using Blog.Data.Infrastructure.Entities;
using Blog.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class AuthorData : EntityBaseData<Model.Author>
    {
        public AuthorData(IOptions<DatabaseSettings> dbOptions) 
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
