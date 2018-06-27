using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvcECommerce.Domain.EntityController
{
    public class UserEC
    {
        private AspNetMvcECommerceEntities mDb;
        public UserEC(AspNetMvcECommerceEntities _db) {
            mDb = _db;
        }

        public User Save(User _user)
        {
            User user = Find(_user.id);
            if (user == null)
            {
                mDb.Users.Add(_user);
                user = _user;
            }
            else {
                Type type = typeof(User);
                foreach (var prop in type.GetProperties())
                {
                    prop.SetValue(user, prop.GetValue(_user));
                }
            }
            
            mDb.SaveChanges();
            return user;
        }

        public User Find(int _userId)
        {
            return mDb.Users.Find(_userId);
        }

        public User Remove(User _user)
        {
            return mDb.Users.Remove(_user);
        }
    }
}
