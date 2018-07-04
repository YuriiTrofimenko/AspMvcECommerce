using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvcECommerce.Domain.EntityController
{
    public class UserEC : AbstractEC<User>
    {
        //private AspNetMvcECommerceEntities mDb;
        public UserEC(AspNetMvcECommerceEntities _db) : base(_db) {
            //mDb = _db;
        }

        /*public User Save(User _user)
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
        }*/

        public User FindByLogin(string _login)
        {
            //TODO при регистрации не допускать повторения имен пользователей
            return mDb.Users.Where(u => (u.login == _login)).SingleOrDefault();
        }

        /*public User Remove(User _user)
        {
            return mDb.Users.Remove(_user);
        }*/
    }
}
