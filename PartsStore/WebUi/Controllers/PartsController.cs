using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUi.Models;

namespace WebUi.Controllers
{
    public class PartsController : Controller
    {
        public int pageSize = 4;

        private IPartsRepository repository;
        
       
        public PartsController(IPartsRepository repo)
        {
            repository = repo;
           

        }

        public ViewResult List(string cat,int page = 1)
        {
            PartsListViewModel model = new PartsListViewModel
            {
                Parts = repository.Parts
                .Where(b => cat == null || b.Category == cat)
                .OrderBy(part => part.PartId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = cat == null ? repository.Parts.Count() : 
                    repository.Parts.Where(part => part.Category == cat).Count()

                },
                CurrentCat = cat

            };
            return View(model);
        }
    }
}