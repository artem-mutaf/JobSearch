using JobBoard.Core.Enums;

namespace JobBoard.Application.DTOs;

public class EmployerDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public EntityType EntityType { get; set; }
    public string CompanyDescription { get; set; }
    public string? About { get; set; }
    public ContactInfoDto ContactInfo { get; set; }
    public LocationDto Location { get; set; }
    public string? ImageUrl { get; set; }
    public string Password { get; set; } 
}

public class ContactInfoDto
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? SocialMediaUrl { get; set; }
}

public class LocationDto
{
    public string Address { get; set; }
    public string Region { get; set; }
}