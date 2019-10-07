using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastNews.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Admin/Role
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new RoleDAO();
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
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDAO();
                long id = dao.Insert(role);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Role");

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
            new RoleDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var role = new RoleDAO().viewDetail(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDAO();
                var result = dao.Updete(role);
                if (result)
                {
                    return RedirectToAction("Index", "Role");

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