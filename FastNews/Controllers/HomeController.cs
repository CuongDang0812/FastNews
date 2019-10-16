using System.Web.Mvc;

using Models.DAO;

namespace FastNews.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PostTest = new PostDAO().GetTop3Post();

            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var menu = new CategoryDAO().ShowMenuCategory();
            return PartialView(menu);
        }


        [ChildActionOnly]
        public ActionResult RecentPost()
        {
            var recent = new PostDAO().GetRecentPost();
            return PartialView(recent);
        }
    }
}