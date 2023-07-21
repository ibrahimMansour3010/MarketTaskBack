using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDBConetxt _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationDbContextInitialiser(ApplicationDBConetxt conetxt, UserManager<IdentityUser> userManager)
        {
            _context = conetxt;
            _userManager = userManager;
        }
        public async Task InitialiseAsync()
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
        {
            if (!_context.Stock.Any())
            {
                var Random = new Random();
                var Stocks = new List<Stock>
            {
                new Stock(){Name = "Vianet",Price=Random.Next(1,101)},
                new Stock(){Name = "Agritek",Price=Random.Next(1,101)},
                new Stock(){Name = "Akamai",Price=Random.Next(1,101)},
                new Stock(){Name = "Baidu",Price=Random.Next(1,101)},
                new Stock(){Name = "Blinkx",Price=Random.Next(1,101)},
                new Stock(){Name = "Blucora",Price=Random.Next(1,101)},
                new Stock(){Name = "Boingo",Price=Random.Next(1,101)},
                new Stock(){Name = "Brainybrawn",Price=Random.Next(1,101)},
                new Stock(){Name = "Carbonite",Price=Random.Next(1,101)},
                new Stock(){Name = "China Finance",Price=Random.Next(1,101)},
                new Stock(){Name = "ChinaCache",Price=Random.Next(1,101)},
                new Stock(){Name = "ADR",Price=Random.Next(1,101)},
                new Stock(){Name = "ChitrChatr",Price=Random.Next(1,101)},
                new Stock(){Name = "Cnova",Price=Random.Next(1,101)},
                new Stock(){Name = "Cogent",Price=Random.Next(1,101)},
                new Stock(){Name = "Crexendo",Price=Random.Next(1,101)},
                new Stock(){Name = "CrowdGather",Price=Random.Next(1,101)},
                new Stock(){Name = "EarthLink",Price=Random.Next(1,101)},
                new Stock(){Name = "Eastern",Price=Random.Next(1,101)},
                new Stock(){Name = "ENDEXX",Price=Random.Next(1,101)},
                new Stock(){Name = "Envestnet",Price=Random.Next(1,101)},
                new Stock(){Name = "Epazz",Price=Random.Next(1,101)},
                new Stock(){Name = "FlashZero",Price=Random.Next(1,101)},
                new Stock(){Name = "Genesis",Price=Random.Next(1,101)},
                new Stock(){Name = "InterNAP",Price=Random.Next(1,101)},
                new Stock(){Name = "MeetMe",Price=Random.Next(1,101)},
                new Stock(){Name = "Netease",Price=Random.Next(1,101)},
                new Stock(){Name = "Qihoo",Price=Random.Next(1,101)},
            };
                await _context.Stock.AddRangeAsync(Stocks);
                await _context.SaveChangesAsync();
            }
        }
    }
}
