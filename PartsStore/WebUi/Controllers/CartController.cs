using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUi.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPartsRepository repository;
        public CartController(IPartsRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int partId, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(b => b.PartId == partId);

            if (part != null)
            {
                cart.AddItem(part, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int partId, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(b => b.PartId == partId);

            if (part != null)
            {
                cart.RemoveLine(part);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

    }
}