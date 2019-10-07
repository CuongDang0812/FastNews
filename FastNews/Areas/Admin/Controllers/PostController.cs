using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastNews.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        // GET: Admin/Post
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new PostDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public void SetviewBag(long? selectedId = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "CategoryID", "CategoryName", selectedId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetviewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                long id = dao.Insert(post);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Post");

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
            new PostDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = new PostDAO().viewDetail(id);
            SetviewBag(post.CategoryID);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                var result = dao.Updete(post);
                if (result)
                {
                    return RedirectToAction("Index", "Post");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetviewBag(post.CategoryID);
            return View("Index");
        }

    }
}