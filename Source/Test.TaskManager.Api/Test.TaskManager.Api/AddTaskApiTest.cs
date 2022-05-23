using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Api.Contracts.Request;
using TaskManager.Api.Controllers;
using TaskManager.Business.Model;
using TaskManager.Business.Service;
using TaskManager.Business.Service.Profiles;
using TaskManager.DataStore;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;
using Test.TaskManager.Api.Mock;
using Xunit;

namespace Test.TaskManager.Api
{
    public class AddTaskApiTest
    {
        private readonly IMapper mapper;
        private readonly ILogger<TaskStoreAddDataStoreService> logger;
        private readonly ILogger<TaskPriorityandDueDateValidation> dueDateLogger;
        private readonly ILogger<TaskInfoAddBusinessService> addServiceLogger;
        private readonly Mock<ITaskManagerDataStore<TaskData>> mockTaskManagerDataStore;
        public AddTaskApiTest()
        {
            mockTaskManagerDataStore = DataStoreMock.GetTaskManagerDataStore();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = configurationProvider.CreateMapper();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = loggerFactory.CreateLogger<TaskStoreAddDataStoreService>();

            using var dueDateLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            dueDateLogger = dueDateLoggerFactory.CreateLogger<TaskPriorityandDueDateValidation>();

            using var addServiceLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            addServiceLogger = addServiceLoggerFactory.CreateLogger<TaskInfoAddBusinessService>();
        }

        [Fact]
        public async Task Call_Valid_AddTaskApi()
        {
            /// Arrange
            TaskStoreAddDataStoreService addStoreservice = new TaskStoreAddDataStoreService(mockTaskManagerDataStore.Object, logger);
            TaskStoreGetAllDataStoreService valiadteStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(valiadteStoreservice, dueDateLogger);
            TaskInfoAddBusinessService addservice = new TaskInfoAddBusinessService(addStoreservice, validationService, mapper, addServiceLogger);
            var newTask = new AddTaskRequest()
            {                
               Name = "Task1",
                Description = "my first task torhbjbnkwehrnkjfnirhweiosnfkwa do",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                DueDate = DateTime.Now.AddDays(3),
                Priority = 2,
                Status = 1
            };
           
            var api = new AddTaskController(addservice);

            /// Act
            var result = await api.Post(newTask, CancellationToken.None);

            /// Assert
            result.TaskDto.Name.Equals(newTask.Name);

        }
    }
}
