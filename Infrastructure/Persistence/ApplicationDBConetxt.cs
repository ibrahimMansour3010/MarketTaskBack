using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDBConetxt: IdentityDbContext<IdentityUser>
    {
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Order { get; set; }

        public ApplicationDBConetxt(DbContextOptions<ApplicationDBConetxt> options)
            : base(options)
        {

        }

    }
}
