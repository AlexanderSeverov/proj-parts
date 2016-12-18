using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concreate
{
    public class EFDbContext : DbContext
    {
        public DbSet<Part> Parts { get; set;}

    }
}
