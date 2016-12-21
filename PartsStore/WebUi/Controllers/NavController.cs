using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUi.Controllers
{
    public class NavController : Controller
    {
        private IPartsRepository repository;

        public NavController(IPartsRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string cat = null)
        {
            ViewBag.SelectedCategory = cat;

            IEnumerable<string> cats = repository.Parts
                .Select(part => part.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(cats);
        }
    }
}