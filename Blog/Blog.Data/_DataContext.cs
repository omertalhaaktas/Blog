using Blog.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(string connectionString) : base (new DbContextOptionsBuilder().UseSqlServer (connectionString).Options)
        {
        } 
        public DbSet<Model.Author> Authors { get; set; } 
        public DbSet<Model.Category> Categories { get; set; }
        public DbSet<Model.Comment> Comments { get; set; }
        public DbSet<Model.Content> Contents { get; set; }
        public DbSet<Model.ContentCategory> ContentCategories { get; set; }
        public DbSet<Model.ContentTag> ContentTags { get; set; }
        public DbSet<Model.Media> Medias { get; set; }
        public DbSet<Model.Tag> Tags { get; set; }
        public DbSet<Model.Setting> Setting { get; set; }
        public DbSet<Model.Role> Roles { get; set; }
        public DbSet<Model.RolePage> RolePages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Model.Author>(entity => entity.ToTable("Blog_Authors"));
            builder.Entity<Model.Category>(entity => entity.ToTable("Blog_Categories"));
            builder.Entity<Model.Comment>(entity => entity.ToTable("Blog_Comments"));
            builder.Entity<Model.Content>(entity => entity.ToTable("Blog_Contents"));
            builder.Entity<Model.ContentCategory>(entity => entity.ToTable("Blog_Content_Categories"));
            builder.Entity<Model.ContentTag>(entity => entity.ToTable("Blog_Content_Tags"));
            builder.Entity<Model.Media>(entity => entity.ToTable("Blog_Medias"));
            builder.Entity<Model.Tag>(entity => entity.ToTable("Blog_Tags"));
            builder.Entity<Model.Setting>(entity => entity.ToTable("Blog_Setting"));
            builder.Entity<Model.Role>(entity => entity.ToTable("Blog_Role"));
            builder.Entity<Model.RolePage>(entity => entity.ToTable("Blog_Role_Page"));
        }
    }
}   