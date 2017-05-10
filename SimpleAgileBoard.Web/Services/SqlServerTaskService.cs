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
            Console.WriteLine("{0}:\tSqlServerTaskService ctor.");
            __dbContextOptions = dbContextOptions;
        }
        public IEnumerable<SM.BoardTask> GetAllTasks()
        {
            Console.WriteLine("{0}:\tSqlServerTaskService Retreiving all tasks.");
           var db = new AgileBoardDbContext(__dbContextOptions);

                return db.Tasks.Select(t=>new SM.BoardTask{
                    Id=t.Id,
                    Name=t.Name,
                    Status=t.Status
                });
//            return Enumerable.Range(1, 100).Select(x => new BoardTask { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Status = (BoardTaskStatus)(x % 3) });
        }
        public SM.BoardTask MoveTask(Guid Id,SM.BoardTaskStatus status){
            Console.WriteLine("{0}:\tSqlServerTaskService.MoveTask Called with parameters Id:{1}, status:{2}",DateTime.Now , Id,status);
            Console.WriteLine("{0}:\tSqlServerTaskService.MoveTask Connecting to database...",DateTime.Now );
            var db = new AgileBoardDbContext(__dbContextOptions);
            var task = db.Tasks.FirstOrDefault(t=>t.Id==Id);
            if(task == null) return null;
            Console.WriteLine("{0}:\tSqlServerTaskService.MoveTask - Found a task: {{\r\n\tId: {1},\r\n\tName: {2},\r\n\tStatus: {3}}}",DateTime.Now,task.Id,task.Name,task.Status);
            if(task.Status == status) throw new InvalidOperationException("Destination status is the same as current");
            task.Status = status;
            Console.WriteLine("{0}:\tSqlServerTaskService.MoveTask Writing to database...",DateTime.Now );
            db.SaveChanges();
            return new SM.BoardTask{
                Id=task.Id,
                Name=task.Name,
                Status=task.Status
            };
        }
    
    }
}