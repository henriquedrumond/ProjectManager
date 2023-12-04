namespace ProjectManagerApi.Services;

using AutoMapper;
using BCrypt.Net;
using ProjectManagerApi.Entities;
using ProjectManagerApi.Helpers;
using ProjectManagerApi.Models;

public interface ITaskService
{
    IEnumerable<Task> GetAll();

    IEnumerable<Task> GetByProjectId(Guid id);

    int GetPerformanceProjectId();
    Task GetById(Guid id);
    void Create(CreateTaskRequest model);
    void Update(Guid id, UpdateTaskRequest model);
    void Delete(Guid id);
}

public class TaskService : ITaskService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public TaskService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Task> GetAll()
    {
        return _context.Tasks;
    }

    public Task GetById(Guid id)
    {
        return getTask(id);
    }

    public IEnumerable<Task> GetByProjectId(Guid id)
    {
        var task = _context.Tasks.Where(x => x.ProjectId == id).ToList();
        if (task == null) throw new KeyNotFoundException("Prject not found");
        return task.AsEnumerable();
    }

    public int GetPerformanceProjectId()
    {
        var task = _context.Tasks.Where(x => x.Target == DateTime.Now.Date.AddDays(-30) && x.Status == Status.Done).ToList();
        return task.Count();
    }

    public void Create(CreateTaskRequest model)
    {
        // validate
        if (string.IsNullOrEmpty(model.Id.ToString()))
            throw new AppException("Id is necessary");

        var project = _context.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault();

        if (project == null)
            throw new AppException("Project is not found");

        var tasks = _context.Tasks.Where(x => x.ProjectId == model.ProjectId);

        if (tasks.Count() >= 20)
            throw new AppException("Project is tasks full.");

        var task = _mapper.Map<Task>(model);

        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public void Update(Guid id, UpdateTaskRequest model)
    {
        var task = getTask(id);

        // validate
        if (task == null)
            throw new AppException("Task Not Found.");

        // copy model to user and save
        _mapper.Map(model, task);
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = getTask(id);
        _context.Tasks.Remove(user);
        _context.SaveChanges();
    }

    private Task getTask(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) throw new KeyNotFoundException("Task not found");
        return task;
    }
}