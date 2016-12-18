using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using WebUi.Controllers;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IPartsRepository> mock = new Mock<IPartsRepository>();
            mock.Setup(m => m.Parts).Returns(new List<Part>
            {
                new Part {PartId = 1,Name = "Part1" },
                 new Part {PartId = 2,Name = "Part2" },
                  new Part {PartId = 3,Name = "Part3" },
                   new Part {PartId = 4,Name = "Part4" },
                    new Part {PartId = 5,Name = "Part5" }
                     
            });

            PartsController controller = new PartsController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<Part> result = (IEnumerable<Part>)controller.List(2).Model;

            List<Part> parts = result.ToList();
            Assert.IsTrue(parts.Count == 2);
            Assert.AreEqual(parts[0].Name, "Part4");
            Assert.AreEqual(parts[1].Name, "Part5");
        }
    }
}
