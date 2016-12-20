using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUi.Models
{
    public class PartsListViewModel
    {
        public IEnumerable<Part> Parts { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}