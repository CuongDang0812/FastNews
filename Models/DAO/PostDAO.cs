using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class PostDAO
    {
        FastNewsDbContext db = null;
        public PostDAO()
        {
            db = new FastNewsDbContext();
        }

        public Post GetById(string postName)
        {
            return db.Posts.SingleOrDefault(x => x.Title == postName);
        }

        public Post viewDetail(int id)
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

        public bool Updete(Post entity)
        {
            try
            {
                var post = db.Posts.Find(entity.PostID);
                post.Title = entity.Title;
                post.MataTitle = entity.MataTitle;
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

    }
}
