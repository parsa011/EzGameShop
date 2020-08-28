using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using EzGame.Data.Context;
using EzGame.Domain.Entities;
using EzGame.Common.Identity;

namespace EzGame.IOC.IdentityConfig
{
    public static class AddIdentityOptionsExtensions
    {
        public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(
                    options => {
                        //Configure Password
                        options.User.RequireUniqueEmail = true;
                        options.User.AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 0;
                        options.Lockout.AllowedForNewUsers = true;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.SignIn.RequireConfirmedAccount = true;
                        options.SignIn.RequireConfirmedEmail = true;
                    })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}