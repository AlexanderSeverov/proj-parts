using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUi.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }  //всего запов
        public int ItemsPerPage { get; set; } //кол запов на страниц
        public int CurrentPage { get; set; } //номерок текущ страниц
        public int TotalPages //всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);  }

        }
    }
}