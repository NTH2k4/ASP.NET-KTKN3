using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _57361.Models;

namespace _57361.Data
{
    public class _57361Context : DbContext
    {
        public _57361Context (DbContextOptions<_57361Context> options)
            : base(options)
        {
        }

        public DbSet<_57361.Models.Presentation> Presentation { get; set; } = default!;
        public DbSet<_57361.Models.Speaker> Speaker { get; set; } = default!;
    }
}
