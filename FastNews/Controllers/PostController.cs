using System.Web.Mvc;

using Models.DAO;

namespace FastNews.Controllers
{
    public class PostController : Controller
    {
        public ActionResult PostDetail(int postID)
        {
            ViewBag.SimilarPost = new PostDAO().GetSimmilarPost(postID);

            ViewBag.PreviousPost = new PostDAO().GetPrevOrNextPost(postID - 1);
            ViewBag.NextPost = new PostDAO().GetPrevOrNextPost(postID + 1);

            var post = new PostDAO().ViewDetail(postID);
            return View(post);
        }


    }
}