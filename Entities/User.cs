namespace Todo.Entities;

public class User
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string PasswordHash { get; set; }

    public required string Mail { get; set; }

    public int Age { get; set; }

    public bool IsActive { get; set; }

    // Navigation: a User has many Tasks
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
