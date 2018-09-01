using System;
using System.Linq;
using late.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace late
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LateContext(
                serviceProvider.GetRequiredService<DbContextOptions<LateContext>>()))
            {
                // Look for any movies.
                if (context.Catalog.Any())
                {
                    return;   // DB has been seeded
                }

                context.Catalog.AddRange(
                     new Catalog
                     {
                         Id = Guid.NewGuid(),
                         Url = ".Net",
                         Title = ".Net",
                         PRI = 0
                     }, new Catalog
                     {
                         Id = Guid.NewGuid(),
                         Url = "Electron",
                         Title = "Electron",
                         PRI = 1
                     }
                );
                context.SaveChanges();
            }
        }
    }

}
