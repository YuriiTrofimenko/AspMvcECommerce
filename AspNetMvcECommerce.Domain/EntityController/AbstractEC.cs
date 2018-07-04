﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvcECommerce.Domain.EntityController
{
    public abstract class AbstractEC<T>
    {
        public AspNetMvcECommerceEntities mDb;
        public AbstractEC(AspNetMvcECommerceEntities _db)
        {
            mDb = _db;
        }

        public T Save(T _parrent)
        {
            //T parrent = Find((int)(_parrent.GetType().GetProperty("id").GetValue(_parrent)));
            int userId =
                (int)(_parrent.GetType().GetProperty("id").GetValue(_parrent));
            if (userId != 0)
            {
                T parrent = Find(userId);
                Type type = typeof(T);
                foreach (var prop in type.GetProperties())
                {
                    prop.SetValue(parrent, prop.GetValue(_parrent));
                }
            }
            else
            {
                PropertyInfo temp = mDb.GetType().GetProperty(typeof(T).Name + "s");
                MethodInfo add = temp.GetType().GetMethod("Add");
                add.Invoke(Activator.CreateInstance(temp.GetType()), new object[] { _parrent });
            }
            mDb.SaveChanges();
            return _parrent;
        }


        public T Find(int _parrentId)
        {
            PropertyInfo temp = mDb.GetType().GetProperty(typeof(T).Name + "s");
            MethodInfo find = temp.GetType().GetMethod("Find");
            T result = (T)find.Invoke(Activator.CreateInstance(temp.GetType()), new object[] { _parrentId });
            return result;

            /*return mDb.Roles.Find(_parrentId);*/
        }

        public T Remove(T _parrent)
        {
            PropertyInfo temp = mDb.GetType().GetProperty(typeof(T).Name + "s");
            MethodInfo remove = temp.GetType().GetMethod("Remove");
            T result = (T)remove.Invoke(Activator.CreateInstance(temp.GetType()), new object[] { _parrent });
            mDb.SaveChanges();
            return result;

            /*return mDb.Roles.Remove(_parrent);*/
        }
    }
}
