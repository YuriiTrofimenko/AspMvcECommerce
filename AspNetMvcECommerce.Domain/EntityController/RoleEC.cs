using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvcECommerce.Domain.EntityController
{
    public class RoleEC
    {
        private AspNetMvcECommerceEntities mDb;
        public RoleEC(AspNetMvcECommerceEntities _db) {
            mDb = _db;
        }

        public Role Save(Role _role)
        {
            Role role = Find(_role.id);
            if (role == null)
            {
                mDb.Roles.Add(_role);
                role = _role;
            }
            else {
                Type type = typeof(Role);
                foreach (var prop in type.GetProperties())
                {
                    prop.SetValue(role, prop.GetValue(_role));
                }
            }
            
            mDb.SaveChanges();
            return role;
        }

        public Role Find(int _roleId)
        {
            return mDb.Roles.Find(_roleId);
        }

        public Role Remove(Role _role)
        {
            return mDb.Roles.Remove(_role);
        }
    }
}
