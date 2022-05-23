using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
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
    public class TaskInfoUpdateBusinessServiceTest
    {
        private readonly IMapper mapper;
        private readonly ILogger<TaskStoreUpdateDataStoreService> logger;
        private readonly ILogger<TaskPriorityandDueDateValidation> validationLogger;
        private readonly ILogger<TaskInfoUpdateBusinessService> updateServiceLogger;
        private readonly Mock<ITaskManagerDataStore<TaskData>> mockTaskManagerDataStore;
        readonly string newTaskName = "TestUpdate";
        public TaskInfoUpdateBusinessServiceTest()
        {
            mockTaskManagerDataStore = DataStoreMock.GetTaskManagerDataStore();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = configurationProvider.CreateMapper();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = loggerFactory.CreateLogger<TaskStoreUpdateDataStoreService>();

            using var validationLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            validationLogger = validationLoggerFactory.CreateLogger<TaskPriorityandDueDateValidation>();

            using var updateServiceLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            updateServiceLogger = updateServiceLoggerFactory.CreateLogger<TaskInfoUpdateBusinessService>();
        }

        [Fact]
        public async Task Execute_Valid_AddBusinessService()
        {
            /// Arrange
            TaskStoreUpdateDataStoreService addStoreservice = new TaskStoreUpdateDataStoreService(mockTaskManagerDataStore.Object, logger);
            TaskStoreGetAllDataStoreService GetAllStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(GetAllStoreservice, validationLogger);
            TaskInfoUpdateBusinessService UpdateTaskservice = new TaskInfoUpdateBusinessService(addStoreservice, validationService, mapper, updateServiceLogger);
            var newTask = await GetAllStoreservice.ExecuteAsync(null,CancellationToken.None);
            var taskForUpdate = mapper.Map<TaskInfo>( newTask.FirstOrDefault());
            taskForUpdate.Name = newTaskName;
            taskForUpdate.Priority = TaskPriority.Low;

            /// Act
            var result = await UpdateTaskservice.ExecuteAsync(taskForUpdate, CancellationToken.None);

            /// Assert
            Assert.Equal(result.TaskDto.Name, newTaskName);

        }
    }
}
