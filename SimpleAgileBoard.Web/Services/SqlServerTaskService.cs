using System;
using System.Collections.Generic;
using System.Linq;
using SM=SimpleAgileBoard.Web.Models.ServiceModels;
using SimpleAgileBoard.Web.Models.DataModels;
using SimpleAgileBoard.Web.Data;
using Microsoft.EntityFrameworkCore;
namespace SimpleAgileBoard.Web.Services
{
    public class SqlServerTaskService : ITaskService
    {
        private readonly DbContextOptions<AgileBoardDbContext> __dbContextOptions;
        public SqlServerTaskService(DbContextOptions<AgileBoardDbContext> dbContextOptions){
            __dbContextOptions = dbContextOptions;
        }
        public IEnumerable<SM.BoardTask> GetAllTasks()
        {
           
           var db = new AgileBoardDbContext(__dbContextOptions);

                return db.Tasks.Select(t=>new SM.BoardTask{
                    Id=t.Id,
                    Name=t.Name,
                    Status=t.Status
                });
//            return Enumerable.Range(1, 100).Select(x => new BoardTask { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Status = (BoardTaskStatus)(x % 3) });
        }
        public SM.BoardTask MoveTask(Guid Id,SM.BoardTaskStatus status){
            var db = new AgileBoardDbContext(__dbContextOptions);
            var task = db.Tasks.FirstOrDefault(t=>t.Id==Id);
            if(task == null) return null;
            task.Status = status;
            db.SaveChanges();
            return new SM.BoardTask{
                Id=task.Id,
                Name=task.Name,
                Status=task.Status
            };
        }
    
    }
}