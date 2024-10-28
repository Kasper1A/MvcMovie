using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi;

public partial class TodoDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Visning> Visnings { get; set; }
    public DbSet<Reserve> Reservs { get; set; }
    public DbSet<Salong> Salongs { get; set; }


    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=Db.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
