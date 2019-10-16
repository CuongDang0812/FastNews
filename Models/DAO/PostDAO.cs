using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Models.ViewModel;

namespace Models.DAO
{
    public class PostDAO
    {
        FastNewsDbContext db = null;
        public PostDAO()
        {
            db = new FastNewsDbContext();
        }

        // no use
        //public Post GetById(string postName)
        //{
        //    return db.Posts.SingleOrDefault(x => x.Title == postName);
        //}

        public Post ViewDetail(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString));
            }
            return model.OrderBy(x => x.Title).ToPagedList(page, pageSize);
        }

        public long Insert(Post entity)
        {
            db.Posts.Add(entity);
            db.SaveChanges();
            return entity.PostID;
        }

        public bool Delete(int id)
        {
            try
            {
                var post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Post entity)
        {
            try
            {
                var post = db.Posts.Find(entity.PostID);
                post.Title = entity.Title;
                post.MetaTitle = entity.MetaTitle;
                post.Decription = entity.Decription;
                post.Image = entity.Image;
                post.ContentDetail = entity.ContentDetail;
                post.CategoryID = entity.CategoryID;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }

        //Add function in Client web

        public List<Post> GetRecentPost()
        {
            var post = db.Posts.OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
            List<PostViewModel> result = new List<PostViewModel>();
            foreach (var item in post)
            {
                var categoryMetaTitle = this.db.Categories.Find(item.CategoryID).MetaTitle;

                // Gán đỡ giá trị MetaTitle của thằng Category vào nội dung của thằng Post
                item.ContentDetail = categoryMetaTitle;
            }

            return post;
        }

        public List<PostViewModel> GetTop3Post()
        {
            List<PostViewModel> listResult = new List<PostViewModel>();
            List<int> listCategoryID = this.db.Categories.Where(x => x.ShowOnMenu == true).Select(x => x.CategoryID).ToList();


            foreach (var itemID in listCategoryID)
            {
                var category = this.db.Categories.Find(itemID);

                var post = (from p in this.db.Posts
                            join c in this.db.Categories
                                on p.CategoryID equals c.CategoryID
                            where c.CategoryID == itemID && c.ShowOnMenu == true
                            select p).OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
                listResult.Add(new PostViewModel()
                {
                    CategoryName = category.CategoryName,
                    CategoryMetaTitle = category.MetaTitle,
                    ListPosts = post
                });
            }
            return listResult;
        }

        //public List<Post> GetListPostFromCategoryMetaTitle(int categoryID)
        //{
        //    return this.db.Posts.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.DatetimeCreate).ToList();
        //}

        public PostViewModel GetDetailCategoryList(int categoryID)
        {
            PostViewModel viewResult = new PostViewModel();

            var category = this.db.Categories.Find(categoryID);
            var listPost = this.db.Posts.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.DatetimeCreate)
                .ToList();

            viewResult.CategoryName = category.CategoryName;
            viewResult.CategoryMetaTitle = category.MetaTitle;
            viewResult.ListPosts = listPost;

            return viewResult;
        }
        public List<Post> GetSimmilarPost(int postID)
        {
            var cateId = this.db.Posts.Find(postID).CategoryID;
            var cateMeta = this.db.Categories.Find(cateId).MetaTitle;

            var listPost = (from p in this.db.Posts
                          join c in this.db.Categories
                              on p.CategoryID equals c.CategoryID
                          where p.PostID != postID && p.CategoryID == cateId
                          select p).OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
            foreach (var item in listPost)
            {
                item.ContentDetail = cateMeta;
            }
            return listPost;
        }

        public Post GetPrevOrNextPost(int postID)
        {
            var post = this.db.Posts.Find(postID);
            if (post != null)
            {
                var categorMetaTitle = this.db.Categories.Find(post.CategoryID).MetaTitle;

                // Gán đỡ giá trị MetaTitle của thằng Category vào nội dung của thằng Post
                post.ContentDetail = categorMetaTitle;
            }
            return post;
        }
       
        //Bat exception khi next or prev NullReferenceException 

        //get similar chua cung the loai
     
    }
}
