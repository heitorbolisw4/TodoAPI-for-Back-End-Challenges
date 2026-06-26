using TaskStatus = Todo.Enum.TaskStatus;

namespace Todo.Entities;

public class Task
{
    public int Id { get; set; }

    // FK to User
    public Guid UserId { get; set; }

    public required string TaskName { get; set; }

    public TaskStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation: a Task belongs to one User
    public User User { get; set; } = null!;
}
