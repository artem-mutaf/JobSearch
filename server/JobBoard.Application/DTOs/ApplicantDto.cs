namespace JobBoard.Application.DTOs;

public class ApplicantDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public ContactInfoDto ContactInfo { get; set; }
    public LocationDto Location { get; set; }
    public string? ResumeUrl { get; set; }
    public List<Guid> CategoryIds { get; set; } = new(); 
    public string Password { get; set; } 
}