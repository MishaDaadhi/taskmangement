using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using TaskManager.Business.Model;
using TaskManager.Business.Service;
using TaskManager.Business.Service.Profiles;
using TaskManager.DataStore;
using TaskManager.DataStore.Contract;
using TaskManager.DataStore.TaskStore;
using Test.TaskManager.Business.Service.Mock;
using Xunit;

namespace Test.TaskManager.Business.Service
{
    public class TaskInfoAddBusinessServiceTest
    {
        private readonly IMapper mapper;
        private readonly ILogger<TaskStoreAddDataStoreService> logger;
        private readonly ILogger<TaskPriorityandDueDateValidation> dueDateLogger;
        private readonly ILogger<TaskInfoAddBusinessService> addServiceLogger;
        private readonly Mock<ITaskManagerDataStore<TaskData>> mockTaskManagerDataStore;
        public TaskInfoAddBusinessServiceTest()
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
        public async Task Execute_Valid_AddBusinessService()
        {
            /// Arrange
            TaskStoreAddDataStoreService addStoreservice = new TaskStoreAddDataStoreService(mockTaskManagerDataStore.Object, logger);
            TaskStoreGetAllDataStoreService valiadteStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(valiadteStoreservice, dueDateLogger);
            TaskInfoAddBusinessService addservice = new TaskInfoAddBusinessService(addStoreservice, validationService, mapper, addServiceLogger);
            var newTask = new TaskInfo() {
                Name = "Task1", Description = "my first task to do", 
                StartDate = DateTime.Now, 
                EndDate = DateTime.Now.AddDays(5), 
                DueDate = DateTime.Now.AddDays(3), 
                Priority = TaskPriority.Middle, 
                Status = Status.New };

            /// Act
            var result = await addservice.ExecuteAsync(newTask, System.Threading.CancellationToken.None);

            /// Assert
            Assert.NotNull(result);

        }        
    }
}
