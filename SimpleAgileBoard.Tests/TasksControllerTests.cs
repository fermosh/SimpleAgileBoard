using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SimpleAgileBoard.Web.Data;
using SimpleAgileBoard.Web.Models;
using SimpleAgileBoard.Web.Models.ServiceModels;
using SimpleAgileBoard.Web.Services;
using SimpleAgileBoard.Web.Controllers;
using Microsoft.AspNetCore.Mvc; 
using Moq;

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
        private ITaskService service;
        private Mock<ITaskService> serviceMock;

        public TaskControllerTests(){
            serviceMock = new Mock<ITaskService>();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.nonExistentTaskId = Guid.Empty;
            this.pendingTaskId = new Guid("8753CA8C-6ECE-4410-AF11-517C3E0CB64F");
            this.wipTaskId = new Guid("EC51D296-40E8-44E7-B56B-846DF44FD72E");
            this.doneTaskId = new Guid("3988FF4C-ED57-4667-B351-6375EE2D8E11");
            this.service = serviceMock.Object;
        }

        [TestMethod]
        public void StartAPendingTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.IN_PROGRESS
                        ) 
                    )
            )
            .Returns(
                (Guid id,BoardTaskStatus status) => new BoardTask{
                    Id=id,
                    Name=String.Empty,
                    Status=status
                }
            ) 
            .Callback<Guid,BoardTaskStatus>( 
                (id,status) => Console.WriteLine("{0}:\tMock Service - Moved task {1} to status {2}",DateTime.Now , id,status)
            );
            
            var controller = new TasksController(service);
            var result = controller.Start(pendingTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(pendingTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void StartAStartedTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.IN_PROGRESS
                        ) 
                    )
            )
            .Callback<Guid,BoardTaskStatus>( 
                (id,status) => Console.WriteLine("{0}:\tMock Service - About to throw an exception!",DateTime.Now)
            )
            .Throws( new InvalidOperationException() ) ;
            var controller = new TasksController(service);
            var result = controller.Start(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void StartNonExistentTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.Is<Guid>( id=>id == nonExistentTaskId ), 
                        It.IsAny<BoardTaskStatus>() 
                    )
            ).Returns( (BoardTask) null ) ;

            var controller = new TasksController(service);
            var result = controller.Start(nonExistentTaskId);
            serviceMock.Verify( svc => svc.MoveTask(It.IsAny<Guid>(),It.IsAny<BoardTaskStatus>()) );
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void DropOngoingTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.TO_DO
                        ) 
                    )
            ).Returns(
                (Guid id,BoardTaskStatus status) => new BoardTask{
                    Id=id,
                    Name=String.Empty,
                    Status=status
                }
            ) ;
            var controller = new TasksController(service);
            var result = controller.Drop(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            serviceMock.Verify( svc => svc.MoveTask(It.IsAny<Guid>(),It.IsAny<BoardTaskStatus>()), Times.AtLeastOnce() );
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(wipTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void DropDoneTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.TO_DO
                        ) 
                    )
            ).Throws( new InvalidOperationException() ) ;
            var controller = new TasksController(service);
            var result = controller.Drop(doneTaskId);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void DropNonExistentTask()
        {
            serviceMock.Setup( 
            svc => svc.MoveTask( 
                    It.Is<Guid>( id=>id == nonExistentTaskId ), 
                    It.IsAny<BoardTaskStatus>() 
                )
            ).Returns( (BoardTask) null ) ;
            serviceMock.Verify( svc => svc.GetAllTasks(), Times.Never() );
            var controller = new TasksController(service);
            var result = controller.Drop(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void FinishOngoingTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.DONE
                        ) 
                    )
            ).Returns(
                (Guid id,BoardTaskStatus status) => new BoardTask{
                    Id=id,
                    Name=String.Empty,
                    Status=status
                }
            ) ;
            var controller = new TasksController(service);
            var result = controller.Finish(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(wipTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void FinishFinishedTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.DONE
                        ) 
                    )
            ).Throws( new InvalidOperationException() ) ;
            var controller = new TasksController(service);
            var result = controller.Finish(doneTaskId);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void FinishNonExistentTask()
        {
            serviceMock.Setup( 
            svc => svc.MoveTask( 
                    It.Is<Guid>( id=>id == nonExistentTaskId ), 
                    It.IsAny<BoardTaskStatus>() 
                )
            ).Returns( (BoardTask) null ) ;
            var controller = new TasksController(service);
            var result = controller.Finish(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public void RetakeDoneTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.IN_PROGRESS
                        ) 
                    )
            ).Returns(
                (Guid id,BoardTaskStatus status) => new BoardTask{
                    Id=id,
                    Name=String.Empty,
                    Status=status
                }
            ) ;
            var controller = new TasksController(service);
            var result = controller.Retake(doneTaskId);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
            Assert.IsNotNull((result as OkObjectResult).Value);
            var returnedTask = (result as OkObjectResult).Value as TaskViewModel;
            Assert.AreEqual(doneTaskId, returnedTask.Id);
        }
        [TestMethod]
        public void RetakeOngoingTask()
        {
            serviceMock.Setup( 
                svc => svc.MoveTask( 
                        It.IsAny<Guid>(), 
                        It.Is<BoardTaskStatus>( 
                            status=>status==BoardTaskStatus.IN_PROGRESS
                        ) 
                    )
            ).Throws( new InvalidOperationException() ) ;
            var controller = new TasksController(service);
            var result = controller.Retake(wipTaskId);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void RetakeNonExistentTask()
        {
            serviceMock.Setup( 
            svc => svc.MoveTask( 
                    It.Is<Guid>( id=>id == nonExistentTaskId ), 
                    It.IsAny<BoardTaskStatus>() 
                )
            ).Returns( (BoardTask) null ) ;
            var controller = new TasksController(service);
            var result = controller.Retake(nonExistentTaskId);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

    }
}
