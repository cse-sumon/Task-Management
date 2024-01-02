using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDto>> GetAllTask();

        Task<TaskDto> GetTaskById(int id);

        Task InsertTask(TaskDto taskDto);

        Task UpdateTask(TaskDto taskDto);

        Task DeleteTask(TaskDto taskDto);


    }
}
