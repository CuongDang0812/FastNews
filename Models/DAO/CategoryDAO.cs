using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        FastNewsDbContext db = null;
        public CategoryDAO()
        {
            db = new FastNewsDbContext();
        }

        public Category GetById(string categoryName)
        {
            return db.Categories.SingleOrDefault(x => x.CategoryName == categoryName);
        }

        public Category viewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CategoryName.Contains(searchString));
            }
            return model.OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryID;
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.Categories.Find(id);
                db.Categories.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updete(Category entity)
        {
            try
            {
                var cate = db.Categories.Find(entity.CategoryID);
                cate.CategoryName = entity.CategoryName;
                cate.DisplayOrder = entity.DisplayOrder;
                cate.Status = entity.Status;
                cate.Target = entity.Target;
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
