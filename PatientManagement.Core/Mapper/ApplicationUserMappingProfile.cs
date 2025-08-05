using AutoMapper;
using PatientManagement.Core.DTO;
using PatientManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.Mapper;

public class ApplicationUserMappingProfile:Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticationResponse>()
             .ForMember(dest => dest.UserID, opt =>
            opt.MapFrom(src => src.UserID))
              .ForMember(dest => dest.Email, opt =>
            opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.PersonName, opt =>
            opt.MapFrom(src => src.PersonName))
              .ForMember(dest => dest.Gender, opt =>
            opt.MapFrom(src => src.Gender))
              .ForMember(dest => dest.Success, opt =>
            opt.Ignore())
              .ForMember(dest => dest.Token, opt =>
            opt.Ignore());
        // Patient Mappings
        CreateMap<PatientAddRequest, Patient>()
            .ForMember(dest => dest.PatientID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<PatientUpdateRequest, Patient>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Patient, PatientResponse>()
            .ForMember(dest => dest.PatientID, opt => opt.MapFrom(src => src.PatientID))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => $"{src.PhoneNumber} | {src.Email}"))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));


    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }


}
