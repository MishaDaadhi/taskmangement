using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.DataStore;
using TaskManager.DataStore.Contract;
namespace Test.TaskManager.Api.Mock
{
    public static class DataStoreMock
    {
        public static Mock<ITaskManagerDataStore<TaskData>> GetTaskManagerDataStore()
        {
            var tasks = new List<TaskData> {
               new TaskData() { Id = Guid.Parse("{BA0EB0EF-B69B-46FD-B8E2-41B4178AE7CB}"),
                   Name = "FirstTask",
                   Description = "my first task to do",
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(5),
                   DueDate = DateTime.Now.AddDays(3),
                   Priority = "High",
                   Status = "New" }
           };
            
            var mockTaskManagerDataStore = new Mock<ITaskManagerDataStore<TaskData>>();
            mockTaskManagerDataStore.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);
            mockTaskManagerDataStore.Setup(repo => repo.UpdateAsync(It.IsAny<TaskData>()))
                .ReturnsAsync(
                (TaskData taskData) =>
                {
                    return taskData;
                })
                ;
            mockTaskManagerDataStore.Setup(repo => repo.AddAsync(It.IsAny<TaskData>()))
                .ReturnsAsync(
                (TaskData taskData) =>
                {
                    tasks.Add(taskData);
                    return taskData;
                });
            mockTaskManagerDataStore.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                (Guid id) =>
                {
                    return tasks.FirstOrDefault(t => t.Id == id);

                });
            return mockTaskManagerDataStore;
        }
    }
}
