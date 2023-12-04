namespace ProjectManagerApi.Services;

using AutoMapper;
using BCrypt.Net;
using ProjectManagerApi.Entities;
using ProjectManagerApi.Helpers;
using ProjectManagerApi.Models;

public interface IProjectService
{
    IEnumerable<Project> GetAll();
    Project GetById(Guid id);
    void Create(CreateProjectRequest model);
    void Update(Guid id, UpdateProjectRequest model);
    void Delete(Guid id);
}

public class ProjectService : IProjectService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public ProjectService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Project> GetAll()
    {
        return _context.Projects;
    }

    public Project GetById(Guid id)
    {
        return getProject(id);
    }

    public void Create(CreateProjectRequest model)
    {
        // validate
        if (string.IsNullOrEmpty(model.Id.ToString()))
            throw new AppException("Id is necessary");

        var user = _mapper.Map<Project>(model);

        _context.Projects.Add(user);
        _context.SaveChanges();
    }

    public void Update(Guid id, UpdateProjectRequest model)
    {
        var project = getProject(id);

        // validate
        if (project == null)
            throw new AppException("Project Not Found.");

        // copy model to user and save
        _mapper.Map(model, project);
        _context.Projects.Update(project);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var tasks = _context.Tasks.Where(x => x.Status != Status.Done);

        // validate
        if (tasks != null)
            throw new AppException("It is not possible to delete the project. It has unfinished activities. Please update the tasks and try again.");

        var user = getProject(id);
        _context.Projects.Remove(user);
        _context.SaveChanges();
    }

    private Project getProject(Guid id)
    {
        var project = _context.Projects.Find(id);
        if (project == null) throw new KeyNotFoundException("Project not found");
        return project;
    }
}