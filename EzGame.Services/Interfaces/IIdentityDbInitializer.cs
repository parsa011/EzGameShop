using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EzGame.Services.Interfaces
{
    public interface IIdentityDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// and virtual DB
        /// </summary>
        void Initialize();

        /// <summary>
        /// seed data
        /// </summary>
        void SeedData();

        Task<IdentityResult> SeedDatabaseWithAdminUserAsync();
    }
}