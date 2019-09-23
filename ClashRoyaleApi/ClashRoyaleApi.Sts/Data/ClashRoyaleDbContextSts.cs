using ClashRoyaleApi.Sts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleApi.Sts.Data
{
    public class ClashRoyaleDbContextSts : IdentityDbContext<ApplicationUser>
    {
        public ClashRoyaleDbContextSts(DbContextOptions<ClashRoyaleDbContextSts> options)
            : base(options)
        {

        }
    }
}
