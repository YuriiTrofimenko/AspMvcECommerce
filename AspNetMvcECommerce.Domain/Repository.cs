using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetMvcECommerce.Domain.EntityController;

namespace AspNetMvcECommerce.Domain
{
    public class Repository : IRepository
    {
        private AspNetMvcECommerceEntities mDb;
        public UserEC UserEC => new UserEC(mDb);
        public RoleEC RoleEC => new RoleEC(mDb);

        public Repository() {
            mDb = new AspNetMvcECommerceEntities();
        }
    }
}
