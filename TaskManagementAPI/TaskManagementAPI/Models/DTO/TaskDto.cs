namespace TaskManagementAPI.Models.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required DateOnly DueDate { get; set; }

        public required string Priority { get; set; }

        public required string Status { get; set; }

        public string? CreatedBy { get; set; }

        public string? CreatedByName { get; set; }

        public string AssignedTo { get; set; }

        public string? AssignedToName { get; set; }
    }
}
