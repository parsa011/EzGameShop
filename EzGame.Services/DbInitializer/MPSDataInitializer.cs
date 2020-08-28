using EzGame.Common.ViewModel.Settings;
using EzGame.Data.Context;
using EzGame.Domain.Entities;
using EzGame.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EzGame.Services.DbInitializer
{
    public static class EzGameDataInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            await using var context = serviceProvider.GetRequiredService<DatabaseContext>();
            await context.Database.EnsureCreatedAsync();
            if (context.Database != null)
            {
                // TODO main entities data :D
            }
        }
    }

    public class IdentityDbInitializer : IIdentityDbInitializer
    {
        private readonly IOptionsSnapshot<SiteSettings> _adminUserSeedOptions;
        private readonly UserManager<User> _applicationUserManager;
        private readonly ILogger<IdentityDbInitializer> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceScopeFactory _scopeFactory;

        public IdentityDbInitializer(
            UserManager<User> applicationUserManager,
            IServiceScopeFactory scopeFactory,
            RoleManager<IdentityRole> roleManager,
            IOptionsSnapshot<SiteSettings> adminUserSeedOptions,
            ILogger<IdentityDbInitializer> logger
        )
        {
            _applicationUserManager = applicationUserManager;
            _scopeFactory = scopeFactory;
            _roleManager = roleManager;
            _adminUserSeedOptions = adminUserSeedOptions;
            _logger = logger;
        }

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
            context.Database.Migrate();
        }

        /// <summary>
        /// Adds some default values to the IdentityDb
        /// </summary>
        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            var identityDbSeedData = serviceScope.ServiceProvider.GetService<IIdentityDbInitializer>();
            var result = identityDbSeedData.SeedDatabaseWithAdminUserAsync().Result;
            if (result == IdentityResult.Failed())
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
        {
            var adminUserSeed = _adminUserSeedOptions.Value.AdminUserSeed;

            var name = adminUserSeed.Username;
            var password = adminUserSeed.Password;
            var email = adminUserSeed.Email;
            var roleName = adminUserSeed.RoleName;
            var firstName = adminUserSeed.FirstName;
            var lastName = adminUserSeed.LastName;

            const string thisMethodName = nameof(SeedDatabaseWithAdminUserAsync);

            var adminUser = await _applicationUserManager.FindByNameAsync(name);
            if (adminUser != null)
            {
                _logger.LogInformation($"{thisMethodName}: adminUser already exists.");
                return IdentityResult.Success;
            }

            //Create the `Admin` Role if it does not exist
            var adminRole = await _roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                adminRole = new IdentityRole(roleName);
                var adminRoleResult = await _roleManager.CreateAsync(adminRole);
                if (adminRoleResult == IdentityResult.Failed())
                {
                    _logger.LogError($"{thisMethodName}: adminRole CreateAsync failed. ");
                    return IdentityResult.Failed();
                }
            }
            else
            {
                _logger.LogInformation($"{thisMethodName}: adminRole already exists.");
            }

            adminUser = new User
            {
                UserName = name,
                Email = email,
                EmailConfirmed = true,
                LockoutEnabled = true,
                FirstName = firstName,
                LastName = lastName,
                IsDeleted = true
            };
            var adminUserResult = await _applicationUserManager.CreateAsync(adminUser, password);
            if (adminUserResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser CreateAsync failed.");
                return IdentityResult.Failed();
            }

            var setLockoutResult = await _applicationUserManager.SetLockoutEnabledAsync(adminUser, enabled: false);
            if (setLockoutResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser SetLockoutEnabledAsync failed.");
                return IdentityResult.Failed();
            }

            var addToRoleResult = await _applicationUserManager.AddToRoleAsync(adminUser, adminRole.Name);
            if (addToRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser AddToRoleAsync failed.");
                return IdentityResult.Failed();
            }

            //await _applicationUserManager.AddOrUpdateClaimsAsync(adminUser.Id, "DynamicPermission",
            //    new List<string>
            //        {"Admin:DynamicAccess:Index", "Admin:UserManager:Index", "Admin:UserManager:RenderUser"});

            return IdentityResult.Success;
        }
    }
}