using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Business.Service;
using TaskManager.DataStore;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;
using Test.TaskManager.Business.Service.Mock;
using Xunit;

namespace Test.TaskManager.Business.Service
{
    public class TaskPriorityandDueDateValidationTest
    {
       
        private readonly ILogger<TaskPriorityandDueDateValidation> logger;
        private readonly Mock<ITaskManagerDataStore<TaskData>> mockTaskManagerDataStore;

        public TaskPriorityandDueDateValidationTest()
        {
            mockTaskManagerDataStore = DataStoreMock.GetTaskManagerDataStore();
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = loggerFactory.CreateLogger<TaskPriorityandDueDateValidation>();
        }

        [Fact]
        public async Task Execute_PastDueDate_ValidationBusinessService()
        {
            /// Arrange
            TaskStoreGetAllDataStoreService GetAllStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(GetAllStoreservice, logger);

            var newTask = new TaskData()
            {
                Name = "Task1",
                Description = "my first task to do",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(3)
            };

            /// Act
            var result = await validationService.ExecuteAsync(newTask, CancellationToken.None);

            /// Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Execute_MaxPriority_ValidationBusinessService()
        {
            /// Arrange
            TaskStoreGetAllDataStoreService GetAllStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(GetAllStoreservice, logger);

            var allTasks = await GetAllStoreservice.ExecuteAsync(null, CancellationToken.None);
            var firsttask = allTasks.FirstOrDefault();
            firsttask.Priority = "High";

            /// Act
            var result = await validationService.ExecuteAsync(firsttask, CancellationToken.None);

            /// Assert
            Assert.False(result);
        }
    }
}
