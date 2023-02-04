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
    public class SettingData : EntityBaseData<Model.Setting>
    {
        public SettingData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
