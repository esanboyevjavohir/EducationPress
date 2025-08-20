using EduPress.Application.Common.Email;
using EduPress.Application.Helpers.GenerateJWT;
using EduPress.Application.MappingProfiles;
using EduPress.Application.Models.User;
using EduPress.Application.Services.Implement;
using EduPress.Application.Services.Interface;
using EduPress.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduPress.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddServices(env);

            services.AddEmailConfiguration(configuration);

            services.AddJwtConfiguration(configuration);

            services.RegisterAutoMapper();

            services.RegisterCashing();

            return services;
        }

        private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactLocationsService, ContactLocationsService>();
            services.AddScoped<ICourseFaqsService, CourseFaqsService>();
            services.AddScoped<ICourseInstructorService, CourseInstructorService>();
            services.AddScoped<ICourseLessonsService, CourseLessonsService>();
            services.AddScoped<ICourseSectionService, CourseSectionService>();
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IInstructorsService, InstructorsService>();
            services.AddScoped<ILessonProgressService, LessonProgressService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReviewRepliesService, ReviewRepliesService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICourseLessonExportService, CourseLessonExportService>();
            services.AddScoped<ICategoryExcelImportService, CategoryExcelImportService>();
            services.AddScoped<IValidator<CreateUserModel>, CreateUserValidator>();
            services.AddScoped<IValidator<ResetPasswordModel>, ResetPasswordValidator>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfilesMarker));
        }

        private static void RegisterCashing(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection("JwtSettings"));
        }
    }
}
