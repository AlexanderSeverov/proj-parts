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
        private IOrderProcessor orderProcessor;


        public CartController(IPartsRepository repo,IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
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

        public ViewResult Checkout ()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините корзина пуста");

            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }
    }
}