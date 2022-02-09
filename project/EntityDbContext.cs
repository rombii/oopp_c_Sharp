using System;
using Microsoft.EntityFrameworkCore;
using project.Game;
using Enemy = project.Models.Enemy;
using Item = project.Models.Item;

namespace project;

public class EntityDbContext : DbContext
{
    public DbSet<Enemy> Enemies { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Element> Elements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source="+Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 25) + "/res/entityDB.db");
    }
}