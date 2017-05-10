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
            try{
                var sm = __taskService.MoveTask(id,BoardTaskStatus.IN_PROGRESS);
                if(sm!=null)
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                return NotFound("No such task");
            }catch(Exception e){
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Drop(Guid id){
            try{
                var sm = __taskService.MoveTask(id,BoardTaskStatus.TO_DO);
                if(sm!=null)
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                return NotFound("No such task");
            }catch(Exception e){
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Finish(Guid id){
            try{
                var sm = __taskService.MoveTask(id,BoardTaskStatus.DONE);
                if(sm!=null)
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                return NotFound("No such task");
            }catch(Exception e){
                return StatusCode(500,e);
            }
        }
        [HttpPost]
        public IActionResult Retake(Guid id){
            try{
                var sm = __taskService.MoveTask(id,BoardTaskStatus.IN_PROGRESS);
                if(sm!=null)
                    return Ok(new TaskViewModel{Id=sm.Id,Name=sm.Name});
                return NotFound("No such task");
            }catch(Exception e){
                return StatusCode(500,e);
            }
        }
    }
}