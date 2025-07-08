using JobBoard.Application.DTOs;

public class RegisterDto
{
    public string Role { get; set; } // "Applicant" или "Employer"
    public ApplicantDto? Applicant { get; set; }
    public EmployerDto? Employer { get; set; }
}