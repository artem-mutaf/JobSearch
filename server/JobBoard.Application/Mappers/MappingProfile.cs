using AutoMapper;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Core.ValueObjects;

namespace JobBoard.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Applicant, ApplicantDto>()
            .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id).ToList()))
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Password, opt => opt.Ignore()); // Игнорируем Password в ответе

        CreateMap<ApplicantDto, Applicant>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new Category { Id = id }).ToList()))
            .ForMember(dest => dest.Chats, opt => opt.Ignore())
            .ForMember(dest => dest.IsEmailConfirmed, opt => opt.Ignore());

        CreateMap<ContactInfo, ContactInfoDto>();
        CreateMap<ContactInfoDto, ContactInfo>();

        CreateMap<Location, LocationDto>();
        CreateMap<LocationDto, Location>();

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.ApplicantIds, opt => opt.MapFrom(src => src.Applicants.Select(a => a.Id).ToList()))
            .ForMember(dest => dest.EmployerIds, opt => opt.MapFrom(src => src.Employers.Select(e => e.Id).ToList()))
            .ForMember(dest => dest.VacancyIds, opt => opt.MapFrom(src => src.Vacancies.Select(v => v.Id).ToList()));
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Applicants, opt => opt.Ignore())
            .ForMember(dest => dest.Employers, opt => opt.Ignore())
            .ForMember(dest => dest.Vacancies, opt => opt.Ignore());

        CreateMap<Chat, ChatDto>();
        CreateMap<ChatDto, Chat>()
            .ForMember(dest => dest.Applicant, opt => opt.Ignore())
            .ForMember(dest => dest.Employer, opt => opt.Ignore())
            .ForMember(dest => dest.Vacancy, opt => opt.Ignore())
            .ForMember(dest => dest.Messages, opt => opt.Ignore());

        CreateMap<Employer, EmployerDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<EmployerDto, Employer>()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.Vacancies, opt => opt.Ignore())
            .ForMember(dest => dest.Chats, opt => opt.Ignore())
            .ForMember(dest => dest.IsEmailConfirmed, opt => opt.Ignore());

        CreateMap<Vacancy, VacancyDto>();
        CreateMap<VacancyDto, Vacancy>()
            .ForMember(dest => dest.Employer, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, Message>()
            .ForMember(dest => dest.Chat, opt => opt.Ignore())
            .ForMember(dest => dest.SentAt, opt => opt.Ignore());
    }
}