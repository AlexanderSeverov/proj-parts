using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concreate
{
    public class EFPartRepository : IPartsRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Part> Parts
        {
            get { return context.Parts; }
        }

    }
}
