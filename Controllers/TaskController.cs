namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerApi.Entities;
using ProjectManagerApi.Services;
using ProjectManagerApi.Models;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private ITaskService _taskService;
    private IHistoryService _historyService;
    private IMapper _mapper;

    public TaskController(
        ITaskService taskService, IHistoryService historyService,
        IMapper mapper)
    {
        _historyService = historyService;
        _taskService = taskService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var task = _taskService.GetAll();
        return Ok(task);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var task = _taskService.GetById(id);
        return Ok(task);
    }

    [HttpGet("{projectid}")]
    public IActionResult GetByProjectId(Guid id)
    {
        var task = _taskService.GetByProjectId(id);
        return Ok(task);
    }

    [HttpGet("{statusUser}")]
    public IActionResult GetPerformanceProjectId(StatusUser statusUser)
    {
        if (statusUser == StatusUser.Manager)
        {
            var task = _taskService.GetPerformanceProjectId();
            return Ok(task);
        }
        else
        {
            return BadRequest("You not authorized.");
        }
    }

    [HttpPost]
    public IActionResult Create(CreateTaskRequest model)
    {
         _taskService.Create(model);
        return Ok(new { message = "Task created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateTaskRequest model)
    {
        _taskService.Update(id, model);

        CreateHistoryRequest history = new CreateHistoryRequest()
        {
            ChangeFields = JsonSerializer.Serialize(model),
            Id = Guid.NewGuid(),
            TaskId = model.Id,
            Modify = DateTime.Now.Date,
            Table = "Tasks"
        };

        _historyService.Create(history);

        return Ok(new { message = "Task updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _taskService.Delete(id);
        return Ok(new { message = "Task deleted" });
    }
}