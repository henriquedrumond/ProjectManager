using System;
using AutoMapper;
using ProjectManagerApi.Entities;
using ProjectManagerApi.Helpers;
using ProjectManagerApi.Models;

namespace ProjectManagerApi.Services
{
    public interface IHistoryService
    {
        IEnumerable<History> GetAll();
        History GetById(Guid id);
        void Create(CreateHistoryRequest model);
    }

    public class HistoryService : IHistoryService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public HistoryService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<History> GetAll()
        {
            return _context.Histories;
        }

        public History GetById(Guid id)
        {
            return getHistory(id);
        }

        public void Create(CreateHistoryRequest model)
        {
            // validate
            if (string.IsNullOrEmpty(model.Id.ToString()))
                throw new AppException("Id is necessary");

            var user = _mapper.Map<Project>(model);

            _context.Projects.Add(user);
            _context.SaveChanges();
        }

        private History getHistory(Guid id)
        {
            var history = _context.Histories.Find(id);
            if (history == null) throw new KeyNotFoundException("History not found");
            return history;
        }
    }
}

