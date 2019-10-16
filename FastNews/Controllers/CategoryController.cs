using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models.DAO;

namespace FastNews.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult CategoryDetail(int categoryID)
        {
            var category = new PostDAO().GetDetailCategoryList(categoryID);
            //ViewBag.CategoryDetail = new PostDAO().GetDetailCategoryList(categoryID);
            return View(category);
        }

    }
}