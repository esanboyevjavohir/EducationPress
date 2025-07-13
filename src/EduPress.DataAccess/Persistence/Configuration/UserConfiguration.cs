using EduPress.Core.Entities;
using EduPress.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;

namespace EduPress.DataAccess.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(GetSeedUser());
        }

        const string seedSalt = "51a3a864-7ed8-4bff-bf5c-1e110ecc6c45";
        const string seedPassword1 = "dev2606";
        const string seedPassword2 = "dev4321";

        private List<User> GetSeedUser() => new()
        {
            new User
            {
                Id = new Guid("c0ae7f44-f3a2-4ea6-8030-01a4ea1b1aee"),
                FullName = "Esanboyev Javohir",
                Email = "javohir.netdeveloper@gmail.com",
                PhoneNumber = "+998933116612",
                Role = UserRole.Admin,
                IsActive = true,
                Salt = seedSalt,
                PasswordHash = Encrypt(seedPassword1, seedSalt),
                CreatedOn = DateTime.Now
            },

            new User
            {
                Id = new Guid("f67273d6-d1ee-4129-9740-75a8df1a5c5b"),
                FullName = "Bilol",
                Email = "biloldeveloper@gmail.com",
                PhoneNumber = "+998932884321",
                Role = UserRole.Student,
                IsActive = true,
                Salt = seedSalt,
                PasswordHash = Encrypt(seedPassword2, seedSalt),
                CreatedOn = DateTime.Now
            }
        };

        private static string Encrypt(string password, string salt)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                iterations: 1000,
                hashAlgorithm: HashAlgorithmName.SHA256);

            return Convert.ToBase64String(algorithm.GetBytes(32));
        }
    }
}
