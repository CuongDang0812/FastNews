using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.DAO
{
    public class RoleDAO
    {
        FastNewsDbContext db = null;
        public RoleDAO()
        {
            db = new FastNewsDbContext();
        }

        public List<Role> ListAll()
        {
            return db.Roles.Where(x => x.Status == true).ToList();
        }

        public Role GetById(string roleName)
        {
            return db.Roles.SingleOrDefault(x => x.RoleName == roleName);
        }

        public Role viewDetail(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Role> model = db.Roles;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.RoleName.Contains(searchString));
            }
            return model.OrderBy(x => x.RoleID).ToPagedList(page, pageSize);
        }

        public long Insert(Role entity)
        {
            db.Roles.Add(entity);
            db.SaveChanges();
            return entity.RoleID;
        }

        public bool Delete(int id)
        {
            try
            {
                var role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updete(Role entity)
        {
            try
            {
                var role = db.Roles.Find(entity.RoleID);
                role.RoleName = entity.RoleName;
                role.Status = entity.Status;
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
