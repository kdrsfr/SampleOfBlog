﻿using Project.DAL.Initialize;
using Project.ENTITIES.Models.Entities;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext():base("MyConnection")
        {
            Database.SetInitializer(new MyInit());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SuspectMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new AppUserMap());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Suspect> Suspects { get; set; }

    }
}
