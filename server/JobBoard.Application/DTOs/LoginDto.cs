namespace JobBoard.Application.DTOs;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // "Applicant" или "Employer"
}