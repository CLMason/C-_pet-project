using System;
using Microsoft.EntityFrameworkCore;


namespace CsharpProject.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}

        public DbSet<Pet> Pets {get;set;}

        public DbSet<Message> Messages {get;set;}
    }
}