namespace TaskManagementAPI.Models.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required DateTime DueDate { get; set; }

        public required string Priority { get; set; }

        public required string Status { get; set; }

        public required string CreatedBy { get; set; }

        public required string CreatedByName { get; set; }

        public required string AssignedTo { get; set; }

        public required string AssignedToName { get; set; }
    }
}
