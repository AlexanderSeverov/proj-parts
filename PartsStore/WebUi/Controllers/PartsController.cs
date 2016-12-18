using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

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
            return View(repository.Parts
                .OrderBy(part => part.PartId)
                .Skip((page - 1)*pageSize)
                .Take(pageSize));
        }
    }
}