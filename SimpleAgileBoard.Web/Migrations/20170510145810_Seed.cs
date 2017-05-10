using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleAgileBoard.Web.Data;
using SM=SimpleAgileBoard.Web.Models.ServiceModels;
using SimpleAgileBoard.Web.Models.DataModels;

namespace SimpleAgileBoard.Web.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using(var db = new AgileBoardDbContext()){
                db.Tasks.AddRange(
                    new BoardTask[]{
                        new BoardTask{ Id=Guid.NewGuid() , Name="Pending Stuff" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Start Actual Coding" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Get My Life Toghether" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Procrastinate" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Forget My Priorities" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Realize I'm Late" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Give Up" , Status=SM.BoardTaskStatus.DONE },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Be Late" , Status=SM.BoardTaskStatus.DONE },
                    }
                );
                db.SaveChanges();

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            using (var db = new AgileBoardDbContext())
            {
               	db.RemoveRange(db.Tasks);
                db.SaveChanges();
            }

        }
    }
}
