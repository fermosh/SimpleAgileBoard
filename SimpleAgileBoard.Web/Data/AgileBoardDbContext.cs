using Microsoft.EntityFrameworkCore;
using SimpleAgileBoard.Web.Models.DataModels;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleAgileBoard.Web.Data
{
    public class AgileBoardDbContext : DbContext
    {
        public DbSet<BoardTask> Tasks{get;set;}
        public AgileBoardDbContext(DbContextOptions<AgileBoardDbContext> options) : base(options){}
        public AgileBoardDbContext() : base(){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            //Called by parameterless ctor Usually Migrations
            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";
            Console.WriteLine(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location)+"\\..\\..\\..");
            optionsBuilder.UseSqlServer(
                new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location)+"\\..\\..\\..") //ugly, I know
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: false)
                    .Build()
                    .GetConnectionString("AgileBoardDbContext")
            );
        }  
    }
}