using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SimpleAgileBoard.Web.Data;
using SimpleAgileBoard.Web.Models;
using SimpleAgileBoard.Web.Models.ServiceModels;
using SimpleAgileBoard.Web.Services;
using SimpleAgileBoard.Web.Controllers;
using Microsoft.AspNetCore.Mvc; 

namespace SimpleAgileBoard.Tests
{
    [TestClass]
    public class TaskControllerTests
    {
        private Guid pendingTaskId;
        private Guid nonExistentTaskId;
        private Guid wipTaskId;
        private Guid doneTaskId;
        private DbContextOptionsBuilder<AgileBoardDbContext> optionsBuilder;
        private SqlServerTaskService service;

        [TestInitialize]
        public void TestInitialize()
        {
            this.nonExistentTaskId = Guid.Empty;
            this.pendingTaskId = new Guid("8753CA8C-6ECE-4410-AF11-517C3E0CB64F");
            this.wipTaskId = new Guid("EC51D296-40E8-44E7-B56B-846DF44FD72E");
            this.doneTaskId = new Guid("3988FF4C-ED57-4667-B351-6375EE2D8E11");
            this.optionsBuilder = new DbContextOptionsBuilder<AgileBoardDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=SimpleAgileBoard;Trusted_Connection=True;");
            this.service = new SqlServerTaskService(optionsBuilder.Options);
        }

        [TestMethod]
        public void StartAPendingTask()
        {
            var controller = new TasksController(service);
            var result = controller.Start(pendingTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(pendingTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void StartNonExistentTask()
        {
            var controller = new TasksController(service);
            var result = controller.Start(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void DropOngoingTask()
        {
            var controller = new TasksController(service);
            var result = controller.Drop(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(wipTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void DropNonExistentTask()
        {
            var controller = new TasksController(service);
            var result = controller.Drop(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void FinishOngoingTask()
        {
            var controller = new TasksController(service);
            var result = controller.Finish(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(wipTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void FinishNonExistentTask()
        {
            var controller = new TasksController(service);
            var result = controller.Finish(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void RetakeDoneTask()
        {
            var controller = new TasksController(service);
            var result = controller.Retake(doneTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(doneTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void RetakeNonExistentTask()
        {
            var controller = new TasksController(service);
            var result = controller.Retake(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

    }
}
