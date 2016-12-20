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
        private IPartsRepository repository;
        public int pageSize = 4;
        public PartsController(IPartsRepository repo)
        {
            repository = repo;

        }

        public ViewResult List(int page = 1)
        {
            PartsListViewModel model = new PartsListViewModel
            {
                Parts = repository.Parts
                .OrderBy(part => part.PartId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage=pageSize,
                    TotalItems = repository.Parts.Count()

                }


            };
            return View(model);
        }
    }
}