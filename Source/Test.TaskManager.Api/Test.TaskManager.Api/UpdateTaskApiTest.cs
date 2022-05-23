using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class UpdateTaskApiTest
    {
        private readonly IMapper mapper;
        private readonly ILogger<TaskStoreUpdateDataStoreService> logger;
        private readonly ILogger<TaskPriorityandDueDateValidation> validationLogger;
        private readonly ILogger<TaskInfoUpdateBusinessService> updateServiceLogger;
        private readonly Mock<ITaskManagerDataStore<TaskData>> mockTaskManagerDataStore;
        readonly string newTaskName = "TestUpdate";
        public UpdateTaskApiTest()
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
        public async Task Call_Valid_UpdateTaskApi()
        {
            /// Arrange
            TaskStoreUpdateDataStoreService addStoreservice = new TaskStoreUpdateDataStoreService(mockTaskManagerDataStore.Object, logger);
            TaskStoreGetAllDataStoreService GetAllStoreservice = new TaskStoreGetAllDataStoreService(mockTaskManagerDataStore.Object);
            TaskPriorityandDueDateValidation validationService = new TaskPriorityandDueDateValidation(GetAllStoreservice, validationLogger);
            TaskInfoUpdateBusinessService UpdateTaskservice = new TaskInfoUpdateBusinessService(addStoreservice, validationService, mapper, updateServiceLogger);
            var newTask = await GetAllStoreservice.ExecuteAsync(null, CancellationToken.None);
            var taskForUpdate = mapper.Map<TaskInfo>(newTask.FirstOrDefault());
            taskForUpdate.Name = newTaskName;
            taskForUpdate.Priority = TaskPriority.Low;


            var api = new UpdateTaskController(UpdateTaskservice);

            /// Act
            var result = await api.Post(taskForUpdate, CancellationToken.None);

            /// Assert
            result.TaskDto.Name.Equals(newTaskName);

        }
    }
}
