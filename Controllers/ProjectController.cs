namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerApi.Services;
using ProjectManagerApi.Models;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private IProjectService _projectService;
    private IMapper _mapper;

    public ProjectController(
        IProjectService userService,
        IMapper mapper)
    {
        _projectService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _projectService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var user = _projectService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateProjectRequest model)
    {
        _projectService.Create(model);
        return Ok(new { message = "Project created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateProjectRequest model)
    {
        _projectService.Update(id, model);
        return Ok(new { message = "Project updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _projectService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}