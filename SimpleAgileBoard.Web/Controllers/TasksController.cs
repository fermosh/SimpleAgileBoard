using System.Collections.Generic; 
using System.Linq; 
using System; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Routing; 
using SimpleAgileBoard.Web.Models;
using SimpleAgileBoard.Web.Services;
using SimpleAgileBoard.Web.Models.ServiceModels;

namespace SimpleAgileBoard.Web.Controllers
{
    public class TasksController:Controller
    {
        private readonly ITaskService __taskService;
        public TasksController(ITaskService taskService){
            __taskService = taskService;
        }
    
        [HttpPost]
        public IActionResult Start(Guid id){
            Console.WriteLine("{0}:\tTasksController - Starting Task: {1}",DateTime.Now,id);
            try{
                Console.WriteLine("{0}:\tTasksController - Calling __taskService.MoveTask('{1}',BoardTaskStatus.IN_PROGRESS)",DateTime.Now,id);
                var sm = __taskService.MoveTask(id,BoardTaskStatus.IN_PROGRESS);
                if(sm!=null){
                    Console.WriteLine("{0}:\tTasksController - Found a task: {{\r\n\tId: {1},\r\n\tName: {2},\r\n\tStatus: {3}}}",DateTime.Now,sm.Id,sm.Name,sm.Status);
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                }
                Console.WriteLine("{0}:\tTasksController - Task not found: {1}",DateTime.Now,id);
                return NotFound("No such task");
            }catch(Exception e){
                Console.WriteLine("{0}:\tTasksController - Error: {1}",DateTime.Now,e.Message);
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Drop(Guid id){
            Console.WriteLine("{0}:\tTasksController - Starting Task: {1}",DateTime.Now,id);
            try{
                Console.WriteLine("{0}:\tTasksController - Calling __taskService.MoveTask('{1}',BoardTaskStatus.TO_DO)",DateTime.Now,id);
                var sm = __taskService.MoveTask(id,BoardTaskStatus.TO_DO);
                if(sm!=null){
                    Console.WriteLine("{0}:\tTasksController - Found a task: {{\r\n\tId: {1},\r\n\tName: {2},\r\n\tStatus: {3}}}",DateTime.Now,sm.Id,sm.Name,sm.Status);
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                }
                Console.WriteLine("{0}:\tTasksController - Task not found: {1}",DateTime.Now,id);
                return NotFound("No such task");
            }catch(Exception e){
                Console.WriteLine("{0}:\tTasksController - Error: {1}",DateTime.Now,e.Message);
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Finish(Guid id){
           Console.WriteLine("{0}:\tTasksController - Starting Task: {1}",DateTime.Now,id);
            try{
                Console.WriteLine("{0}:\tTasksController - Calling __taskService.MoveTask('{1}',BoardTaskStatus.DONE)",DateTime.Now,id);
                var sm = __taskService.MoveTask(id,BoardTaskStatus.DONE);
                if(sm!=null){
                    Console.WriteLine("{0}:\tTasksController - Found a task: {{\r\n\tId: {1},\r\n\tName: {2},\r\n\tStatus: {3}}}",DateTime.Now,sm.Id,sm.Name,sm.Status);
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                }
                Console.WriteLine("{0}:\tTasksController - Task not found: {1}",DateTime.Now,id);
                return NotFound("No such task");
            }catch(Exception e){
                Console.WriteLine("{0}:\tTasksController - Error: {1}",DateTime.Now,e.Message);
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Retake(Guid id){
            Console.WriteLine("{0}:\tTasksController - Starting Task: {1}",DateTime.Now,id);
            try{
                Console.WriteLine("{0}:\tTasksController - Calling __taskService.MoveTask('{1}',BoardTaskStatus.IN_PROGRESS)",DateTime.Now,id);
                var sm = __taskService.MoveTask(id,BoardTaskStatus.IN_PROGRESS);
                if(sm!=null){
                    Console.WriteLine("{0}:\tTasksController - Found a task: {{\r\n\tId: {1},\r\n\tName: {2},\r\n\tStatus: {3}}}",DateTime.Now,sm.Id,sm.Name,sm.Status);
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                }
                Console.WriteLine("{0}:\tTasksController - Task not found: {1}",DateTime.Now,id);
                return NotFound("No such task");
            }catch(Exception e){
                Console.WriteLine("{0}:\tTasksController - Error: {1}",DateTime.Now,e.Message);
                return StatusCode(500,e);
            }
        }
    }
}