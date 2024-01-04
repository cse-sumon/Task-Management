using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models.Domain;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TaskRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

       
        public async Task<IEnumerable<TaskDto>> GetAllTask()
        {
            var result = await (from t in _context.Tasks
                                join c in _userManager.Users on t.CreatedBy equals c.Id
                                join a in _userManager.Users on t.AssignedTo equals a.Id
                                select new TaskDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Description = t.Description,
                                    DueDate = t.DueDate,
                                    Priority = t.Priority,
                                    Status = t.Status,
                                    CreatedBy = t.CreatedBy,
                                    AssignedTo = t.AssignedTo,
                                    CreatedByName = c.FullName,
                                    AssignedToName = a.FullName,

                                }).AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(result);

        }

        public async Task<TaskDto> GetTaskById(int id)
        {

            var result = await (from t in _context.Tasks
                                .Where(t => t.Id == id)
                                join c in _userManager.Users on t.CreatedBy equals c.Id
                                join a in _userManager.Users on t.AssignedTo equals a.Id
                                select new TaskDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Description = t.Description,
                                    DueDate = t.DueDate,
                                    Priority = t.Priority,
                                    Status = t.Status,
                                    CreatedBy = t.CreatedBy,
                                    AssignedTo = t.AssignedTo,
                                    CreatedByName = c.FullName,
                                    AssignedToName = a.FullName,

                                }).AsNoTracking().FirstOrDefaultAsync();



            return _mapper.Map<TaskDto>(result);
        }

        public async Task InsertTask(TaskDto taskDto)
        {
            var result = _mapper.Map<TaskModel>(taskDto);
            _context.Tasks.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskDto taskDto)
        {
            var result = _mapper.Map<TaskModel>(taskDto);

            _context.Tasks.Update(result);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteTask(TaskDto taskDto)
        {

            var result = _mapper.Map<TaskModel>(taskDto);

            if (result != null)
            {
                _context.Tasks.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
