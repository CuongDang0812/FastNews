using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using FastNews.Common;

namespace FastNews.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new AccountDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public void SetviewBag(long? selectedId = null)
        {
            var dao = new RoleDAO();
            ViewBag.RoleID = new SelectList(dao.ListAll(), "RoleID", "RoleName", selectedId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetviewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account user)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var encrytedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encrytedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Account");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            SetviewBag();
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AccountDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user = new AccountDAO().viewDetail(id);
            SetviewBag(user.RoleID);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Account user)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encrytedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encrytedMd5Pas;
                }

                var result = dao.Updete(user);
                if (result)
                {
                    return RedirectToAction("Index", "Account");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetviewBag(user.RoleID);
            return View("Index");
        }

    }
}