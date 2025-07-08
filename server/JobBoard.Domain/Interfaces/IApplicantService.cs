using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Core.Interfaces;

public interface IApplicantService
{
    Task<Applicant> CreateApplicantAsync(Applicant applicant, List<Guid> categoryIds);
    Task<Applicant?> GetApplicantByIdAsync(Guid id);
    Task<IEnumerable<Applicant>> GetAllApplicantsAsync();
    Task UpdateApplicantAsync(Applicant applicant, List<Guid> categoryIds);
    Task DeleteApplicantAsync(Guid id);
    Task<Chat> RespondToVacancyAsync(Guid applicantId, Guid vacancyId);
}