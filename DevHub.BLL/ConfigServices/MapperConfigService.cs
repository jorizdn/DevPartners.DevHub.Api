using AutoMapper;
using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevHub.BLL.ConfigServices
{

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserInfo, BookLog>();
            CreateMap<UserInfo, ClientMaster>();
            CreateMap<BookLog, BookLogInfo>();
            CreateMap<UserInfo, BookLogInfo>();
            CreateMap<ClientMaster, BookLogInfo>();

            CreateMap<BookLog, TimeTrackingLogger>()
                .ForMember(a => a.BookingId, b => b.ResolveUsing(a => a.Id));

            CreateMap<TimeTrackingLogger, TimeTrackerDetails>();

            CreateMap<AspNetUserRoles, IdentityUserRole<string>>();
            CreateMap<AspNetUserClaims, IdentityUserClaim<string>>();
            CreateMap<ApplicationUser, AspNetUsers>();
        }
    }

    public static class MapperConfigService
    {
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }
    }
}
