using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _57362.Models;

namespace _57362.Data
{
    public class _57362Context : DbContext
    {
        public _57362Context (DbContextOptions<_57362Context> options)
            : base(options)
        {
        }

        public DbSet<_57362.Models.Curator> Curator { get; set; } = default!;
        public DbSet<_57362.Models.Exhibition> Exhibition { get; set; } = default!;
    }
}
