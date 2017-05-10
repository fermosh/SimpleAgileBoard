using System.Collections.Generic;
using SimpleAgileBoard.Web.Models.ServiceModels;
using System.Linq;
using System;
namespace SimpleAgileBoard.Web.Services
{
    public interface ITaskService
    {
        IEnumerable<BoardTask> GetAllTasks();
        BoardTask MoveTask(Guid Id,BoardTaskStatus status);
    }
}