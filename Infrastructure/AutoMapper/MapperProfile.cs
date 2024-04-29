using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.DTOs.GroupDto;
using Domain.DTOs.MentorDto;
using Domain.DTOs.MentorGroupDto;
using Domain.DTOs.ProgressBookDto;
using Domain.DTOs.StudentDto;
using Domain.DTOs.StudentGroupDto;
using Domain.DTOs.TimeTableDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // STUDENT
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentsDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();

        // COURSE
        CreateMap<Course, AddCourseDto>().ReverseMap();
        CreateMap<Course, GetCoursesDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();

        // GROUP
        CreateMap<Group, AddGroupDto>().ReverseMap();
        CreateMap<Group, GetGroupsDto>().ReverseMap();
        CreateMap<Group, UpdateGroupDto>().ReverseMap();

        // MENTOR
        CreateMap<Mentor, AddMentorDto>().ReverseMap();
        CreateMap<Mentor, GetMentorsDto>().ReverseMap();
        CreateMap<Mentor, UpdateMentorDto>().ReverseMap();

        // STUDENT GROUP
        CreateMap<StudentGroup, AddStudentGroupDto>().ReverseMap();
        CreateMap<StudentGroup, GetStudentGroupDto>().ReverseMap();
        CreateMap<StudentGroup, UpdateStudentGroupDto>().ReverseMap();

        // MENTOR GROUP
        CreateMap<MentorGroup, AddMentorGroupDto>().ReverseMap();
        CreateMap<MentorGroup, GetMentorGroupDto>().ReverseMap();
        CreateMap<MentorGroup, UpdateMentorGroupDto>().ReverseMap();

        // TIMETABLE
        CreateMap<TimeTable, AddTimeTableDto>().ReverseMap();
        CreateMap<TimeTable, GetTimeTableDto>().ReverseMap();
        CreateMap<TimeTable, UpdateTimeTableDto>().ReverseMap();

        // PROGRESS BOOK
        CreateMap<ProgressBook, AddProgressBookDto>().ReverseMap();
        CreateMap<ProgressBook, GetProgressBookDto>().ReverseMap();
        CreateMap<ProgressBook, UpdateProgressBookDto>().ReverseMap();
        //ForMembers
        CreateMap<AddTimeTableDto, TimeTable>()
            .ForMember(sDto => sDto.FromTime, opt => opt.MapFrom(t => TimeSpan.Parse(t.FromTime)))
            .ForMember(sDto => sDto.ToTime, opt => opt.MapFrom(t => TimeSpan.Parse(t.ToTime)));


        // // ignore
        // CreateMap<Student, AddStudentDto>()
        //     .ForMember(dest => dest.FirstName, opt => opt.Ignore());


    }
}