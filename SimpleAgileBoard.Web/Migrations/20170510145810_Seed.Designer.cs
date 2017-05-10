using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleAgileBoard.Web.Data;
using SimpleAgileBoard.Web.Models.ServiceModels;

namespace SimpleAgileBoard.Web.Migrations
{
    [DbContext(typeof(AgileBoardDbContext))]
    [Migration("20170510145810_Seed")]
    partial class Seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleAgileBoard.Web.Models.DataModels.BoardTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });
        }
    }
}
