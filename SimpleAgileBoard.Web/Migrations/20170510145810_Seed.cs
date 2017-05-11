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
                        new BoardTask{ Id=new Guid("8753CA8C-6ECE-4410-AF11-517C3E0CB64F") , Name="Pending Stuff" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Start Actual Coding" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Get My Life Toghether" , Status=SM.BoardTaskStatus.TO_DO },
                        new BoardTask{ Id=new Guid("EC51D296-40E8-44E7-B56B-846DF44FD72E") , Name="Procrastinate" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Forget My Priorities" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=Guid.NewGuid() , Name="Realize I'm Late" , Status=SM.BoardTaskStatus.IN_PROGRESS },
                        new BoardTask{ Id=new Guid("3988FF4C-ED57-4667-B351-6375EE2D8E11") , Name="Give Up" , Status=SM.BoardTaskStatus.DONE },
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
