using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Ads.Models
{
    public class AdContext : DbContext
    {
        public AdContext() : base("MyCS") { }


        public DbSet<Ad> Ads { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories{ get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        public DbSet<ForbiddenWord> ForbiddenWords { get; set; }

        public DbSet<AdminMessage> AdminMessages { get; set; }
        
    }
}