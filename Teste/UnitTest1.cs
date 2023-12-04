namespace Teste;
using ProjectManagerApi.Services;
using ProjectManagerApi.Models;
using ProjectManagerApi.Entities;
using System;

[TestClass]
public class UnitTest1
{
    private readonly ProjectService _projectService;
    private readonly TaskService _taskService;

    private Guid GuidProject = Guid.NewGuid();
    private Guid GuidTask = Guid.NewGuid();

    public UnitTest1(ProjectService projectService, TaskService taskService)
    {
        _projectService = projectService;
        _taskService = taskService;
    }

    [TestMethod]
    public void TestMethod1()
    {
        CreateProjectRequest createProjectRequest = new CreateProjectRequest()
        {
            CreateDate = DateTime.Now.Date,
            Id = GuidProject,
            Name = "TESTE",
            Owner = "Ricardo"

        };

        _projectService.Create(createProjectRequest);

        Assert.Equals(_projectService.GetById(GuidProject), createProjectRequest);
    }

    [TestMethod]
    public void TestMethod2()
    {
        CreateTaskRequest createTaskRequest = new CreateTaskRequest()
        {
            Id = GuidTask,
            ProjectId = GuidProject,
            description = "Test Task",
            Owner = "Ricardo",
            Target = DateTime.Now.Date,
            Time = 0,
            taskPriority = ProjectManagerApi.Entities.TaskPriority.High,
            Status = "New",
            Comment = "Test"
        };

        _taskService.Create(createTaskRequest);

        Assert.Equals(_taskService.GetById(GuidTask), createTaskRequest);
    }

    [TestMethod]
    public void TestMethod3()
    {
        CreateTaskRequest createTaskRequest = new CreateTaskRequest()
        {
            Id = GuidTask,
            ProjectId = GuidProject,
            description = "Test Task",
            Owner = "Ricardo",
            Target = DateTime.Now.Date,
            Time = 0,
            taskPriority = ProjectManagerApi.Entities.TaskPriority.High,
            Status = "New",
            Comment = "Test"
        };

        Assert.Equals(_taskService.GetById(GuidTask), createTaskRequest);
    }

    [TestMethod]
    public void TestMethod4()
    {
        Assert.Equals(_taskService.GetById(Guid.NewGuid()), "Task Not Found.");
    }

    [TestMethod]
    public void TestMethod5()
    {
        Assert.Equals(_projectService.GetById(GuidProject), "Project Not Found.");
    }
}
