using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.DAO
{
    public class AccountDAO
    {
        FastNewsDbContext db = null;
        public AccountDAO()
        {
            db = new FastNewsDbContext();
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Accounts.SingleOrDefault(x => x.AccountName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            if (result.IsLock == false)
            {
                return -1;
            }
            else
            {
                if (result.Password == passWord)
                    return 1;
                else
                    return -2;
            }
        }

        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.AccountName == userName);
        }

        public Account viewDetail(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.AccountName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.AccountName).ToPagedList(page, pageSize);
        }

        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.AccountID;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Accounts.Find(id);
                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updete(Account entity)
        {
            try
            {
                var user = db.Accounts.Find(entity.AccountID);
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Name = entity.Name;
                user.RoleID = entity.RoleID;
                user.IsLock = entity.IsLock;

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
