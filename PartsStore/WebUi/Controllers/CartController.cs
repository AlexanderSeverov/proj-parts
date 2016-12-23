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

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int partId, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(b => b.PartId == partId);

            if (part != null)
            {
                GetCart().AddItem(part, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int partId, string returnUrl)
        {
            Part part = repository.Parts
                .FirstOrDefault(b => b.PartId == partId);

            if (part != null)
            {
                GetCart().RemoveLine(part);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

    }
}