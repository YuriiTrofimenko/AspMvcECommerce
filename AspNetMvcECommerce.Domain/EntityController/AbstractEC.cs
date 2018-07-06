using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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
                Object o = temp.GetValue(mDb, null);
                MethodInfo add = o.GetType().GetMethod("Add");
                add.Invoke(o, new object[] { _parrent });
            }
            mDb.SaveChanges();
            return _parrent;
        }


        public T Find(int _parrentId)
        {
            PropertyInfo temp = mDb.GetType().GetProperty(typeof(T).Name + "s");
            Object o = temp.GetValue(mDb, null);
            MethodInfo find = o.GetType().GetMethod("Find");
            /*Object o = Activator.CreateInstance(temp.GetType(),
                    BindingFlags.CreateInstance |
                     BindingFlags.Public |
                     BindingFlags.Instance |
                     BindingFlags.OptionalParamBinding,
                     null, new Object[] { Type.Missing }, null);*/
            //Object o = FormatterServices.GetUninitializedObject(temp.GetType());
            T result = (T)find.Invoke(o, new object[] { new object[] { _parrentId } });
            return result;

            /*return mDb.Roles.Find(_parrentId);*/
        }

        public T Remove(T _parrent)
        {
            PropertyInfo temp = mDb.GetType().GetProperty(typeof(T).Name + "s");
            Object o = temp.GetValue(mDb, null);
            MethodInfo remove = o.GetType().GetMethod("Remove");
            //FormatterServices.GetUninitializedObject(temp.GetType())
            //Object o = Activator.CreateInstance(temp.GetType());
            T result = (T)remove.Invoke(o, new object[] { new object[] { _parrent } });
            mDb.SaveChanges();
            return result;

            /*return mDb.Roles.Remove(_parrent);*/
        }
    }
}
