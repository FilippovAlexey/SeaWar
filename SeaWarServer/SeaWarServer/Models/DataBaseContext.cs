using SeaWarServer.NonProject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=SeaWarConnectionString")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<BookItem> Book { get; set; }
    }
}