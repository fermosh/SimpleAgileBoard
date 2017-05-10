using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAgileBoard.Web.Controllers
{
    using Services;
    using Models;
    using Models.ServiceModels;
    public class HomeController : Controller
    {
        private readonly ITaskService __taskService;
        public HomeController(ITaskService taskService){
            __taskService = taskService;
        }
        public IActionResult Index()
        {
            var serviceModel = __taskService.GetAllTasks();
            Func<BoardTask,TaskViewModel> projectTask = sm => new TaskViewModel{Id=sm.Id,Name=sm.Name};
            var viewModel = new BoardViewModel{ 
                        ToDo=serviceModel.Where(t=>t.Status==BoardTaskStatus.TO_DO).Select(projectTask), 
                        InProgress=serviceModel.Where(t=>t.Status==BoardTaskStatus.IN_PROGRESS).Select(projectTask), 
                        Done=serviceModel.Where(t=>t.Status==BoardTaskStatus.DONE).Select(projectTask)
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
