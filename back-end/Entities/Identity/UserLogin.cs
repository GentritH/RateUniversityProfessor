using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RateForProfessor.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<int>
    {
    }

    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.ProviderKey });
        }
    }
}
