using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{

    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Part part1 = new Part { PartId = 1, Name = "Part1" };
            Part part2 = new Part { PartId = 2, Name = "Part2" };

            Cart cart = new Cart();

            cart.AddItem(part1, 1);
            cart.AddItem(part2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Part, part1);
            Assert.AreEqual(results[1].Part, part2);



        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Part part1 = new Part { PartId = 1, Name = "Part1" };
            Part part2 = new Part { PartId = 2, Name = "Part2" };

            Cart cart = new Cart();

            cart.AddItem(part1, 1);
            cart.AddItem(part2, 1);
            cart.AddItem(part1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c=> c.Part.PartId).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);

        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            Part part1 = new Part { PartId = 1, Name = "Part1" };
            Part part2 = new Part { PartId = 2, Name = "Part2" };
            Part part3 = new Part { PartId = 3, Name = "Part3" };

            Cart cart = new Cart();

            cart.AddItem(part1, 1);
            cart.AddItem(part2, 1);
            cart.AddItem(part1, 5);
            cart.AddItem(part3, 2);
            cart.RemoveLine(part2);


            Assert.AreEqual(cart.Lines.Where(c=> c.Part == part2).Count(),0);
            Assert.AreEqual(cart.Lines.Count(), 2);


        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            Part part1 = new Part { PartId = 1, Name = "Part1",Price = 100 };
            Part part2 = new Part { PartId = 2, Name = "Part2",Price = 55 };
           

            Cart cart = new Cart();

            cart.AddItem(part1, 1);
            cart.AddItem(part2, 1);
            cart.AddItem(part1, 5);
            decimal result = cart.ComputerTotalValue();

            Assert.AreEqual(result,655);
         }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            Part part1 = new Part { PartId = 1, Name = "Part1", Price = 100 };
            Part part2 = new Part { PartId = 2, Name = "Part2", Price = 55 };


            Cart cart = new Cart();

            cart.AddItem(part1, 1);
            cart.AddItem(part2, 1);
            cart.AddItem(part1, 5);
            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);

        }

    }
}






