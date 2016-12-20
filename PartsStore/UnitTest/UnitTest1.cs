using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUi.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUi.Models;
using WebUI.HtmlHelpers;

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

           PartsListViewModel result = (PartsListViewModel)controller.List(2).Model;

            List<Part> parts = result.Parts.ToList();
            Assert.IsTrue(parts.Count == 2);
            Assert.AreEqual(parts[0].Name, "Part4");
            Assert.AreEqual(parts[1].Name, "Part5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
      
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IPartsRepository> mock = new Mock<IPartsRepository>();
            mock.Setup(m => m.Parts).Returns(new List<Part>
            {
                new Part{PartId = 1, Name = "Part1"},
                new Part{PartId = 2, Name = "Part2"},
                new Part{PartId = 3, Name = "Part3"},
                new Part{PartId = 4, Name = "Part4"},
                new Part{PartId = 5, Name = "Part5"}
            });

            PartsController controller = new PartsController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            PartsListViewModel result = (PartsListViewModel)controller.List(2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}

        