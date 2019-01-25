using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MusicStoreDbContext:DbContext
    {
        public MusicStoreDbContext()
        {
            Database.Log = s => Debug.WriteLine(s);
        }
        public DbSet<Album> Albums { get; set; }
    }
}