namespace Todo.DTO
{
    public class RegisterRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}