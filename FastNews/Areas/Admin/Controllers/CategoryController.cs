using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastNews.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                long id = dao.Insert(cate);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            return View("Index");
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cate = new CategoryDAO().viewDetail(id);
            return View(cate);
        }

        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                var result = dao.Updete(cate);
                if (result)
                {
                    return RedirectToAction("Index", "Category");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
    }
}